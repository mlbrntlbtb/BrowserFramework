using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    public enum MarkState {Active, Inactive};

    public class Mark
    {
        private String value;
        private String markType;
        private IWebElement markElement;

        public Mark(String pstrMarkType, String Value, IWebElement pobjMarkElement)
        {
            markType = pstrMarkType;
            value = Value;
            markElement = pobjMarkElement;
        }

        public void Click()
        {
            
            DlkBaseControl mark = new DlkBaseControl("Mark" + value, markElement);
            String onclickEvent = mark.GetAttributeValue("onclick");
            if (onclickEvent != null && onclickEvent != "")
            {
                DlkEnvironment.CaptureUrl();
            }
            mark.Click();
            if (onclickEvent != null && onclickEvent != "")
            {
                DlkEnvironment.WaitUrlUpdate();
            }
        }

        public MarkState GetState()
        {
            DlkBaseControl objMark;
            String strSrc;
            switch (markType)
            {
                case "markwidget":
                    IWebElement markItem = markElement.FindElement(By.XPath("./img"));
                    //objMark = new DlkBaseControl("Mark" + value, markElement, "img");
                    objMark = new DlkBaseControl("Mark" + value, markItem);
                    strSrc = objMark.GetAttributeValue("src");
                    switch(value)
                    {
                        case "0":
                            if(strSrc.Contains("/neo/static/Qtib6DrVGuLmzNFHN5nHgQcwSo3nn7W7k2wPDfheYsU.gif"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("/neo/static/4SCXHOh22R3qABs1Xk990s5ePKGOt8KLQvFiR604xZd.gif"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            if (strSrc.Contains("/static/pITUkARKCwhq6tVTD8ppiDy9apFpGmlwlCOeIlb1deL.gif"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("/static/qrHWtkrNA2o2DyFDEZ7PrPvRusxMjGZFKoevoUapJPV.gif"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "hide":
                            if (strSrc.Contains("/neo/static/kqLJpzb5qSs3etdyagkOuFl2eGpVKIlIevAbcxqJQZc.gif"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("/neo/static/ywgNuSH849Fsi2PZwVrEPL2EKF1amuJ2Ldeafyzbru9.gif"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        default:
                            throw new Exception("GetState() failed. Mark '" + value + "' is not valid.");
                            throw new Exception("GetState() Failed.");
                    }
                case "markinglevelscontainer":
                    objMark = new DlkBaseControl("Mark" + value, markElement);
                    strSrc = objMark.GetAttributeValue("class");
                    switch (value)
                    {
                        case "0":
                            if (strSrc.Contains("markActive"))
                            {
                                return MarkState.Active;
                            }
                            else if (strSrc.Contains("not-marked"))
                            {
                                return MarkState.Inactive;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            if (strSrc.Contains("markActive"))
                            {
                                return MarkState.Active;
                            }
                            else if (strSrc.Equals("mark"))
                            {
                                return MarkState.Inactive;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "hide":
                            if (strSrc.Contains("endCap hide"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("markActive"))
                            {
                                //new advance search control does not show active state 
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                            }
                        default:
                            throw new Exception("GetState() failed. Mark '" + value + "' is not valid.");
                    }
                case "markopp":
                    objMark = new DlkBaseControl("Mark" + value, markElement);
                    strSrc = objMark.GetAttributeValue("src");
                    switch (value)
                    {
                        case "0":
                            if (strSrc.Contains("KGivP0QLNkpavGVQkttJvT0q9_Ql6XOO2Jxpdlz1T7pbdXP4JqtGISAGAc0kGCTcazA9OsofT5Q66aJcVPV3Y28zWWNSv5d6YndCz3iTXaSoZQ-DxQhya6FyOYcI0P_uUVY9eGxyNRAMkLUy_6f9KAOi-9RTU5Uf7IRxM2K5sXrePkfq0"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("J-hhyYU2ZHnb-UC50OwX4z9qG7mcCDaH8rFlU0KZ0MKmBDLXw4byHoTwvEGihlGJXhefhKz2ikcILawyKMWqqSwuFQpGjefBls0Yo_w0mYSEmG-jfNFn0wFrBqZtYMoP7cEfCpT2nBTdRqPAT4uJ4XWb0GoYV8zCzKqbL8p78_4jb4uU0"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            if (strSrc.Contains("KZESTM-0lvlMZMzS45hjhKD8yeVO1CCAzlxlRH_IaeOQJ8A1eNcgdOokzX2yzLOZsQC4s5wHXa346zyQt1j-w2Qu7R_lYPWTngHCrCB9IY8qOctNt8buwz4JMQJteG8rxHLBVeLEaf7P7mC7bLEqDK6U5-uxLhUXl2H8B6dDjt_GGKV00") || strSrc.Contains("OFF"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("c") || strSrc.Contains("/CES7/WebResource.axd?d=zGnNRVS3sN1ldfxoP302x_s2CmHBC_76xtJoF7I18LGOQ80NLK4c9B8GuxHWQWr58Pbh8EpCsGqnuCtMNVFOFFhxLA0oCHUydgaGxTyqySUyHIFPK4VnQfR8Ty4EmsSeFWQu9PlLke_neAEv925GyDZSKzZyKgqxAgvbAI4lNEX4gtEA0&t=635793059590522586"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "hide":
                            if (strSrc.Contains(@"tO6ZIb5paq1MO5Zmr8Qeo_DMAICYULnflwRWUQ5o3GjrCx6D5kV1pc6OweQqv4MjBA_qz2D0h6ZZyRaB1bnpoHgEVBiPe6xNgLWH82CkYxnax3vQ_31_eQbizB0Y0VU7oGfds0IGPI7OZacIkQJMS8ssS9f-YoxmAeEeJWpE6gK9AhnL0"))

                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("FPIdtoZCqaGufnON1WbJJIVs5ZnaTXSSV6V4OTpah2SonYvsrWpplln4iw20EvqpcezpJPpwDa6w-qcCaguIpBcfQ3Zimlf2YBmZjslOjvzUd_C1t3H_la6i_MOoSPYSX_ppCydc0snrGOZg4zQJyQN9ndCelMbVknrDIGygqDomn3tj0"))
                            {
                                //new advance search control does not show active state 
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                            }
                        default:
                            throw new Exception("GetState() failed. Mark '" + value + "' is not valid.");
                    }
                case "marktable":
                    objMark = new DlkBaseControl("Mark" + value, markElement);
                    strSrc = objMark.GetAttributeValue("src");
                    switch (value)
                    {
                        case "0":
                            if (strSrc.Contains("unmarked_inactive.gif"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("unmarked_active.gif"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            if (strSrc.Contains("marked_inactive.gif"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("marked_active.gif"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }
                        case "hide":
                            if (strSrc.Contains("hide_inactive.gif"))
                            {
                                return MarkState.Inactive;
                            }
                            else if (strSrc.Contains("hide_active.gif"))
                            {
                                return MarkState.Active;
                            }
                            else
                            {
                                throw new Exception("GetState() failed. Mark '" + value + "'. Unknown src state. '" + strSrc + "'");
                                throw new Exception("GetState() Failed.");
                            }                            
                        default:
                            throw new Exception("GetState() failed. Mark '" + value + "' is not valid.");
                            throw new Exception("GetState() Failed.");
                    }
                default:
                    throw new Exception("GetState() failed. Mark control of type '" + markType + "' is not supported.");
                    throw new Exception("GetState() Failed.");

            }
        }
    }

    public enum MarkPrioroity
    {
        hide = -1,
        Unamrked = 0,
        Low,
        LowMedium,
        Medium,
        MediumHigh,
        High       
    }

    public class MarkContainer
    {
        private String mstrMarkType = "";
        private IWebElement mMarkContainerElement;
        private Dictionary<String, Mark> mdictMarkItems;

        public MarkContainer(String pstrMarkType, IWebElement pobjMarkContainerElement)
        {
            mstrMarkType = pstrMarkType;
            mMarkContainerElement = pobjMarkContainerElement;
            mdictMarkItems = new Dictionary<String, Mark>();
            switch (mstrMarkType)
            {
                case "markwidget":
                    mdictMarkItems.Add("0", new Mark(mstrMarkType, "0", mMarkContainerElement.FindElement(By.Id("not-marked"))));
                    mdictMarkItems.Add("1", new Mark(mstrMarkType, "1", mMarkContainerElement.FindElement(By.Id("mark1"))));
                    mdictMarkItems.Add("2", new Mark(mstrMarkType, "2", mMarkContainerElement.FindElement(By.Id("mark2"))));
                    mdictMarkItems.Add("3", new Mark(mstrMarkType, "3", mMarkContainerElement.FindElement(By.Id("mark3"))));
                    mdictMarkItems.Add("4", new Mark(mstrMarkType, "4", mMarkContainerElement.FindElement(By.Id("mark4"))));
                    mdictMarkItems.Add("5", new Mark(mstrMarkType, "5", mMarkContainerElement.FindElement(By.Id("mark5"))));
                    {
                        IWebElement hideElement = null;
                        try
                        {
                            hideElement = mMarkContainerElement.FindElement(By.Id("hide"));
                            if (hideElement is IWebElement)
                            {
                                mdictMarkItems.Add("hide", new Mark(mstrMarkType, "hide", hideElement));
                            }
                            else
                            {
                                //mdictMarkItems.Add("hide", new Mark(mstrMarkType, "", null));
                            }
                        }
                        catch
                        {
                        }
                    }
                    break;

                case "markinglevelscontainer":
                    mdictMarkItems.Add("0", new Mark(mstrMarkType, "0", mMarkContainerElement.FindElement(By.XPath(".//a[@data-level=100]"))));
                    mdictMarkItems.Add("1", new Mark(mstrMarkType, "1", mMarkContainerElement.FindElement(By.XPath(".//a[@data-level=1]"))));
                    mdictMarkItems.Add("2", new Mark(mstrMarkType, "2", mMarkContainerElement.FindElement(By.XPath(".//a[@data-level=2]"))));
                    mdictMarkItems.Add("3", new Mark(mstrMarkType, "3", mMarkContainerElement.FindElement(By.XPath(".//a[@data-level=3]"))));
                    mdictMarkItems.Add("4", new Mark(mstrMarkType, "4", mMarkContainerElement.FindElement(By.XPath(".//a[@data-level=4]"))));
                    mdictMarkItems.Add("5", new Mark(mstrMarkType, "5", mMarkContainerElement.FindElement(By.XPath(".//a[@data-level=5]"))));
                    if (mMarkContainerElement.FindElements(By.XPath(".//a")).Count == 7)
                    {
                        mdictMarkItems.Add("hide", new Mark(mstrMarkType, "hide", mMarkContainerElement.FindElement(By.XPath(".//a[@data-level=0]"))));
                    }
                        
                    break;

                case "markopp":

                    mdictMarkItems.Add("0", new Mark(mstrMarkType, "0", mMarkContainerElement.FindElement(By.Id("0"))));
                    mdictMarkItems.Add("1", new Mark(mstrMarkType, "1", mMarkContainerElement.FindElement(By.Id("1"))));
                    mdictMarkItems.Add("2", new Mark(mstrMarkType, "2", mMarkContainerElement.FindElement(By.Id("2"))));
                    mdictMarkItems.Add("3", new Mark(mstrMarkType, "3", mMarkContainerElement.FindElement(By.Id("3"))));
                    mdictMarkItems.Add("4", new Mark(mstrMarkType, "4", mMarkContainerElement.FindElement(By.Id("4"))));
                    mdictMarkItems.Add("5", new Mark(mstrMarkType, "5", mMarkContainerElement.FindElement(By.Id("5"))));

                    if (mMarkContainerElement.FindElements(By.XPath(".//img")).Count == 7)
                    {
                        mdictMarkItems.Add("hide", new Mark(mstrMarkType, "hide", mMarkContainerElement.FindElement(By.Id("6"))));
                    }
                    break;

                case "marktable":
                    mdictMarkItems.Add("0", new Mark(mstrMarkType, "0", mMarkContainerElement.FindElement(By.Name("Rank0"))));
                    mdictMarkItems.Add("1", new Mark(mstrMarkType, "1", mMarkContainerElement.FindElement(By.Name("Rank1"))));
                    mdictMarkItems.Add("2", new Mark(mstrMarkType, "2", mMarkContainerElement.FindElement(By.Name("Rank2"))));
                    mdictMarkItems.Add("3", new Mark(mstrMarkType, "3", mMarkContainerElement.FindElement(By.Name("Rank3"))));
                    mdictMarkItems.Add("4", new Mark(mstrMarkType, "4", mMarkContainerElement.FindElement(By.Name("Rank4"))));
                    mdictMarkItems.Add("5", new Mark(mstrMarkType, "5", mMarkContainerElement.FindElement(By.Name("Rank5"))));
                    mdictMarkItems.Add("hide", new Mark(mstrMarkType, "hide", mMarkContainerElement.FindElement(By.Name("Rank6"))));
                    break;
            }
        }

        public String GetValue()
        {
            return GetCurrentPriorityValue();
        }

        public bool IsHidden()
        {
            string hide = Convert.ToString(MarkPrioroity.hide);

            if (mdictMarkItems.ContainsKey(hide))
            {
                if (mdictMarkItems[hide].GetState() == MarkState.Active)
                    return true;
            }

            return false;
        }

        public void Select(String pstrValue)
        {
            if (mdictMarkItems.ContainsKey(pstrValue))
            {
                if (pstrValue.ToLower() == "hide")
                {
                    mdictMarkItems["hide"].Click();
                }
                else if (GetCurrentPriorityValue() != pstrValue)
                {
                    mdictMarkItems[pstrValue].Click();
                }
            }
            else
            {
                throw new Exception("Select() failed. Value '" + pstrValue + "' is not valid.");
            }
        }

        public string GetCurrentPriorityValue()
        {
            string high = Convert.ToString((int)MarkPrioroity.High);
            string medHigh = Convert.ToString((int)MarkPrioroity.MediumHigh);
            string med = Convert.ToString((int)MarkPrioroity.Medium);
            string lowMed = Convert.ToString((int)MarkPrioroity.LowMedium);
            string low = Convert.ToString((int)MarkPrioroity.Low);
            string unmarked = Convert.ToString((int)MarkPrioroity.Unamrked);


            if (mdictMarkItems.ContainsKey(high))
            {
                if(mdictMarkItems[high].GetState() == MarkState.Active)
                    return high;
            }
            
            if (mdictMarkItems.ContainsKey(medHigh))
            {
                if(mdictMarkItems[medHigh].GetState() == MarkState.Active)
                    return medHigh;
            }

            if (mdictMarkItems.ContainsKey(med))
            {
                if (mdictMarkItems[med].GetState() == MarkState.Active)
                    return med;
            }

            if (mdictMarkItems.ContainsKey(lowMed))
            {
                if (mdictMarkItems[lowMed].GetState() == MarkState.Active)
                    return lowMed;
            }

            if (mdictMarkItems.ContainsKey(low))
            {
                if (mdictMarkItems[low].GetState() == MarkState.Active)
                    return low;
            }

            if (mdictMarkItems.ContainsKey(unmarked))
            {                
                    return unmarked;
            }

            return "";             
        }       
    }

    [ControlType("Mark")]
    public class DlkMark : DlkBaseControl
    {
        private MarkContainer mobjMarkContainer;
        private IWebElement marker;

        public DlkMark(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMark(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMark(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkMark(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            
            this.PerformAction(() =>
                {
                    FindElement();
                    String markClass = GetAttributeValue("class").ToLower();
                    if (GetAttributeValue("nodeName").ToLower() == "div")
                    {
                        if (GetAttributeValue("class").ToLower().Contains("markwidget"))
                        {
                            mElement.Click();
                            String searchValue = ".//span";
                            marker = mElement.FindElement(By.XPath(searchValue));                            
                            markClass = "divMarkWidget";
                        }
                    }
                    else if (GetAttributeValue("nodeName").ToLower() == "span")
                    {
                        markClass = "markWidget";

                        if(GetAttributeValue("class").ToLower().Contains("markingwidgetlevelselect"))
                            markClass = "markinglevelscontainer";
                    }
                    else if (GetAttributeValue("nodeName").ToLower() == "td")
                    {
                        markClass = "markTable";
                    }
                    else
                    {
                        markClass = GetAttributeValue("class");
                    }
                    switch (markClass)
                    {
                        case "markWidget":
                            mobjMarkContainer = new MarkContainer("markwidget", mElement);
                            break;
                        case "markOpp":
                            mobjMarkContainer = new MarkContainer("markopp", mElement);
                            break;
                        case "markTable":
                            mobjMarkContainer = new MarkContainer("marktable", mElement);
                            break;
                        case "divMarkWidget":
                            mobjMarkContainer = new MarkContainer("markwidget", marker);
                            break;
                        case "markinglevelscontainer":
                            mobjMarkContainer = new MarkContainer("markinglevelscontainer", mElement);
                            break;
                        default:
                            Exception exceptionMessage = new Exception("DlkMark Initialize() failed. Mark control of type '" + markClass + "' is not supported.");
                            throw exceptionMessage;
                            //throw new Exception(exceptionMessage.Message);
                    }
                }, new String[] { "", "" });                
        }

        public new String GetValue()
        {
            Initialize();
            return mobjMarkContainer.GetValue();
        }

        [RetryKeyword("Select", new String[] { "1|text|Mark Value (0,1,2,3,4,5,hide)|1" })]
        public void Select(String MarkValue)
        {
            String Value = MarkValue;

            this.PerformAction(() =>
                {
                    Initialize();                    
                    mobjMarkContainer.Select(Value);
                }, new String[]{"retry"});
        }

        #region Verify methods
        [RetryKeyword("VerifyValue", new String[] { "1|text|Expected Mark Value (0,1,2,3,4,5)|1" })]
        public void VerifyValue(String ExpectedMarkValue)
        {
            String expectedValue = ExpectedMarkValue;

            this.PerformAction(() =>
            {
                Initialize();

                DlkAssert.AssertEqual("VerifyValue()", expectedValue, mobjMarkContainer.GetValue());
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyIfHidden", new String[] { "1|text|Expected Mark Value (TRUE if hidden)|TRUE" })]
        public void VerifyIfHidden(String ExpectedMarkValue)
        {
            String expectedValue = ExpectedMarkValue;

            this.PerformAction(() =>
            {
                Initialize();

                DlkAssert.AssertEqual("VerifyIfHidden()", Convert.ToBoolean(expectedValue), mobjMarkContainer.IsHidden());
            }, new String[]{"retry"});
        }
        #endregion
    }
}
