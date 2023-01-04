using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTLib.DlkControls.Contract
{
    public interface IGrid
    {
        IWebElement GetCellElement(string columnHeaderName, string rowNumber);
        IWebElement GetCellElementByContent(string columnHeaderName, string cellContent);
        int GetRowCount();
        void SelectRow(string rowNumber);
        List<IWebElement> GetGridHeaders();
        List<IWebElement> GetGridValues();
        int GetCellRowNumber(string columnHeaderName, string cellContent);
        IWebElement GetHeaderElement(string columnHeaderName, IWebElement element);
        List<IWebElement> GetSortedColumnCount(IWebElement element, bool isAscending);
        IList<List<IWebElement>> GetRows();
        string ActionRowsXpath { get; }
        string ModifyButtonXpath { get; }
        void HoverOnColumnHeader(string columnHeaderName);
        string ColumnMenuXpath { get; }
        string GetGridCellText(IWebElement element);
    }
}
