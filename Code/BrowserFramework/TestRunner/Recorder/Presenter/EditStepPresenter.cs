using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using Recorder.View;

namespace Recorder.Presenter
{
    /// <summary>
    /// Presenter logic for IEditStepView
    /// </summary>
    public class EditStepPresenter
    {   
        #region PRIVATE MEMBERS
        private IEditStepView mView;
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="View"></param>
        public EditStepPresenter(IEditStepView View)
        {
            mView = View;
            DlkAssemblyKeywordHandler.Initialize(DlkEnvironment.mLibrary);
        }

        /// <summary>
        /// Save step
        /// </summary>
        public void SaveStep()
        {
            string paramString = string.Empty;
            string delimiter = DlkTestStepRecord.globalParamDelimiter;
            foreach (DlkKeywordParameterRecord kwp in mView.Parameters)
            {
                paramString += kwp.mValue + delimiter;
            }
            if (!string.IsNullOrEmpty(paramString) && paramString.EndsWith(delimiter))
            {
                paramString = paramString.Substring(0, paramString.Length - delimiter.Length);
            }
            mView.TargetStep.mParameters[0] = paramString;
        }

        /// <summary>
        /// Get parameters of step keyword
        /// </summary>
        public void GetParameterCollection()
        {
            ObservableCollection<DlkKeywordParameterRecord> prms = new ObservableCollection<DlkKeywordParameterRecord>();
            if (!string.IsNullOrEmpty(mView.TargetStep.mScreen))
            {
                String mControl = string.Empty;
                if (String.IsNullOrEmpty(mView.TargetStep.mControl))
                {
                    mControl = mView.TargetStep.mControl;
                }
                else if (mView.TargetStep.mControl == "Function" && mView.TargetStep.mScreen == "Function")
                {
                    mControl = "Function";
                }
                else
                {
                    mControl = DlkDynamicObjectStoreHandler.Instance.GetControlType(mView.TargetStep.mScreen, mView.TargetStep.mControl);
                }
                for (int idx = 0; idx < DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, mView.TargetStep.mKeyword).Count; idx++)
                {
                    prms.Add(new DlkKeywordParameterRecord(DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, mView.TargetStep.mKeyword)[idx], "", idx));
                }

                if(prms.Any())
                {
                    string[] values = mView.TargetStep.mParameters.First().Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                    if (values.Any())
                    {
                        for (int i = 0; i < values.Count(); i++)
                        {
                            prms[i].mValue = values[i];
                        }
                    }
                }
            }
            mView.Parameters = prms;
        }
        #endregion

    }
}
