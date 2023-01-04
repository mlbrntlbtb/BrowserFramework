using CPExtensibility.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPExtensibility.Messages
{
    public class ControlsEdited
    {
        #region PRIVATE FIELDS
        private ObservableCollection<Control> _controlsToEdit;
        private string _containerNameToAdd;
        #endregion

        #region CONSTRUCTORS
        public ControlsEdited(ObservableCollection<Control> selectedCtls)
        {
            _controlsToEdit = selectedCtls;
        }

        public ControlsEdited(ObservableCollection<Control> selectedCtls, string containerName)
        {
            _controlsToEdit = selectedCtls;
            _containerNameToAdd = containerName;
        }
        #endregion

        #region PUBLIC PROPERTIES
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

        public string ContainerName
        {
            get
            {
                return _containerNameToAdd;
            }
            set
            {
                _containerNameToAdd = value;
            }
        }
        #endregion
    }
}
