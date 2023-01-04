using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace TestRunner.Common
{
    public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo
    {
        #region Private Variables
        private UIElementCollection children;
        private ItemsControl itemsControl;
        private readonly Dictionary<UIElement, Rect> realizedChildLayout = new Dictionary<UIElement, Rect>();
        private WrapPanelAbstraction abstractPanel;
        private IItemContainerGenerator generator;
        private Size childSize;
        private Size pixelMeasuredViewport = new Size(0, 0);
        private Size extent = new Size(0, 0);
        private Size viewport = new Size(0, 0);
        private Point offset = new Point(0, 0);
        private int firstIndex = 0;
        #endregion

        #region Properties
        private Size ChildSlotSize
        {
            get
            {
                return new Size(ItemWidth, ItemHeight);
            }
        }
        #endregion

        #region Dependency Properties
        [TypeConverter(typeof(LengthConverter))]
        public double ItemHeight
        {
            get
            {
                return (double)GetValue(ItemHeightProperty);
            }
            set
            {
                SetValue(ItemHeightProperty, value);
            }
        }

        [TypeConverter(typeof(LengthConverter))]
        public double ItemWidth
        {
            get
            {
                return (double)GetValue(ItemWidthProperty);
            }
            set
            {
                SetValue(ItemWidthProperty, value);
            }
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(double.PositiveInfinity));
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register("ItemWidth", typeof(double), typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(double.PositiveInfinity));
        public static readonly DependencyProperty OrientationProperty = StackPanel.OrientationProperty.AddOwner(typeof(VirtualizingWrapPanel), new FrameworkPropertyMetadata(Orientation.Horizontal));
        #endregion

        #region Methods
        public void SetFirstRowViewItemIndex(int index)
        {
            SetVerticalOffset((index) / Math.Floor((viewport.Width) / childSize.Width));
            SetHorizontalOffset((index) / Math.Floor((viewport.Height) / childSize.Height));
        }

        private void Resizing(object sender, EventArgs e)
        {
            if (viewport.Width != 0)
            {
                int firstIndexCache = firstIndex;
                abstractPanel = null;
                MeasureOverride(viewport);
                SetFirstRowViewItemIndex(firstIndex);
                firstIndex = firstIndexCache;
            }
        }

        public int GetFirstVisibleSection()
        {
            try
            {
                int section;
                var maxSection = abstractPanel.Max(x => x.Section);
                if (Orientation == Orientation.Horizontal)
                {
                    section = (int)offset.Y;
                }
                else
                {
                    section = (int)offset.X;
                }
                if (section > maxSection)
                    section = maxSection;
                return section;
            }
            catch
            {
                return 0;
            }
        }

        public int GetFirstVisibleIndex()
        {
            try
            {
                int section = GetFirstVisibleSection();
                var item = abstractPanel.Where(x => x.Section == section).FirstOrDefault();
                if (item != null)
                    return item._index;
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private void CleanUpItems(int minDesiredGenerated, int maxDesiredGenerated)
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                GeneratorPosition childGeneratorPos = new GeneratorPosition(i, 0);
                int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPos);
                if (itemIndex < minDesiredGenerated || itemIndex > maxDesiredGenerated)
                {
                    generator.Remove(childGeneratorPos, 1);
                    RemoveInternalChildRange(i, 1);
                }
            }
        }

        private void ComputeExtentAndViewport(Size pixelMeasuredViewportSize, int visibleSections)
        {
            if (Orientation == Orientation.Horizontal)
            {
                viewport.Height = visibleSections;
                viewport.Width = pixelMeasuredViewportSize.Width;
            }
            else
            {
                viewport.Width = visibleSections;
                viewport.Height = pixelMeasuredViewportSize.Height;
            }

            if (Orientation == Orientation.Horizontal)
            {
                extent.Height = abstractPanel.SectionCount + ViewportHeight - 1;

            }
            else
            {
                extent.Width = abstractPanel.SectionCount + ViewportWidth - 1;
            }
            _owner.InvalidateScrollInfo();
        }

        private void ResetScrollInfo()
        {
            offset.X = 0;
            offset.Y = 0;
        }

        private int GetNextSectionClosestIndex(int itemIndex)
        {
            ItemAbstraction abstractItem = abstractPanel[itemIndex];
            if (abstractItem.Section < abstractPanel.SectionCount - 1)
            {
                ItemAbstraction ret = abstractPanel.
                    Where(x => x.Section == abstractItem.Section + 1).
                    OrderBy(x => Math.Abs(x.SectionIndex - abstractItem.SectionIndex)).
                    First();
                return ret._index;
            }
            else
                return itemIndex;
        }

        private int GetLastSectionClosestIndex(int itemIndex)
        {
            ItemAbstraction abstractItem = abstractPanel[itemIndex];
            if (abstractItem.Section > 0)
            {
                ItemAbstraction ret = abstractPanel.
                    Where(x => x.Section == abstractItem.Section - 1).
                    OrderBy(x => Math.Abs(x.SectionIndex - abstractItem.SectionIndex)).
                    First();
                return ret._index;
            }
            else
                return itemIndex;
        }

        private void NavigateDown()
        {
            ItemContainerGenerator gen = generator.GetItemContainerGeneratorForPanel(this);
            UIElement selected = (UIElement)Keyboard.FocusedElement;
            int itemIndex = gen.IndexFromContainer(selected);
            int depth = 0;
            while (itemIndex == -1)
            {
                selected = (UIElement)VisualTreeHelper.GetParent(selected);
                itemIndex = gen.IndexFromContainer(selected);
                depth++;
            }
            DependencyObject next = null;
            if (Orientation == Orientation.Horizontal)
            {
                int nextIndex = GetNextSectionClosestIndex(itemIndex);
                next = gen.ContainerFromIndex(nextIndex);
                while (next == null)
                {
                    SetVerticalOffset(VerticalOffset + 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(nextIndex);
                }
            }
            else
            {
                if (itemIndex == abstractPanel._itemCount - 1)
                    return;
                next = gen.ContainerFromIndex(itemIndex + 1);
                while (next == null)
                {
                    SetHorizontalOffset(HorizontalOffset + 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(itemIndex + 1);
                }
            }
            while (depth != 0)
            {
                next = VisualTreeHelper.GetChild(next, 0);
                depth--;
            }
            (next as UIElement).Focus();
        }

        private void NavigateLeft()
        {
            ItemContainerGenerator gen = generator.GetItemContainerGeneratorForPanel(this);

            UIElement selected = (UIElement)Keyboard.FocusedElement;
            int itemIndex = gen.IndexFromContainer(selected);
            int depth = 0;
            while (itemIndex == -1)
            {
                selected = (UIElement)VisualTreeHelper.GetParent(selected);
                itemIndex = gen.IndexFromContainer(selected);
                depth++;
            }
            DependencyObject next;
            if (Orientation == Orientation.Vertical)
            {
                int nextIndex = GetLastSectionClosestIndex(itemIndex);
                next = gen.ContainerFromIndex(nextIndex);
                while (next == null)
                {
                    SetHorizontalOffset(HorizontalOffset - 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(nextIndex);
                }
            }
            else
            {
                if (itemIndex == 0)
                    return;
                next = gen.ContainerFromIndex(itemIndex - 1);
                while (next == null)
                {
                    SetVerticalOffset(VerticalOffset - 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(itemIndex - 1);
                }
            }
            while (depth != 0)
            {
                next = VisualTreeHelper.GetChild(next, 0);
                depth--;
            }
            (next as UIElement).Focus();
        }

        private void NavigateRight()
        {
            ItemContainerGenerator gen = generator.GetItemContainerGeneratorForPanel(this);
            UIElement selected = (UIElement)Keyboard.FocusedElement;
            int itemIndex = gen.IndexFromContainer(selected);
            int depth = 0;
            while (itemIndex == -1)
            {
                selected = (UIElement)VisualTreeHelper.GetParent(selected);
                itemIndex = gen.IndexFromContainer(selected);
                depth++;
            }
            DependencyObject next;
            if (Orientation == Orientation.Vertical)
            {
                int nextIndex = GetNextSectionClosestIndex(itemIndex);
                next = gen.ContainerFromIndex(nextIndex);
                while (next == null)
                {
                    SetHorizontalOffset(HorizontalOffset + 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(nextIndex);
                }
            }
            else
            {
                if (itemIndex == abstractPanel._itemCount - 1)
                    return;
                next = gen.ContainerFromIndex(itemIndex + 1);
                while (next == null)
                {
                    SetVerticalOffset(VerticalOffset + 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(itemIndex + 1);
                }
            }
            while (depth != 0)
            {
                next = VisualTreeHelper.GetChild(next, 0);
                depth--;
            }
            (next as UIElement).Focus();
        }

        private void NavigateUp()
        {
            ItemContainerGenerator gen = generator.GetItemContainerGeneratorForPanel(this);
            UIElement selected = (UIElement)Keyboard.FocusedElement;
            int itemIndex = gen.IndexFromContainer(selected);
            int depth = 0;
            while (itemIndex == -1)
            {
                selected = (UIElement)VisualTreeHelper.GetParent(selected);
                itemIndex = gen.IndexFromContainer(selected);
                depth++;
            }
            DependencyObject next;
            if (Orientation == Orientation.Horizontal)
            {
                int nextIndex = GetLastSectionClosestIndex(itemIndex);
                next = gen.ContainerFromIndex(nextIndex);
                while (next == null)
                {
                    SetVerticalOffset(VerticalOffset - 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(nextIndex);
                }
            }
            else
            {
                if (itemIndex == 0)
                    return;
                next = gen.ContainerFromIndex(itemIndex - 1);
                while (next == null)
                {
                    SetHorizontalOffset(HorizontalOffset - 1);
                    UpdateLayout();
                    next = gen.ContainerFromIndex(itemIndex - 1);
                }
            }
            while (depth != 0)
            {
                next = VisualTreeHelper.GetChild(next, 0);
                depth--;
            }
            (next as UIElement).Focus();
        }
        #endregion

        #region Override
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    NavigateDown();
                    e.Handled = true;
                    break;
                case Key.Left:
                    NavigateLeft();
                    e.Handled = true;
                    break;
                case Key.Right:
                    NavigateRight();
                    e.Handled = true;
                    break;
                case Key.Up:
                    NavigateUp();
                    e.Handled = true;
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        protected override void OnItemsChanged(object sender, ItemsChangedEventArgs args)
        {
            base.OnItemsChanged(sender, args);
            abstractPanel = null;
            ResetScrollInfo();
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.SizeChanged += new SizeChangedEventHandler(this.Resizing);
            base.OnInitialized(e);
            itemsControl = ItemsControl.GetItemsOwner(this);
            children = InternalChildren;
            generator = ItemContainerGenerator;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            try
            {
                if (itemsControl == null || itemsControl.Items.Count == 0)
                    return availableSize;
                if (abstractPanel == null)
                    abstractPanel = new WrapPanelAbstraction(itemsControl.Items.Count);

                pixelMeasuredViewport = availableSize;

                realizedChildLayout.Clear();

                Size realizedFrameSize = availableSize;

                int itemCount = itemsControl.Items.Count;
                int firstVisibleIndex = GetFirstVisibleIndex();

                GeneratorPosition startPos = generator.GeneratorPositionFromIndex(firstVisibleIndex);

                int childIndex = (startPos.Offset == 0) ? startPos.Index : startPos.Index + 1;
                int current = firstVisibleIndex;
                int visibleSections = 1;
                using (generator.StartAt(startPos, GeneratorDirection.Forward, true))
                {
                    bool stop = false;
                    bool isHorizontal = Orientation == Orientation.Horizontal;
                    double currentX = 0;
                    double currentY = 0;
                    double maxItemSize = 0;
                    int currentSection = GetFirstVisibleSection();
                    while (current < itemCount)
                    {
                        // Get or create the child                    
                        UIElement child = generator.GenerateNext(out bool newlyRealized) as UIElement;
                        if (newlyRealized)
                        {
                            // Figure out if we need to insert the child at the end or somewhere in the middle
                            if (childIndex >= children.Count)
                            {
                                AddInternalChild(child);
                            }
                            else
                            {
                                InsertInternalChild(childIndex, child);
                            }
                            generator.PrepareItemContainer(child);
                            child.Measure(ChildSlotSize);
                        }
                        else
                        {
                            // The child has already been created, let's be sure it's in the right spot
                            //Debug.Assert(child == children[childIndex], "Wrong child was generated");
                        }
                        childSize = child.DesiredSize;
                        Rect childRect = new Rect(new Point(currentX, currentY), childSize);
                        if (isHorizontal)
                        {
                            maxItemSize = Math.Max(maxItemSize, childRect.Height);
                            if (childRect.Right > realizedFrameSize.Width) //wrap to a new line
                            {
                                currentY += maxItemSize;
                                currentX = 0;
                                maxItemSize = childRect.Height;
                                childRect.X = currentX;
                                childRect.Y = currentY;
                                currentSection++;
                                visibleSections++;
                            }
                            if (currentY > realizedFrameSize.Height)
                                stop = true;
                            currentX = childRect.Right;
                        }
                        else
                        {
                            maxItemSize = Math.Max(maxItemSize, childRect.Width);
                            if (childRect.Bottom > realizedFrameSize.Height) //wrap to a new column
                            {
                                currentX += maxItemSize;
                                currentY = 0;
                                maxItemSize = childRect.Width;
                                childRect.X = currentX;
                                childRect.Y = currentY;
                                currentSection++;
                                visibleSections++;
                            }
                            if (currentX > realizedFrameSize.Width)
                                stop = true;
                            currentY = childRect.Bottom;
                        }
                        realizedChildLayout.Add(child, childRect);
                        abstractPanel.SetItemSection(current, currentSection);

                        if (stop)
                            break;
                        current++;
                        childIndex++;
                    }
                }
                CleanUpItems(firstVisibleIndex, current - 1);
                ComputeExtentAndViewport(availableSize, visibleSections);

                return availableSize;
            }
            catch
            {
                return availableSize;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (children != null)
            {
                foreach (UIElement child in children)
                {
                    Rect layoutInfo = realizedChildLayout[child];
                    child.Arrange(layoutInfo);
                }
            }
            return finalSize;
        }

        #endregion

        #region IScrollInfo Members
        private bool _canHScroll = false;
        public bool CanHorizontallyScroll
        {
            get { return _canHScroll; }
            set { _canHScroll = value; }
        }

        private bool _canVScroll = false;
        public bool CanVerticallyScroll
        {
            get { return _canVScroll; }
            set { _canVScroll = value; }
        }

        public double ExtentHeight
        {
            get { return extent.Height; }
        }

        public double ExtentWidth
        {
            get { return extent.Width; }
        }

        public double HorizontalOffset
        {
            get { return offset.X; }
        }

        public double VerticalOffset
        {
            get { return offset.Y; }
        }

        public void LineDown()
        {
            if (Orientation == Orientation.Vertical)
                SetVerticalOffset(VerticalOffset + 20);
            else
                SetVerticalOffset(VerticalOffset + 1);
        }

        public void LineLeft()
        {
            if (Orientation == Orientation.Horizontal)
                SetHorizontalOffset(HorizontalOffset - 20);
            else
                SetHorizontalOffset(HorizontalOffset - 1);
        }

        public void LineRight()
        {
            if (Orientation == Orientation.Horizontal)
                SetHorizontalOffset(HorizontalOffset + 20);
            else
                SetHorizontalOffset(HorizontalOffset + 1);
        }

        public void LineUp()
        {
            if (Orientation == Orientation.Vertical)
                SetVerticalOffset(VerticalOffset - 20);
            else
                SetVerticalOffset(VerticalOffset - 1);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            ItemContainerGenerator gen = generator.GetItemContainerGeneratorForPanel(this);
            UIElement element = (UIElement)visual;
            int itemIndex = gen.IndexFromContainer(element);
            while (itemIndex == -1)
            {
                element = (UIElement)VisualTreeHelper.GetParent(element);
                itemIndex = gen.IndexFromContainer(element);
            }

            Rect elementRect = realizedChildLayout[element];
            if (Orientation == Orientation.Horizontal)
            {
                double viewportHeight = pixelMeasuredViewport.Height;
                if (elementRect.Bottom > viewportHeight)
                    offset.Y += 1;
                else if (elementRect.Top < 0)
                    offset.Y -= 1;
            }
            else
            {
                double viewportWidth = pixelMeasuredViewport.Width;
                if (elementRect.Right > viewportWidth)
                    offset.X += 1;
                else if (elementRect.Left < 0)
                    offset.X -= 1;
            }
            InvalidateMeasure();
            return elementRect;
        }

        public void MouseWheelDown()
        {
            PageDown();
        }

        public void MouseWheelLeft()
        {
            PageLeft();
        }

        public void MouseWheelRight()
        {
            PageRight();
        }

        public void MouseWheelUp()
        {
            PageUp();
        }

        public void PageDown()
        {
            SetVerticalOffset(VerticalOffset + viewport.Height * 0.8);
        }

        public void PageLeft()
        {
            SetHorizontalOffset(HorizontalOffset - viewport.Width * 0.8);
        }

        public void PageRight()
        {
            SetHorizontalOffset(HorizontalOffset + viewport.Width * 0.8);
        }

        public void PageUp()
        {
            SetVerticalOffset(VerticalOffset - viewport.Height * 0.8);
        }

        private ScrollViewer _owner;
        public ScrollViewer ScrollOwner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public void SetHorizontalOffset(double offset)
        {
            if (offset < 0 || viewport.Width >= extent.Width)
            {
                offset = 0;
            }
            else
            {
                if (offset + viewport.Width >= extent.Width)
                {
                    offset = extent.Width - viewport.Width;
                }
            }

            this.offset.X = offset;

            if (_owner != null)
                _owner.InvalidateScrollInfo();

            InvalidateMeasure();
            firstIndex = GetFirstVisibleIndex();
        }

        public void SetVerticalOffset(double offset)
        {
            if (offset < 0 || viewport.Height >= extent.Height)
            {
                offset = 0;
            }
            else
            {
                if (offset + viewport.Height >= extent.Height)
                {
                    offset = extent.Height - viewport.Height;
                }
            }

            this.offset.Y = offset;

            if (_owner != null)
                _owner.InvalidateScrollInfo();

            InvalidateMeasure();
            firstIndex = GetFirstVisibleIndex();
        }

        public double ViewportHeight
        {
            get { return viewport.Height; }
        }

        public double ViewportWidth
        {
            get { return viewport.Width; }
        }
        #endregion

        #region Helper Class
        class ItemAbstraction
        {
            private readonly WrapPanelAbstraction _panel;
            public readonly int _index;

            int _sectionIndex = -1;
            public int SectionIndex
            {
                get
                {
                    if (_sectionIndex == -1)
                    {
                        return _index % _panel._averageItemsPerSection - 1;
                    }
                    return _sectionIndex;
                }
                set
                {
                    if (_sectionIndex == -1)
                        _sectionIndex = value;
                }
            }

            private int _section = -1;
            public int Section
            {
                get
                {
                    if (_section == -1)
                    {
                        return _index / _panel._averageItemsPerSection;
                    }
                    return _section;
                }
                set
                {
                    if (_section == -1)
                        _section = value;
                }
            }

            public ItemAbstraction(WrapPanelAbstraction panel, int index)
            {
                _panel = panel;
                _index = index;
            }
        }

        class WrapPanelAbstraction : IEnumerable<ItemAbstraction>
        {
            public readonly int _itemCount;
            public int _averageItemsPerSection;
            private int _currentSetSection = -1;
            private int _currentSetItemIndex = -1;
            private int _itemsInCurrentSecction = 0;
            private readonly object _syncRoot = new object();

            public int SectionCount
            {
                get
                {
                    int ret = _currentSetSection + 1;
                    if (_currentSetItemIndex + 1 < Items.Count)
                    {
                        int itemsLeft = Items.Count - _currentSetItemIndex;
                        ret += itemsLeft / _averageItemsPerSection + 1;
                    }
                    return ret;
                }
            }

            public IEnumerator<ItemAbstraction> GetEnumerator()
            {
                return Items.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public ItemAbstraction this[int index]
            {
                get { return Items[index]; }
            }

            private ReadOnlyCollection<ItemAbstraction> Items { get; set; }

            public WrapPanelAbstraction(int itemCount)
            {
                List<ItemAbstraction> items = new List<ItemAbstraction>(itemCount);
                for (int i = 0; i < itemCount; i++)
                {
                    ItemAbstraction item = new ItemAbstraction(this, i);
                    items.Add(item);
                }

                Items = new ReadOnlyCollection<ItemAbstraction>(items);
                _averageItemsPerSection = itemCount;
                _itemCount = itemCount;
            }

            public void SetItemSection(int index, int section)
            {
                lock (_syncRoot)
                {
                    if (section <= _currentSetSection + 1 && index == _currentSetItemIndex + 1)
                    {
                        _currentSetItemIndex++;
                        Items[index].Section = section;
                        if (section == _currentSetSection + 1)
                        {
                            _currentSetSection = section;
                            if (section > 0)
                            {
                                _averageItemsPerSection = (index) / (section);
                            }
                            _itemsInCurrentSecction = 1;
                        }
                        else
                            _itemsInCurrentSecction++;
                        Items[index].SectionIndex = _itemsInCurrentSecction - 1;
                    }
                }
            }
        }
        #endregion
    }
}
