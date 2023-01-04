using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Query
    {
        public string VerifyTitle(string ExpectedTitle)
        {
            StringBuilder action = new StringBuilder();
            ExpectedTitle = ExpectedTitle.Trim();
            action.AppendLine("Function.WaitControlDisplayed(new SeleniumControl(sDriver,\"QueryTitle\", \"ID\", \"qryHeaderLabel\"));");
            action.Append("Function.AssertEqual(\"").Append(ExpectedTitle).Append("\", ");
            action.AppendLine("new SeleniumControl(sDriver,\"QueryTitle\", \"ID\", \"qryHeaderLabel\").GetValue().Trim());");
            return action.ToString();
        }

        public string SetSearchCriterion(string CriterionName, string Relationship, string SearchString)
        {
            StringBuilder action = new StringBuilder();
            action.Append("SeleniumControl ctl = new SeleniumControl(sDriver,\"Target\", \"xpath_display\", \"//*[@id='basicQTbl']/tbody/tr/td/span[text()='").Append(CriterionName).AppendLine("']\");");
            action.AppendLine("Function.WaitControlDisplayed(ctl);");
            action.AppendLine("SeleniumControl RelationshipField = new SeleniumControl(sDriver,\"Relationship\", ctl.mElement.FindElement(By.XPath(\"../following-sibling::td[1]\")));");
            action.AppendLine("SeleniumControl SearchField = new SeleniumControl(sDriver,\"Relationship\", ctl.mElement.FindElement(By.XPath(\"../following-sibling::td[2]\")));");
            
            if (!Relationship.ToLower().Equals("is"))
            {
                action.AppendLine("new SeleniumControl(sDriver, \"RelDropdown\", RelationshipField.mElement.FindElements(By.XPath(\"./descendant::*[@class='tCCBImg']\")).FirstOrDefault()).Click();");
                action.Append("new SeleniumControl(sDriver, \"RelItem\", \"xpath_display\", \".//descendant::div[text() = '").Append(Relationship).AppendLine("']\").Click();");
            }

            action.Append("if (SearchField.mElement.GetAttribute(\"class\").Equals(\"tCCB\"))").AppendLine(" {");
            action.AppendLine("new SeleniumControl(sDriver, \"SearchDropdown\", SearchField.mElement.FindElements(By.XPath(\".//descendant::*[@class='tCCBImg']\")).FirstOrDefault()).Click();");
            action.Append("new SeleniumControl(sDriver, \"SearchItem\", \"xpath_display\", \".//descendant::div[text()='").Append(SearchString).AppendLine("']\").Click();");
            action.AppendLine("} else { ");
            action.AppendLine("IWebElement elem = SearchField.mElement.FindElements(By.XPath(\".//descendant::*[@class='queryBasicFld']\")).FirstOrDefault(); elem.Clear(); elem.Click();");
            action.Append("new SeleniumControl(sDriver, \"SearchDropdown\", SearchField.mElement.FindElements(By.XPath(\".//descendant::*[@class='queryBasicFld']\")).FirstOrDefault()).SendKeys(\"").Append(SearchString).Append("\");").AppendLine("}");


            return action.ToString();
        }

        public string AddQueryCondition(string LogicalOperator, string ConditionName, string Relationship, string Value)
        {
            StringBuilder action = new StringBuilder();
            action.AppendLine("new SeleniumControl(sDriver, \"ConditionName\", \"id\", \"fieldList\").Click();");
            //action.AppendLine("cboCondition.Click();");
            action.Append("new SeleniumControl(sDriver, \"ConditionItem\", \"xpath_display\", \".//descendant::div[text() = '").Append(ConditionName).AppendLine("']\").Click();");

            action.AppendLine("new SeleniumControl(sDriver, \"Relationship\", \"id\", \"relationList\").Click();");
            //action.AppendLine("cboRelationship.Click();");
            action.Append("new SeleniumControl(sDriver, \"RelationItem\", \"xpath_display\", \".//descendant::div[text() = '").Append(Relationship).AppendLine("']\").Click();");

            if (!String.IsNullOrEmpty(Value))
            {
                action.AppendLine("SeleniumControl txtValue = new SeleniumControl(sDriver, \"Value\", \"id\", \"valueEntered\");");
                action.AppendLine("SeleniumControl cboValue = new SeleniumControl(sDriver, \"ValueCB\", \"id\", \"valueCBView\");");
                action.AppendLine("if(txtValue.Exists()){");
                action.Append("txtValue.SendKeys(\"").Append(Value).AppendLine("\");");
                action.AppendLine("} else {");
                action.AppendLine("cboValue.Click();");
                action.Append("new SeleniumControl(sDriver, \"SearchItem\", \"xpath_display\", \".//descendant::div[text()='").Append(Value).AppendLine("']\").Click(); }");
            }
            if (!String.IsNullOrEmpty(LogicalOperator))
            {
                action.AppendLine("new SeleniumControl(sDriver, \"LogicalOperator\", \"id\", \"CombineOperator\").Click();");
                //action.AppendLine("Function.WaitControlDisplayed(LogicalOperator);");
                action.Append("new SeleniumControl(sDriver, \"OperatorItem\", \"xpath_display\", \".//descendant::div[text() = '").Append(LogicalOperator).AppendLine("']\").Click();");
            }
            action.AppendLine("new SeleniumControl(sDriver, \"AddButton\", \"id\", \"AddBtn\").Click();");
            return action.ToString();
        }
    }
}
