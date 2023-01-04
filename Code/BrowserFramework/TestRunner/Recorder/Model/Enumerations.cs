using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder.Model
{
    public static class Enumerations
    {
        public enum ActionType
        {
            Click,
            AssignValue,
            Keystroke,
            Hover,
            Verify,
            RightClick,
            Unknown
        }

        public enum ControlType
        {
            // Add as needed
            Button,
            Calendar,
            CheckBox,
            ComboBox,
            ComboBoxItem,
            ContextMenu,
            Dialog,
            Form,
            Link,
            Menu,
            MessageArea,
            RadioButton,
            TextBox,
            Tab,
            Table,
            MultiPartTable,
            NavigationMenu,
            SearchAppResultList,
            Toolbar,
            TextArea,
            ConditionListBox,
            AppMenuView,
            Unknown
        }

        public enum DescriptorType
        {
            Id,
            Class,
            Xpath,
            Unknown
        }

        public enum ViewStatus
        {
            Default,
            Paused,
            Resuming,
            ActionDetected,
            ActionDetected_Continue,
            InitializingDefault,
            //StepAdded
            ReadyToVerify,
            InitializingAssignValue,
            ReadyToGetValue,
            GettingValue,
            GetValueFinished,
            InitializingVerify,
            Verifying,
            VerifyFinished,
            VerifyNotSupported,
            //Ready
            Recording,
            Stopping,
            UnhandledAlertDetected,
            AutoLogin,
            Playback,
            //Mapping
            ControlAlreadyExists,
            DuplicateControlName,
            UnsupportedControlType,
            Saved
        }

        public enum KeywordType
        {
            Set,
            Verify,
            GetValue,
            Alternate
        }

        public enum VerifyType
        {
            VerifyContent,
            VerifyExists,
            VerifyReadOnly
        }

        /// <summary>
        /// AssignValue options
        /// </summary>
        public enum GetValueType
        {
            AssignValue,
            AssignPartialValue            
        }
    }
}
