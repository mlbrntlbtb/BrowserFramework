using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;


using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("ResultItem")]
    public class DlkResultItem : DlkBaseControl
    {
        private String mstrTitleXpath = "./div[1]/h4";

        public DlkResultItem(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkResultItem(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkResultItem(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkResultItem(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        public void ClickItem()
        {
            Initialize();
            DlkLink Title = new DlkLink("Title", this, "XPATH", mstrTitleXpath + "//a");
            Title.Click();
        }

        public String GetItemTitle()
        {
            DlkLabel Title = new DlkLabel("Title", this, "XPATH", mstrTitleXpath);
            return TrimValue(Title.GetValue());
        }

        public String GetItemDetail(String sDetailCaption)
        {
            //DlkBaseControlItemDetail = new DlkBaseControl("ItemDetail", this,"XPATH", ".//span[contains(text(), '" + sDetailCaption + "')]/..");
            IWebElement ItemDetail = mElement.FindElement(By.XPath(".//span[contains(text(), '" + sDetailCaption + "')]/.."));
            if (ItemDetail.GetAttribute("class") == "markOpp")
            {
                DlkMark ItemMark = new DlkMark("ItemMark", ItemDetail);
                return ItemMark.GetValue();
            }
            else
            {
                return ItemDetail.Text.Replace(ItemDetail.Text.Split(':').First() + ":", "").Trim();
                //return ItemDetailLine[1].Trim();
            }
        }

        public IWebElement GetItemDetailLink(String sDetailCaption)
        {
            //DlkBaseControlItemDetail = new DlkBaseControl("ItemDetail", this,"XPATH", ".//span[contains(text(), '" + sDetailCaption + "')]/..");
            IWebElement ItemDetail = mElement.FindElement(By.XPath(".//span[contains(text(), '" + sDetailCaption + "')]/.."));
            return ItemDetail.FindElement(By.XPath(".//a"));
        }

        public String GetItemDetailTitle(String sDetailCaption)
        {
            IWebElement ItemDetail = DlkEnvironment.AutoDriver.FindElement(By.XPath(".//li//span[contains(text(), '" + sDetailCaption + "')]/.."));
            String[] ItemDetailLine = ItemDetail.Text.Split(':');
            return ItemDetailLine[0].Trim();
        }

        public void MarkItem(String sValue)
        {
            //DlkMark testItemMark = new DlkMark("testItemMark", this, "XPATH", ".//li[@class='markOpp']");
            IWebElement rawItemMark = DlkEnvironment.AutoDriver.FindElement(By.XPath(".//li[@class='markOpp']//a"));
            rawItemMark.Click();
            DlkMark ItemMark = new DlkMark("ItemMark", "XPATH", "//li[@class='markOpp']//span/parent::div");
            ItemMark.Select(sValue);

        }

        public DlkMark GetMarkItem()
        {
            //DlkMark testItemMark = new DlkMark("testItemMark", this, "XPATH", ".//li[@class='markOpp']");
            IWebElement rawItemMark = DlkEnvironment.AutoDriver.FindElement(By.XPath(".//li[@class='markOpp']//a"));
            rawItemMark.Click();
            DlkMark ItemMark = new DlkMark("ItemMark", "XPATH", "//li[@class='markOpp']//span/parent::div");
            return ItemMark;
        }

        private String TrimValue(String sValue)
        {
            sValue = sValue.Replace("\n", " ");
            sValue = sValue.Replace("\r", " ");
            while (sValue.Contains("  "))
            {
                sValue = sValue.Replace("  ", " ");
            }
            return sValue;
        }
    }
}

