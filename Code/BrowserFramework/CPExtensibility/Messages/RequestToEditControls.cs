using CPExtensibility.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPExtensibility.Messages
{
    /// <summary>
    /// This message will be sent from the CPExtensibilityMainFormViewModel to the EditScreenWindow viewmodel.   
    /// </summary>
    public class RequestToEditControls
    {
        private ObservableCollection<Control> _controlsToEdit;
        public RequestToEditControls(ObservableCollection<Control> selectedCtls)
        {
            _controlsToEdit = selectedCtls;
        }

        public ObservableCollection<Control> ControlsToEdit
        {
            get
            {
                return _controlsToEdit;
            }
            set
            {
                _controlsToEdit = value;
            }
        }
    }
}
