using CommonLib.DlkControls;
using System;

namespace CommonLib.DlkRecords
{
    public class DlkCapturedStep
    {
        #region CONSTRUCTOR
        public DlkCapturedStep(DlkBaseControl control, String elementTxt, String actionType, String elementId, String elementClass, 
            String elementTagName, String popRefId, String inferredScreen, string ancestorFormIndex)
        {
            WebElement = control;
            ElementTxt = elementTxt;
            ActionType = actionType;
            ElementId = elementId;
            ElementClass = elementClass;
            ElementTagName = elementTagName;
            PopRefId = popRefId;
            InferredScreen = inferredScreen;
            AncestorFormIndex = ancestorFormIndex;
        }
        #endregion

        #region PROPERTIES
        public DlkBaseControl WebElement { get; set; }
        public String ElementTxt { get; set; }
        public String ActionType { get; set; }
        public String ElementId { get; set; }
        public String ElementClass { get; set; }
        public String ElementTagName { get; set; }
        public String PopRefId { get; set; }
        public String InferredScreen { get; set; }
        public String AncestorFormIndex { get; set; }
        #endregion

    }
}
