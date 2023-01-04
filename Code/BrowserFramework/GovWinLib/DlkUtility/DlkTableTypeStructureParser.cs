using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace GovWinLib.DlkUtility
{
    public static class DlkTableTypeStructureParser
    {
        public static string ParseFormTableStructure(IWebElement tableElement)
        {
            //for form table structure we detect tbody that contains rows.
            //and each row contains th and td element
            /*
             * <tbody>
             *   <tr>
             *     <th>
             *     <td>
             *   </tr>
             *   .
             *   .
             * </tbody>
             * 
             */
            IList<IWebElement> rows = tableElement.FindElements(By.XPath(@"./tbody/tr"));

            var processedRows = rows.Where(row => row.FindElement(By.XPath(@"./td")) != null && row.FindElement(By.XPath(@"./th")) != null);

            if (rows.Count() == processedRows.Count())
                return "formtable";
            else
                return "";
        }
    }
}
