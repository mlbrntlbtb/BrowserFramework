using CommonLib.DlkSystem;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// Class that holds password masked enabled keyword parameter
    /// </summary>
    public class DlkPasswordParameter
    {
        public DlkPasswordParameter(string name, int index, string password, bool allowMasked = false)
        {
            Name = name;
            Index = index;
            Password = password;
            AllowMasked = allowMasked;
        }

        public string Name { get; set; }
        public int Index { get; set; }
        public string Password { get; set; }
        public bool AllowMasked { get; set; }
    }

    /// <summary>
    /// Class that holds password enabled controls
    /// </summary>
    public class DlkPasswordControl
    {
        public DlkPasswordControl(string screen, string control, string keyword, List<DlkPasswordParameter> parameters)
        {
            Screen = screen;
            Control = control;
            Keyword = keyword;
            Parameters = parameters;
        }
        public string Screen { get; private set; }
        public string Control { get; private set; }
        public string Keyword { get; private set; }
        public List<DlkPasswordParameter> Parameters { get; set; }
    }

    /// <summary>
    /// Master record for password masked
    /// </summary>
    public class DlkPasswordMaskedRecord
    {
        #region constants
        /// <summary>
        /// password masked character
        /// </summary>
        public const string PasswordChar = "•";
        public const string DEFAULT_BLANK_MASKED_VALUE = "•••••";
        #endregion

        public static List<DlkPasswordControl> GetPasswordControls()
        {
            List<DlkPasswordControl> result = new List<DlkPasswordControl>
            {
                new DlkPasswordControl("CP7Login", "Password", "Set",
                new List<DlkPasswordParameter> { new DlkPasswordParameter("TextToEnter", 0, "") }),

                new DlkPasswordControl("CP7Login", "Password", "VerifyText",
                new List<DlkPasswordParameter> { new DlkPasswordParameter("TextToVerify", 0, "") }),

                new DlkPasswordControl("CP7Login", "Password", "ShowAutoComplete",
                new List<DlkPasswordParameter> { new DlkPasswordParameter("LookupString", 0, "") }),

                new DlkPasswordControl("Database", "", "ExecuteQuery",
                new List<DlkPasswordParameter> { new DlkPasswordParameter("DBPassword", 4, "") }),

                new DlkPasswordControl("Database", "", "ExecuteQueryAssignToVariable",
                new List<DlkPasswordParameter> { new DlkPasswordParameter("DBPassword", 4, "") })
            };

            return result;
        }

        /// <summary>
        /// Checks current product if password masked enabled
        /// </summary>
        public static bool IsPasswordMaskedProduct
        {
            get
            {
                return new string[]
                {
                    "BudgetingAndPlanning",
                    "Costpoint_711",
                    "TimeAndExpense",
                    "Costpoint_701"
                }.Contains(DlkEnvironment.mProductFolder);
            }
        }

        /// <summary>
        /// Get password masked enabled control parameters
        /// </summary>
        /// <param name="step">current Step</param>
        /// <returns>control with specific password enabled parameters only</returns>
        public static DlkPasswordControl GetMatchedControl(DlkTestStepRecord step)
        {
            if (passwordControls == null)
            {
                passwordControls = GetPasswordControls();
            }

            DlkPasswordControl control = passwordControls.SingleOrDefault(s => s.Screen == step.mScreen && s.Control == step.mControl && s.Keyword == step.mKeyword);
            return control;
        }

        /// <summary>
        /// Checks if the parameter should be masked or not
        /// </summary>
        /// <param name="step"> the entire test step</param>
        /// <param name="index">index of parameter</param>
        /// <returns></returns>
        public static bool IsMaskedParameter(DlkTestStepRecord step, int index)
        {
            bool ret = false;
            DlkPasswordControl control = GetMatchedControl(step);
            if (control == null) return false;

            if(control.Parameters.Any(x => x.Index == index))
            {
                ret = true;
            }
            return ret;
        }

        private static List<DlkPasswordControl> passwordControls { get; set; }
    }

}
