using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestRunner.Common
{
    static class DlkExtensionMethodsUI
    {
        /// <summary>
        /// Extension method for getting TreeViewItem for the selected item in a TreeView
        /// </summary>
        /// <param name="root">Generates the user interface on behalf of its host, such as ItemsControl</param>
        /// <param name="item">Selected Item from the TreeViewItem</param>
        /// <returns>Returns the selected TreeViewItem from the TreeView</returns>
        public static TreeViewItem ContainerFromItemRecursive(this ItemContainerGenerator root, object item)
        {
            var treeViewItem = root.ContainerFromItem(item) as TreeViewItem;
            if (treeViewItem != null)
                return treeViewItem;
            foreach (var subItem in root.Items)
            {
                treeViewItem = root.ContainerFromItem(subItem) as TreeViewItem;
                var search = treeViewItem?.ItemContainerGenerator.ContainerFromItemRecursive(item);
                if (search != null)
                    return search;
            }
            return null;
        }
    }
}
