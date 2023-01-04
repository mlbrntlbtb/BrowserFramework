using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    /// <summary>
    /// Form tag that houses multiple dynamic dual lists.
    /// </summary>
    [ControlType("DynamicForm")]
    public class DlkDynamicForm : DlkBaseControl
    {
        #region Declarations

        private const string DUAL_LIST_TABLE_IDENTIFIER = @".//table[@class='table']";
        private const string LOCK_CATEGORY_IDENTIFIER = @".//input[contains(@name,'lock_category_metrics')]";
        private const string PHASE_WEIGHT_IDENTIFIER = @".//input[contains(@name,'phase_weight')]";
        private const string CASCADE_APPRAISAL_COMBOBOX = @".//select[./../../../div[contains(@class,'form-group form-required')]]";
        private const string MANAGE_WEIGHTS_TEXTBOX = @".//input[contains(@id,'field_weight')]";
        //input[contains(@id,'field_weight')]

        #endregion

        #region Constructors

        public DlkDynamicForm(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            initialize();
        }

        #endregion

        #region Keywords

        [Keyword("SelectDualListByRow")]
        public void SelectDualListByRow(string DualListRowNumber, string ItemRowNumber)
        {
            try
            {
                int rowIndex = Convert.ToInt32(DualListRowNumber) - 1;
                List<IWebElement> dualLists = getDualLists();
                
                if (rowIndex < dualLists.Count)
                {
                    DlkDualList selectedDualList = new DlkDualList("Dual List", dualLists[rowIndex]);
                    selectedDualList.SelectByRow(ItemRowNumber);
                    DlkLogger.LogInfo("SelectDualListByRow( ) execution passed.");
                }
                else
                {
                    DlkLogger.LogError(new Exception("DualList row " + DualListRowNumber + " not found."));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectDualListByRow( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("DeselectDualListByRow")]
        public void DeselectDualListByRow(string DualListRowNumber, string ItemRowNumber)
        {
            try
            {
                int rowIndex = Convert.ToInt32(DualListRowNumber) - 1;
                List<IWebElement> dualLists = getDualLists();

                if (rowIndex < dualLists.Count)
                {
                    DlkDualList selectedDualList = new DlkDualList("Dual List", dualLists[rowIndex]);
                    selectedDualList.DeselectByRow(ItemRowNumber);
                    DlkLogger.LogInfo("DeselectDualListByRow( ) execution passed.");
                }
                else
                {
                    DlkLogger.LogError(new Exception("DualList row " + DualListRowNumber + " not found."));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DeselectDualListByRow( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SelectLockCategoryByRow")]
        public void SelectLockCategoryByRow(string LockCategoryRowNumber, string Value)
        {
            try
            {
                int rowIndex = Convert.ToInt32(LockCategoryRowNumber) - 1;
                IList<IWebElement> lockCategories = getCommonElementList(LOCK_CATEGORY_IDENTIFIER);

                if (rowIndex < lockCategories.Count)
                {
                    DlkCheckBox lockCategoryCheckBox = new DlkCheckBox("Lock Category", lockCategories[rowIndex]);
                    lockCategoryCheckBox.Set(Value);
                    DlkLogger.LogInfo("SelectLockCategoryByRow( ) execution passed.");
                }
                else
                {
                    DlkLogger.LogError(new Exception("LockCategory row " + LockCategoryRowNumber + " not found."));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectLockCategoryByRow( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SetDualListCellValue")]
        public void SetDualListCellValue(string DualListRow, string Row, string Column, string Value, string Table)
        {
            try
            {
                int rowIndex = Convert.ToInt32(DualListRow) - 1;
                List<IWebElement> dualLists = getDualLists();

                if (rowIndex < dualLists.Count)
                {
                    DlkDualList selectedDualList = new DlkDualList("Dual List", dualLists[rowIndex]);
                    selectedDualList.SetCellValue(Row, Column, Value, Table);
                    DlkLogger.LogInfo("SetDualListByRowColumn( ) execution passed.");
                }
                else
                {
                    DlkLogger.LogError(new Exception("DualList row " + DualListRow + " not found."));
                }
            }
            catch(Exception ex)
            {
                throw new Exception("SetDualListByRowColumn( ) execution failed. : " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
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
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SelectAppraisalByLabelName")]
        public void SelectAppraisalByLabelName(string LabelName, string Value)
        {
            try
            {
                IList<IWebElement> selectAppraisalElements = mElement.FindElements(By.XPath(CASCADE_APPRAISAL_COMBOBOX));
                foreach (IWebElement element in selectAppraisalElements)
                {
                    IWebElement labelName = element.FindElement(By.XPath(@"../../label"));
                    DlkBaseControl labelControl = new DlkBaseControl("Label", labelName);
                    if (labelControl.GetValue().Trim().Equals(LabelName))
                    {
                        DlkComboBox comboBoxControl = new DlkComboBox("ComboBox : " + Value, element);
                        comboBoxControl.Select(Value);
                        break;
                    }
                }
                DlkLogger.LogInfo("SelectAppraisalByLabelName() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SelectAppraisalByLabelName() execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SetWeightByLabelName")]
        public void SetWeightByLabelName(string LabelName, string Value)
        {
            try
            {
                IList<IWebElement> selectAppraisalElements = mElement.FindElements(By.XPath(MANAGE_WEIGHTS_TEXTBOX));
                foreach (IWebElement element in selectAppraisalElements)
                {
                    IWebElement labelName = element.FindElement(By.XPath(@"../../label"));
                    DlkBaseControl labelControl = new DlkBaseControl("Label", labelName);
                    if (labelControl.GetValue().Trim().Equals(LabelName))
                    {
                        DlkTextBox comboBoxControl = new DlkTextBox("TextBox : " + Value, element);
                        comboBoxControl.Set(Value);
                        break;
                    }
                }
                DlkLogger.LogInfo("SetWeightByLabelName() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SetWeightByLabelName() execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        [Keyword("SetPhaseWeightByRow")]
        public void SetPhaseWeightByRow(string RowNumber, string Value)
        {
            try
            {
                int rowIndex = Convert.ToInt32(RowNumber) - 1;
                IList<IWebElement> phaseWeightElements = getCommonElementList(PHASE_WEIGHT_IDENTIFIER);

                if (phaseWeightElements.Count > 0)
                {
                    DlkTextBox phaseWeight = new DlkTextBox("Phase_Weight : ", phaseWeightElements[0]);
                    phaseWeight.Set(Value);
                    DlkLogger.LogInfo("SetPhaseWeightByRow( ) execution passed.");
                }
                else
                {
                    throw new Exception("SetPhaseWeightByRow() Row : " + RowNumber + " not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetPhaseWeightByRow() execution failed. " + ex.Message, ex);
            }
            finally
            {
                if (IsFromIFrame)
                {
                    DlkEnvironment.mSwitchediFrame = false;
                }
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();

            if (IsFromIFrame)
            {
                DlkEnvironment.mSwitchediFrame = true;
            }
        }

        private List<IWebElement> getDualLists()
        {
            List<IWebElement> dualLists = new List<IWebElement>();
            IList<IWebElement> dualListTables = getCommonElementList(DUAL_LIST_TABLE_IDENTIFIER);

            foreach (IWebElement table in dualListTables)
            {
                IWebElement dualListItem = FindParentByAttribute("class", "complex_duallist_layout row", table.FindElement(By.XPath("..")));

                if (dualListItem == null)
                {
                    DlkLogger.LogInfo("Found table but dual list is missing.");
                }
                else
                {
                    dualLists.Add(dualListItem);
                }
            }

            return dualLists;
        }

        private IList<IWebElement> getCommonElementList(string identifierXPath)
        {
            return mElement.FindElements(By.XPath(identifierXPath));
        }

        #endregion

        #region Properties

        private bool IsFromIFrame
        {
            get
            {
                if (mSearchType.ToLower().Equals("iframe_xpath"))
                {
                    return true;
                }

                return false;
            }
        }

        #endregion
    }
}
