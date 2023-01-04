using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCLib.DlkControls
{
    [ControlType("Grid")]
    public class DlkGrid : DlkBaseControl
    {
        public DlkGrid(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize() => FindElement();

        [Keyword("VerifyExists")]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCell")]
        public void ClickCell(String RowNumber, String ColumnNumber)
        {
            try
            {
                int row, col;

                if (!int.TryParse(RowNumber, out row)) throw new Exception($"Invalid Row ({row}).");
                if (!int.TryParse(ColumnNumber, out col)) throw new Exception($"Invalid Column ({col}).");

                if(row < 1) throw new Exception($"Row ({row}) must be greater than or equal to 1.");
                if(col < 1) throw new Exception($"Column ({col}) must be greater than or equal to 1.");

                Initialize();

                var cell = mElement.FindElements(By.XPath($"./tr[{row}]/td[{col}]")).FirstOrDefault();
                if(cell == null) throw new Exception($"Cannot find cell with row of ({row}) and column of ({col}).");

                cell.Click();

                DlkLogger.LogInfo("ClickCell() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCell() failed : " + e.Message, e);
            }
        }
    }
}
