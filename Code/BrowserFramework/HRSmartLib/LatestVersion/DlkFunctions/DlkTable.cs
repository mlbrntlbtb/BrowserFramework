using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using Controls = HRSmartLib.LatestVersion.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using OpenQA.Selenium;
using System.Threading;

namespace HRSmartLib.LatestVersion.DlkFunctions
{
    [Component("Table")]
    public static class DlkTable
    {
        #region Declarations

        #endregion 

        #region Properties
        private static DlkDynamicObjectStoreHandler _dynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }


        #endregion

        #region Methods

        public static void GetTableRowWithColumnValue(string ScreenName, 
                                                       string ControlName,
                                                       string Column,
                                                       string SearchValue,
                                                       string VariableName)
        {
            try

            {
                Controls.DlkTable tableControl = getTableControl(ScreenName, ControlName);
                tableControl.GetTableRowWithColumnValue(Column, SearchValue, VariableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PerformRowActionByColumn(string ScreenName,
                                                     string ControlName,
                                                     string Row,
                                                     string ColumnNumber,
                                                     string MenuAction)
        {
            try

            {
                Controls.DlkTable tableControl = getTableControl(ScreenName, ControlName);
                tableControl.PerformRowActionByColumn(Row, ColumnNumber, MenuAction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static Controls.DlkTable getTableControl(string screenName, string controlName)
        {
            DlkObjectStoreFileControlRecord control = _dynamicObjectStoreHandler.GetControlRecord(screenName, controlName);
            return new Controls.DlkTable(control.mKey, control.mSearchMethod, control.mSearchParameters.Split('~'));
        }

        #endregion
    }
}
