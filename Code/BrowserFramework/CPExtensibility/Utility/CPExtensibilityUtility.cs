using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.DlkUtility;
using Recorder.Presenter;
using System.Collections.ObjectModel;
using System.Collections;
using System.Diagnostics;

namespace CPExtensibility.Utility
{
    /// <summary>
    /// This static class will contain all the utility methods to be able to support the functionality needed by the extensibility tool.
    /// The difference of this class with the Services is that this class SHOULD NOT contain utilities that involve business logic.
    /// Example: Parsing, Casting, etc.
    /// If we need to add utilities that is involved with the business logic, we add them in the services folder.
    /// </summary>
    public static class CPExtensibilityUtility
    {
        /// <summary>
        /// An extension method that returns an ObservableCollection after passing any type that implements IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T> (this IEnumerable<T> collection)
        {
            var observable = new ObservableCollection<T>();
            foreach (var item in collection)
            {
                observable.Add(item);
            }
            return observable;
        }

        public static void OpenUserGuidePDF()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\" + "Costpoint 711 Extensibility Mapping Tool User Guide.pdf";
            Process.Start(filepath);
        }
    }
}
