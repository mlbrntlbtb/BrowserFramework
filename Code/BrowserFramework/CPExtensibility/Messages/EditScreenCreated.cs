using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPExtensibility.Messages
{
    /// <summary>
    /// This message will be sent from the EditScreenWindowViewModel to the Messenger to propagate to all viewmodels listening to this type of message
    /// This is basically a fancy way of storing data that will be passed from one viewmodel to another.
    /// </summary>
    public class EditScreenCreated
    {
        #region CONSTRUCTORS
        public EditScreenCreated()
        {

        }

        public EditScreenCreated(string scr, string env)
        {
            this.Environment = env;
            this.Screen = scr;
        }
        #endregion

        #region PRIVATE FIELDS
        private string _screen;
        private string _environment;
        #endregion

        #region PUBLIC PROPERTIES TO EXPOSE THE PRIVATE FIELDS
        public string Screen
        {
            get { return _screen; }
            set { _screen = value; }
        }

        public string Environment
        {
            get { return _environment; }
            set { _environment = value; }
        }
        #endregion
    }
}
