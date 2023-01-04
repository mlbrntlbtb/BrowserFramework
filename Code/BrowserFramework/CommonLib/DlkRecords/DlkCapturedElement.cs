using OpenQA.Selenium;

namespace CommonLib.DlkRecords
{
    public class DlkCapturedElement
    {
        #region CONSTRUCTOR
        public DlkCapturedElement(dynamic[] array)
        {
            /*
            position - value
            * 0 - web element that we clicked (the source)
            * 1 - the text of the source
            * 2 - action type that the user executed
            * 3 - ID of the source
            * 4 - CLASS of the source
            * 5 - TAG NAME of the source
            * 6 - 1st web element parent of the source (the parent of the source)
            * 7 - ID of the 1st web element parent
            * 8 - CLASS of the 1st web element parent
            * 9 - 2nd web element parent of the source (the parent of the parent of the source)
            * 10 - ID  of the 2nd web element parent
            * 11 - CLASS of the 2nd web element parent
            * 12 - 3rd web element parent of the source (the parent of the parent of the parent of the source)
            * 13 - ID of the 3rd web element parent. this is almost always the <div id="0"> element where the id changes based on how many subforms are open
            * 14 - CLASS of the 3rd web elementparent
            * 15 - FORM IDENTIFIER ELEMENT (the popRef element)
            * 16 - ID of the popRef element
            * 17 - CLASS of the popRef element
            * 18 - Possible SCREEN value (first element in DOM with class 'app')
            * 19 - Ancestor Form Index
            */

            WebElement = (IWebElement) array[0];
            ElementText = array[1] ?? string.Empty;
            ActionType = array[2];
            ElementId = array[3];
            ElementClass = array[4];
            ElementTagName = array[5];;
            Parent1 = (IWebElement)array[6];
            Parent1Id = array[7];
            Parent1Class = array[8];
            Parent2 = (IWebElement)array[9];
            Parent2Id = array[10];
            Parent2Class = array[11];
            Parent3 = (IWebElement)array[12];
            Parent3Id = array[13];
            Parent3Class = array[14];
            PopRef = array[15];
            PopRefId = array[16];
            PopRefClass = array[17];
            InferredScreen = array[18];
            AncestorFormIndex = array[19];
        }
        #endregion
        
        #region PROPERTIES
        public IWebElement WebElement { get; set; }
        public string ElementText { get; set; }
        public string ActionType { get; set; }
        public string ElementId { get; set; }
        public string ElementClass { get; set; }
        public string ElementTagName { get; set; }
        public IWebElement Parent1 { get; set; }
        public string Parent1Id { get; set; }
        public string Parent1Class { get; set; }
        public IWebElement Parent2 { get; set; }
        public string Parent2Id { get; set; }
        public string Parent2Class { get; set; }
        public IWebElement Parent3 { get; set; }
        public string Parent3Id { get; set; }
        public string Parent3Class { get; set; }
        public IWebElement PopRef { get; set; }
        public string PopRefId { get; set; }
        public string PopRefClass { get; set; }
        public string InferredScreen { get; set; }
        public string AncestorFormIndex { get; set; }
        #endregion
    }
}
