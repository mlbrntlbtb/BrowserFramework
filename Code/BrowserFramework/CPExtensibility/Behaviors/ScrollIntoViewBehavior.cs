using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CPExtensibility.Behaviors
{
    /// <summary>
    /// Behaviors are attached to a UI Element's event.
    /// </summary>
    public class ScrollIntoViewBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += DataGrid_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SelectionChanged -= DataGrid_SelectionChanged;
        }

        void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssociatedObject.SelectedItem != null)
                AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
            else if (AssociatedObject.SelectedItems != null && AssociatedObject.SelectedItems.Count > 0)
                AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItems[0]);
        }
    }
}
