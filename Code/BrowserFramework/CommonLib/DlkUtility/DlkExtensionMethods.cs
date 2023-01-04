using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;

namespace CommonLib.DlkUtility
{
    /// <summary>
    /// Add your extension methods here.
    /// </summary>
    public static class DlkExtensionMethods
    {
        /// <summary>
        /// ObservableCollection.Sort()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection">Your ObservableCollection</param>
        /// <param name="sort">The lambda expression to compare</param>
        public static void Sort<T, TKey>(this ObservableCollection<T> collection, Func<ObservableCollection<T>, TKey> sort)
        {
            /* example : this will sort the observablecollection by date
             new ObservableCollection<T>.Sort(observableCollection => observableCollection.OrderByDescending(item => DateTime.ParseExact(item.ExecutionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)));
             */
            try
            {
                var sorted = (sort.Invoke(collection) as IOrderedEnumerable<T>).ToArray();
                for (int i = 0; i < sorted.Count(); i++)
                    collection.Move(collection.IndexOf(sorted[i]), i);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable ToDataTable<T>(this List<T> items, string[] PropsToAdd)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (PropsToAdd.Contains(prop.Name))
                {
                    tb.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            foreach (var item in items)
            {
                var values = new object[PropsToAdd.Length];
                //for (var i = 0; i < props.Length; i++)
                //{
                //    if (PropsToAdd.Contains(props[i].Name))
                //        values[i] = props[i].GetValue(item, null);
                //}

                int count = 0;
                foreach (var prop in props)
                {
                    if (PropsToAdd.Contains(prop.Name))
                    {
                        values[count++] = prop.GetValue(item, null);
                        //DataRow dr = tb.NewRow();
                        //dr[count++] = prop.GetValue(item, null);
                        //tb.Rows.Add(dr);
                    }

                }
                tb.Rows.Add(values);
            }

            return tb;
        }
        /// <summary>
        /// Returns the first element with result excluding elements with property Display is equals to false.
        /// </summary>
        /// <param name="element">Element where the driver will search through.</param>
        /// <param name="byList">List of search parameters.</param>
        /// <returns>IWebElement</returns>
        public static IWebElement FindWebElementCoalesce(this IWebElement element, params By[] byList)
        {

            var myElementReturn = element.FindWebElementsCoalesce(false, byList);
            if (myElementReturn != null && myElementReturn.Count > 0)
                return myElementReturn[0];

            return null;
        }
        /// <summary>
        /// Returns the first list of elements with result.
        /// </summary>
        /// <param name="element">Element where the driver will search through.</param>
        /// <param name="includeInvisibleElements">Include elements that is not displayed</param>
        /// <param name="byList">List of search parameters.</param>
        /// <returns>List of IWebElement</returns>
        public static IList<IWebElement> FindWebElementsCoalesce(this IWebElement element, bool includeInvisibleElements, params By[] byList)
        {
            foreach (var by in byList)
            {
                var myElemenstReturn = element.FindElements(by);
                if (myElemenstReturn.Count > 0)
                {
                    if (includeInvisibleElements)
                        return myElemenstReturn;
                    else if (myElemenstReturn.Any(row => row.Displayed))
                        return myElemenstReturn.Where(row => row.Displayed).ToList();
                }
            }
            return null;
        }

        public static IWebElement FindElement(this IWebElement Element, By SearchMethod, int Timeout)
        {
            IWebElement ret = null;

            try
            {
                do
                {
                    ret = Element.FindElements(SearchMethod).FirstOrDefault();
                    if (ret != null)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                while (Timeout-- > 0);
            }
            catch
            {
                // Do nothing. Return null
            }

            return ret;
        }

        public static IWebElement FindElement(this IWebDriver driver, params By[] byList)
        {
            IWebElement hit = null;
            foreach (var by in byList)
            {
                hit = driver.FindElements(by).FirstOrDefault();
                if (hit != null)
                {
                    break;
                }
            }
            return hit;
        }

        public static void Swipe(this AppiumDriver<AppiumWebElement> driver, int originX, int originY, 
            int  destX, int destY, long wait)
        {
            var action = new TouchAction(driver);
            action.Press(originX, originY)
                .Wait(wait)
                .MoveTo(destX, destY)
                .Release()
                .Perform();
        }

        /// <summary>
        /// Retrieve all the inner exceptions given the Exception
        /// </summary>
        /// <param name="exception">The exception that which we will extract inner exception from</param>
        public static IEnumerable<Exception> GetInnerExceptions(this Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException();
            }

            var innerException = exception;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }
}
