using CommonLib.DlkSystem;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;
using TestRunner.Common;

namespace TestRunner
{
    public class XmlViewerEventArgs : RoutedEventArgs
    {
        public XmlViewerEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        { }
    }


    /// <summary>
    /// Interaction logic for XmlViewer.xaml
    /// </summary>
    public partial class XmlViewer : UserControl
    {
        public String mSelectedItemXPath = "";

        public static readonly RoutedEvent SelectedItemChangedEvent =
            EventManager.RegisterRoutedEvent(
                "SelectedItemChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(XmlViewer));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }


        private XmlDocument _xmldocument;
        public XmlDocument xmlDocument
        {
            get { return _xmldocument; }
            set
            {
                _xmldocument = value;
                //BindXMLDocument();                
                xmlDataProvider.Document = _xmldocument;
                OnPropertyChanged("xmlDataProvider");
            }
        }

        private XmlDataProvider _xmlDataProvider;
        public XmlDataProvider xmlDataProvider
        {
            get { return _xmlDataProvider; }
            set
            {
                _xmlDataProvider = value;
                OnPropertyChanged("xmlDataProvider");
            }
        }

        public XmlViewer()
        {
            InitializeComponent();
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.Document = _xmldocument;
            xmlTree.DataContext = xmlDataProvider;
        }

        public void SelectNode()
        {
            TreeViewItem root = (TreeViewItem)(xmlTree.ItemContainerGenerator.ContainerFromIndex(0));
            TraverseNode(root);
        }

        private void TraverseNode(TreeViewItem treeViewItem)
        {
            if (treeViewItem == null)
            {
                return;
            }
            if (treeViewItem.Header.GetType() != typeof(XmlElement))
            {
                return;
            }

            if (((XmlElement)treeViewItem.Header).GetAttribute("dlkselected") == "true")
            {
                treeViewItem.Focus();
                return;
            }

            if (treeViewItem.HasItems)
            {
                for (int i = 0; i < treeViewItem.ItemContainerGenerator.Items.Count; i++)
                {
                    TreeViewItem item = (TreeViewItem)treeViewItem.ItemContainerGenerator.ContainerFromIndex(i);
                    TraverseNode(item);
                }
            }
        }

        private void BindXMLDocument()
        {
            xmlTree.ItemsSource = null;
 
            XmlDataProvider provider = new XmlDataProvider();
            provider.Document = _xmldocument;
            Binding binding = new Binding();
            binding.Source = provider;
            binding.XPath = "child::node()";
            xmlTree.SetBinding(TreeView.ItemsSourceProperty, binding);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void xmlTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                TreeViewItem selectedItem = xmlTree.Tag as TreeViewItem;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void xmlTree_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                xmlTree.Tag = e.OriginalSource;
                TreeViewItem selected = e.OriginalSource as TreeViewItem;
                if(selected != null)
                {
                    selected.BringIntoView();
                    selected.Focus();
                }

                mSelectedItemXPath = FindXPath((XmlNode)xmlTree.SelectedItem);
                RaiseEvent(new XmlViewerEventArgs(XmlViewer.SelectedItemChangedEvent, sender));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        public string FindXPath(XmlNode node)
        {
            StringBuilder builder = new StringBuilder();
            while (node != null)
            {
                switch (node.NodeType)
                {
                    case XmlNodeType.Attribute:
                        builder.Insert(0, "/@" + node.Name);
                        node = ((XmlAttribute)node).OwnerElement;
                        break;
                    case XmlNodeType.Element:
                        int index = FindElementIndex((XmlElement)node);
                        builder.Insert(0, "/" + node.Name + "[" + index + "]");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.Document:
                        return builder.ToString();
                    default:
                        throw new ArgumentException("Only elements and attributes are supported");
                }
            }
            throw new ArgumentException("Node was not in a document");
        }

        public int FindElementIndex(XmlElement element)
        {
            XmlNode parentNode = element.ParentNode;
            if (parentNode is XmlDocument)
            {
                return 1;
            }
            XmlElement parent = (XmlElement)parentNode;
            int index = 1;
            foreach (XmlNode candidate in parent.ChildNodes)
            {
                if (candidate is XmlElement && candidate.Name == element.Name)
                {
                    if (candidate == element)
                    {
                        return index;
                    }
                    index++;
                }
            }
            throw new ArgumentException("Couldn't find element within parent");
        }
    }
}
