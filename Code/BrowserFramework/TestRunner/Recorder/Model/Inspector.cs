using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkRecords;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace Recorder.Model
{
    public abstract class Inspector
    {
        #region ABSTRACT METHODS
        /// <summary>
        /// Gets accessed control
        /// </summary>
        /// <param name="ActionType">Type of action</param>
        /// <param name="AlertText">Error message if exception is encountered</param>
        /// <param name="IsVerify">Checks if action is verified</param>
        /// <param name="IsHotkeyPressed">Checks if a hotkey is pressed</param>
        /// <returns>Returns control if found, throws an Alert Dialog otherwise</returns>
        public abstract DlkBaseControl GetAccessedControl(out string ActionType, out string AlertText, 
            out DlkCapturedStep CurrentStep, bool IsVerify, bool IsHotkeyPressed);

        /// <summary>
        /// Infers the target element
        /// </summary>
        /// <param name="Source">The element interacted</param>
        /// <returns>Returns the target element</returns>
        public abstract IWebElement InferTarget(IWebElement Source, string sourceClassName,
            out string newClass, ref string newId, IWebElement parent1 = null,
            string parent1Id = "", string parent1Class = "", IWebElement parent2 = null,
            string parent2Id = "", string parent2Class = "", IWebElement parent3 = null,
            string parent3Id = "", string parent3Class = "");

        /// <summary>
        /// Get app screen from metadata
        /// </summary>
        /// <param name="DescriptorValue">The value of the descriptor</param>
        /// <returns>Returns the name of the screen by which the control belongs to</returns>
        public abstract string GetScreen(string DescriptorValue);

        /// <summary>
        /// Gets the descriptor
        /// </summary>
        /// <param name="Source">The control where the descriptor must be extracted</param>
        /// <returns>Returns the descriptor of the control</returns>
        public abstract KeyValuePair<string, string> GetDescriptor(DlkBaseControl Source, string Id, string Class);

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Gets the control via Xpath
        /// </summary>
        /// <param name="xpath">Xpath location of the control</param>
        /// <param name="relativeControl">Name of the control relative to the one being located</param>
        /// <returns>Returns the control found in the given Xpath</returns>
        public DlkBaseControl GetControlViaXPath(string xpath, DlkBaseControl relativeControl = null)
        {
            DlkBaseControl ret = null;
            try
            {
                ret = relativeControl == null ? new DlkBaseControl("Control", DlkEnvironment.AutoDriver.FindElement(By.XPath(xpath)))
                    : new DlkBaseControl("Control", relativeControl.mElement.FindElement(By.XPath(xpath)));
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Gets the attribute value of a specified attribute in an element
        /// </summary>
        /// <param name="SearchType">Type of element</param>
        /// <param name="SearchString">ID of the element</param>
        /// <param name="Attribute">Name of attribute where value must be extracted</param>
        /// <returns>Returns the attribute value</returns>
        public string GetAttributeValue(string SearchType, string SearchString, string Attribute)
        {
            string ret = "";
            try
            {
                switch (SearchType.ToLower())
                {
                    case "id":
                        IWebElement elem = DlkEnvironment.AutoDriver.FindElement(By.Id(SearchString));
                        ret = elem.GetAttribute(Attribute);
                        break;
                }
            }
            catch
            {
                ret = "";
            }
            return ret;
        }
        #endregion
    }
}
