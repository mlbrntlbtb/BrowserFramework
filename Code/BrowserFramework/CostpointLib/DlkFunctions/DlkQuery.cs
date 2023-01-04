using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using CostpointLib.System;
using CostpointLib.DlkControls;

namespace CostpointLib.DlkFunctions
{
    [Component("Query")]
    public static class DlkQuery
    {
        [Keyword("SetSearchCriterion", new String[] {"1|text|Criterion Name|ID", 
                                                     "2|text|Relationship|begins with", 
                                                     "3|text|Search String|A"})]
        public static void SetSearchCriterion(String CriterionName, String Relationship, String SearchString)
        {
            try
            {
                // Find CriterionName
                DlkBaseControl ctl = new DlkBaseControl("Target", "xpath", "//*[@id='basicQTbl']/tbody/tr/td/span[text()='" + CriterionName + "']");
                ctl.FindElement();
                DlkBaseControl relationship = new DlkBaseControl("Relationship", ctl.mElement.FindElement(By.XPath("../following-sibling::td[1]")));
                DlkBaseControl searchString = new DlkBaseControl("SearchString", ctl.mElement.FindElement(By.XPath("../following-sibling::td[2]")));

                if (!SetSearchCriterionRelationship(relationship, Relationship))
                {
                    throw new Exception("Unable to set search criterion relationship");
                }

                if (!SetSearchCriterionSearchString(searchString, SearchString))
                {
                    throw new Exception("Unable to set search criterion search string");

                }

                DlkLogger.LogInfo("Successfully executed SetSearchCriterion()");
            }
            catch (Exception e)
            {
                throw new Exception("SetSearchCriterion() failed : " + e.Message, e);
            }
        }


        [Keyword("AddQueryCondition", new String[] {"1|text|Logical Operator|and",
                                                    "2|text|Condition Name|ID",
                                                    "3|text|Relationship [optional]|Begins With",
                                                    "4|text|Value|A"})]
        public static void AddQueryCondition(String LogicalOperator, String ConditionName, String Relationship, String Value)
        {
            try
            {
                // set field list
                DlkComboBox cboCondition = new DlkComboBox("ConditionName", "ID", "fieldList");
                cboCondition.Select(ConditionName);

                // set relationship
                DlkComboBox cboRelationship = new DlkComboBox("Relationship", "ID", "relationList", false);
                cboRelationship.Select(Relationship);

                // set value if not null
                if (!String.IsNullOrEmpty(Value))
                {
                    DlkTextBox txtValue = new DlkTextBox("Value", "ID", "valueEntered");
                    DlkComboBox cboValue = new DlkComboBox("ValueCB", "ID", "valueCBView");

                    if (txtValue.Exists())
                    {
                        txtValue.Set(Value);
                    }
                    else if (cboValue.Exists())
                    {
                        cboValue.Select(Value);
                    }
                    else
                    {
                        throw new Exception("Failed to set Value");
                    }
                }

                // Set Logical Operator
                DlkComboBox cboLogicalOperator = new DlkComboBox("Operator", "ID", "CombineOperator");
                cboLogicalOperator.Initialize();

                if ((DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("7.1")) || String.IsNullOrEmpty(DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version))
                {
                    if (cboLogicalOperator.mElement.FindElements(By.XPath("./descendant::span[@class='tCCBImg' and @dis='1']")).Count == 0)
                    {
                        cboLogicalOperator.Select(LogicalOperator);
                    }
                }
                else
                {
                    // For CP 701 version
                    if (cboLogicalOperator.mElement.FindElements(By.XPath("./descendant::img[@class='tCCBImg' and contains(@src, 'disabled')]")).Count == 0)
                    {
                        cboLogicalOperator.Select(LogicalOperator);
                    }
                }
        
                // click add button
                DlkBaseControl ctlButton = new DlkBaseControl("AddButton", "ID", "AddBtn");
                ctlButton.Click();

                DlkLogger.LogInfo("Successfully executed AddQueryCondition()");

            }
            catch (Exception e)
            {
                throw new Exception("AddQueryCondition() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Expected Title|sample title" })]
        public static void VerifyTitle(String ExpectedTitle)
        {
            DlkBaseControl lblTitle = new DlkBaseControl("QueryTitle", "ID", "qryHeaderLabel");

            lblTitle.FindElement();
            DlkAssert.AssertEqual("VerifyTitle() : QueryTitle", ExpectedTitle.Trim(), lblTitle.GetValue().Trim());
        }

        #region Private Functions

        private static bool SetSearchCriterionRelationship(DlkBaseControl RelationshipField, String Relationship)
        {
            if (Relationship.ToLower().Equals("is"))
            {
                // control value is already set to 'is' and readonly
                return true;
            }
            try
            {
                DlkComboBox cboRelationship = new DlkComboBox("Relationship", RelationshipField, "classname", "tCCB");
                cboRelationship.Select(Relationship);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool SetSearchCriterionSearchString(DlkBaseControl SearchStringField, String SearchString)
        {
            try
            {
                DlkTextBox fld = new DlkTextBox("SearchString", SearchStringField, "classname", "queryBasicFld");
                DlkComboBox lst = new DlkComboBox("SearchString", SearchStringField, "classname", "tCCB");
                if (fld.Exists())
                {
                    fld.Set(SearchString);
                }
                else if (lst.Exists())
                {
                    lst.Select(SearchString);
                }
                else
                {
                    throw new Exception("Failed to set Value");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
