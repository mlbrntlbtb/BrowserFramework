using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CPExtensibility.View;
using CPExtensibility.Utility;
using CPExtensibility.Messages;
using System.Collections.ObjectModel;
using CPExtensibility.Model;

namespace CPExtensibility.Services
{
    /// <summary>
    /// This class will serve as a utility class to be used when opening a new window/view
    /// </summary>
    public static class DialogService
    {
        private static Window newscreen = null;
        private static Window editScreen = null;
        private static Window editControls = null;
        private static Window unsavedchanges = null;
        private static Window mainForm = null;

        public static void ShowNewScreenDialog()
        {
            try
            {
                newscreen = new NewScreenWindow();
                newscreen.ShowDialog();
            }
            catch
            {
            }
        }

        public static void CloseNewScreenDialog()
        {
            if (newscreen != null)
            {
                newscreen.Close();
            }
        }

        public static void ShowEditScreenDialog()
        {
            try
            {
                editScreen = new EditScreenWindow();
                editScreen.ShowDialog();
            }
            catch 
            {
                
            }
        }

        public static void CloseEditScreenDialog()
        {
            if (editScreen != null)
            {
                editScreen.Close();
            }
        }

        public static void ShowEditControlsDialog(object controls)
        {
            try
            {
                editControls = new EditControlWindow();
                editControls.Show();
                Messenger.Default.Send<RequestToEditControls>(new RequestToEditControls(controls as ObservableCollection<Control>), "EDIT CONTROLS");
            }
            catch
            {

            }
        }

        public static void CloseEditControlsDialog()
        {
            if (editControls != null)
            {
                editControls.Close();
            }
        }

        public static void ShowUnsavedChangesDialog()
        {
            unsavedchanges = new UnsavedChangesWindow();
            unsavedchanges.ShowDialog();
        }

        public static void CloseUnsavedChangesDialog()
        {
            if (unsavedchanges != null)
            {
                unsavedchanges.Close();
            }
        }

        public static void ShowMainFormDialog()
        {
            mainForm = new CPExtensibilityMainForm();
            mainForm.Show();
        }

        public static void CloseMainFormDialog()
        {
            if (mainForm != null)
            {
                mainForm.Close();
            }
        }

    }
}
