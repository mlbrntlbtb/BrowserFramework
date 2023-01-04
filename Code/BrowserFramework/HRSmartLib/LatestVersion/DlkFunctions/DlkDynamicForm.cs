using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkFunctions
{

    [Component("Dynamic_Forms")]
    public static class DlkDynamicForm
    {

        #region Declarations


        private const string FORM_ROWS = "//div[@id='page-container']/div[contains(@class,'page-content')]/div[contains(@class,'sortable')]/div";
        private const string FIELD_LIST = "./following-sibling::div[contains(@class,'list-group-sortable')]";
        private const string INNER_ROWS = ".//span[@class='handler']/..";
        private const string INNER_COLUMNS = ".//div[contains(@class,'form-droppable')]";

        #endregion

        #region Properties

        #endregion

        #region Keywords

        [Keyword("AddFieldToFormByInnerDetails")]
        public static void AddFieldToFormByInnerDetails(string FromFieldType, string FromFieldName, string ToFormRow, string ToInnerRow, string ToInnerColumn)
        {
            try
            {
                IWebElement targetElementToAdd = getElementFromFieldList(FromFieldType, FromFieldName);
                IWebElement targetElementLocationToDrop = getElementFromFormByInnerDetails(ToFormRow, ToInnerRow, ToInnerColumn);
                DlkCommon.DlkCommonFunction.DragAndDrop(targetElementToAdd, targetElementLocationToDrop);
                DlkLogger.LogInfo("AddFieldToFormByInnerDetails( ) Successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("AddFieldToForm")]
        public static void AddFieldToForm(string FromFieldType, string FromFieldName, string ToFormRow, string ToFieldName)
        {
            try
            {
                IWebElement targetElementToAdd = getElementFromFieldList(FromFieldType, FromFieldName);
                IWebElement targetElementLocationToDrop = getElementFromFormByFieldName(ToFormRow, ToFieldName);
                DlkCommon.DlkCommonFunction.DragAndDrop(targetElementToAdd, targetElementLocationToDrop);
                DlkLogger.LogInfo("AddFieldToForm( ) Successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("RemoveFieldFromFormByDrag")]
        public static void RemoveFieldFromFormByDrag(string FromFormRow, string FromFieldName, string ToFieldType, string ToFieldName)
        {
            try
            {
                IWebElement targetElementToRemove = getElementFromFormByFieldName(FromFormRow, FromFieldName);
                IWebElement targetElementLocationToDrop = getElementFromFieldList(ToFieldType, ToFieldName);
                DlkCommon.DlkCommonFunction.DragAndDrop(targetElementToRemove, targetElementLocationToDrop);
                DlkLogger.LogInfo("RemoveFieldFromFormByDrag( ) Successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("RemoveFieldFromForm")]
        public static void RemoveFieldFromForm(string FromFormRow, string FromFieldName)
        {
            try
            {
                IWebElement targetElementToRemove = getElementFromFormByFieldName(FromFormRow, FromFieldName);
                IWebElement removeButton = targetElementToRemove.FindElement(By.XPath(".//a[contains(@class,'item-remove')]"));
                removeButton.Click();
                DlkLogger.LogInfo("RemoveFieldFromForm( ) Successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Keyword("RemoveFormRow")]
        public static void RemoveFormRow(string FormRow)
        {
            try
            {
                int formRowIndex = Convert.ToInt16(FormRow) - 1;
                //Get all rows of the form 
                IList<IWebElement> formRows = DlkEnvironment.AutoDriver.FindElements(By.XPath(FORM_ROWS));
                if (formRows.Count > formRowIndex)
                {
                    IWebElement removeButton = formRows[formRowIndex].FindElement(By.XPath(".//a[contains(@class,'row-remove')]"));
                    removeButton.Click();
                }
                else
                {
                    throw new Exception("Missing Form Row : " + FormRow);
                }
                DlkLogger.LogInfo("RemoveFormRow( ) Successfully executed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Methods


        private static IWebElement getElementFromFieldList(string fieldType, string fieldName)
        {
            IWebElement draggableElement = null;
            IList<IWebElement> fieldTypeList = DlkCommon.DlkCommonFunction.GetElementWithText(fieldType, 
                                                                                              elementTag: "a", 
                                                                                              ignoreCasing: true, 
                                                                                              returnDisplayedElements: true);
            if (fieldTypeList.Count > 0)
            {
                IWebElement targetFieldList = fieldTypeList[0].FindElement(By.XPath(FIELD_LIST));
                draggableElement = getFieldElement(targetFieldList, fieldName);
            }
            else
            {
                throw new Exception("Missing field type : " + fieldType);
            }

            return draggableElement;
        }

        private static IWebElement getElementFromFormByInnerDetails(string formRow, 
                                                                    string innerRow, 
                                                                    string innerColumn)
        {
            IWebElement elementFromForm = null;
            int formRowIndex = Convert.ToInt16(formRow) - 1;
            int innerRowIndex = Convert.ToInt16(innerRow) - 1;
            int innerColumnIndex = Convert.ToInt16(innerColumn) - 1;

            //Get all rows of the form 
            IList<IWebElement> formRows = DlkEnvironment.AutoDriver.FindElements(By.XPath(FORM_ROWS));
            if (formRows.Count > formRowIndex)
            {
                //Get target form row 
                IWebElement targetFormRow = formRows[formRowIndex];
                IList<IWebElement> innerColumns = targetFormRow.FindElements(By.XPath(INNER_COLUMNS));
                if (innerColumns.Count > innerColumnIndex)
                {
                    IWebElement targetInnerColumn = innerColumns[innerColumnIndex];
                    IList<IWebElement> innerRows = targetInnerColumn.FindElements(By.XPath(INNER_ROWS));
                    if (innerRows.Count > innerRowIndex)
                    {
                        elementFromForm = innerRows[innerRowIndex];
                    }
                    else
                    {
                        elementFromForm = targetInnerColumn;
                    }
                }
                else
                {
                    throw new Exception("Missing inner index : " + innerColumn);
                }
            }
            else
            {

                throw new Exception("Missing form row : " + formRow);
            }

            return elementFromForm;
        }
        private static IWebElement getElementFromFormByFieldName(string formRow, string fieldName)
        {
            IWebElement elementFromForm = null;
            int formRowIndex = Convert.ToInt16(formRow) - 1;

            //Get all rows of the form 
            IList<IWebElement> formRows = DlkEnvironment.AutoDriver.FindElements(By.XPath(FORM_ROWS));
            if (formRows.Count > formRowIndex)
            {
                //Get target form row 
                IWebElement targetFormRow = formRows[formRowIndex];
                elementFromForm = getFieldElement(targetFormRow, fieldName);

            }
            else
            {

                throw new Exception("Missing form row : " + formRow);
            }

            return elementFromForm;
        }

        private static IWebElement getFieldElement(IWebElement parentElement, string fieldName)
        {
            IWebElement fieldElement = null;
            IList<IWebElement> targetElementList = DlkCommon.DlkCommonFunction.GetElementWithText(fieldName,
                                                                                                  parentElement,
                                                                                                  true,
                                                                                                  elementTag: "div",
                                                                                                  ignoreCasing: true);
            if (targetElementList.Count > 0)
            {
                fieldElement = targetElementList[0].FindElement(By.XPath("./ancestor::div[contains(@class, 'dynamic_field_item') or contains(@class, 'list-group-item')]"));
            }
            else
            {
                throw new Exception("Missing field name : " + fieldName);
            }

            return fieldElement;
        }
        #endregion


    }
}
