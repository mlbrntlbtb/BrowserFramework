using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestRunner.Common;
using Recorder.Model;
using Recorder.View;
using CommonLib.DlkSystem;

namespace Recorder
{
    /// <summary>
    /// Interaction logic for AddHost.xaml
    /// </summary>
    public partial class AddVariable : Window, INotifyPropertyChanged
    {
        const string LABEL_TABLEROW = "Row :";

        public event PropertyChangedEventHandler PropertyChanged;

        //private List<Model.Variable> mCurrentVariables;
        private string mInitialValue;
        private string mCurrentValue;
        private string mStartIndex = string.Empty;
        private string mLength = string.Empty;
        private Model.Enumerations.ControlType mCType;
        private readonly Enumerations.GetValueType mGetValueType;

        public AddVariable(string InitialValue,  Model.Enumerations.ControlType CType, Window Owner)
        {
            InitializeComponent();
            this.Owner = Owner;
            //mCurrentVariables = currentVariables;
            mInitialValue = InitialValue;
            mCType = CType;
            mGetValueType = ((IMainView)Owner).GetValueType;
            mStartIndex = "0";
            mLength = mInitialValue.Length.ToString();
            StartIndex = mStartIndex;
            Length = mLength;
            Initialize();
        }

        public string CurrentValue
        {
            get
            {
                return mCurrentValue;
            }
            set
            {
                mCurrentValue = value;
                OnPropertyChanged("CurrentValue");
            }
        }

        public string StartIndex
        {
            get
            {
                return mStartIndex;
            }
            set
            {
                mStartIndex = ValidateStartIndex(value);
                OnPropertyChanged("StartIndex");             
                int iIdxBase = mCType == Enumerations.ControlType.MessageArea ? 1 : 0;
                if (int.Parse(mLength) > mInitialValue.Length - (int.Parse(mStartIndex) - iIdxBase))
                {
                    mLength = (mInitialValue.Length - (int.Parse(mStartIndex) - iIdxBase)).ToString();
                }
                Length = mLength;
            }
        }

        public string Length
        {
            get
            {
                return mLength;
            }
            set
            {
                mLength = ValidateLength(value);
                OnPropertyChanged("Length");
                CurrentValue = mInitialValue.Substring(int.Parse(mStartIndex) - (mCType == Enumerations.ControlType.MessageArea ? 1 : 0), int.Parse(mLength));
            }
        }

        private void Initialize()
        {
            cboName.ItemsSource = Variable.AllVariables;
            cboName.Items.Refresh();

            switch (mCType)
            {
                case Enumerations.ControlType.Table:
                    this.Title = Model.Constants.CP_KEYWORD_GETTABLEROWWITHCOLUMNVALUE + "()";
                    lblValue.Content = LABEL_TABLEROW;
                    txtStartIndex.IsEnabled = false;
                    txtLength.IsEnabled = false;
                    btnReset.IsEnabled = false;
                    break;
                default:
                    if (mGetValueType == Enumerations.GetValueType.AssignValue)
                    {
                        this.Title = Constants.CP_KEYWORD_ASSIGNVALUETOVARIABLE + "()";
                        rowValueSubStr.Height = new GridLength(0);
                        this.Height = 270;
                    }
                    else
                    {
                        this.Title = Constants.CP_KEYWORD_ASSIGNPARTIALVALUE + "()";
                    }
                    break;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                string curr = cboName.Text.Trim();
                //bool bFound = false;

                if (Validate())
                {
                    if (Variable.AllVariables.FindAll(x => x.Name == curr).Count > 0)
                    {
                        Variable.AllVariables.Find(x => x.Name == curr).InUse = true;
                        Variable.AllVariables.Find(x => x.Name == curr).CurrentValue = CurrentValue;
                        Variable.AllVariables.Find(x => x.Name == curr).StartIndex = StartIndex;
                        Variable.AllVariables.Find(x => x.Name == curr).Length = Length;
                    }
                    else
                    {
                        Variable newVar = new Variable(curr, (IMainView)this.Owner, CurrentValue, StartIndex, Length);
                    }
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }

                //foreach (Variable varb in mCurrentVariables)
                //{
                //    if (varb.Name == curr)
                //    {
                //        //varb.CurrentValue = mCurrentValue;
                //        varb.InUse = true;
                //        bFound = true;
                //        continue;
                //    }
                //    //varb.InUse = false;
                //}
                //if (!bFound)
                //{
                //    Variable newVar = new Variable(mCurrentVariables);
                //    newVar.Name = curr;
                //    newVar.InUse = true;
                //    Variable newVar = new Variable
                //    {
                //        //CurrentValue = mCurrentValue,
                //        Name = curr,
                //        InUse = true
                //    };
                //    mCurrentVariables.Add(newVar);
                //}
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private bool Validate()
        {
            return !string.IsNullOrEmpty(cboName.Text);
        }

        private void txtLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //Length = ValidateLength();
                //CurrentValue = mInitialValue.Substring(int.Parse(StartIndex) - 1, int.Parse(Length));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtStartIndex_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //StartIndex = ValidateStartIndex();
                //CurrentValue = mInitialValue.Substring(int.Parse(StartIndex) - 1, int.Parse(Length));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private string ValidateLength(string length)
        {
            int val;
            string ret = length;

            if (!int.TryParse(length, out val))
            {
                ret = mInitialValue.Length.ToString();
            }
            else if (val <= 0)
            {
                ret = "0";
            }
            else if (mCType == Enumerations.ControlType.MessageArea)
            {
                if (val > mInitialValue.Length - (int.Parse(mStartIndex) - 1))
                {
                    ret = (mInitialValue.Length - (int.Parse(mStartIndex) - 1)).ToString();
                }
            }
            else
            {
                if (val == mInitialValue.Length - (int.Parse(mStartIndex)))
                {
                    ret = (mInitialValue.Length - (int.Parse(mStartIndex))).ToString();
                }
            }
            return ret;
        }

        private string ValidateStartIndex(string startIndex)
        {
            int val;
            string ret = startIndex;

            if (mCType == Enumerations.ControlType.MessageArea)
            {
                if (!int.TryParse(startIndex, out val))
                {
                    ret = "1";
                }
                else if (val <= 0 || mInitialValue.Length == 0)
                {
                    ret = "1";
                }
                else if (val > mInitialValue.Length)
                {
                    ret = mInitialValue.Length.ToString();
                }
                return ret;
            }
            else
            {
                if (!int.TryParse(startIndex, out val))
                {
                    ret = "0";
                }
                else if (val <= 0 || mInitialValue.Length == 0)
                {
                    ret = "0";
                }
                else if (val > mInitialValue.Length)
                {
                    int zeroBasedLength = mInitialValue.Length - 1;
                    ret = zeroBasedLength.ToString();
                }
                return ret;
            }       
        }

        //private bool nonNumberEntered = false;

        // Handle the KeyDown event to determine the type of character entered into the control.
        private void numericTB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Initialize the flag to false.
                bool nonNumberEntered = false;

                // Determine whether the keystroke is a number from the top of the keyboard.
                if (e.Key < Key.D0 || e.Key > Key.D9)
                {
                    // Determine whether the keystroke is a number from the keypad.
                    if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                    {
                        // Determine whether the keystroke is a backspace.
                        if (e.Key != Key.Back)
                        {
                            // A non-numerical keystroke was pressed.
                            // Set the flag to true and evaluate in KeyPress event.
                            nonNumberEntered = true;
                        }
                    }
                }
                //If shift key was pressed, it's not a number.
                if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)
                {
                    nonNumberEntered = true;
                }

                if (nonNumberEntered)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Property changed notifyer
        /// </summary>
        /// <param name="Name"></param>
        private void OnPropertyChanged(string Name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(Name));
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mLength = mInitialValue.Length.ToString();
                StartIndex = mCType == Enumerations.ControlType.MessageArea ? "1" : "0";
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSIUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StartIndex = Convert.ToString((int.Parse(StartIndex)) + 1);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSIDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StartIndex = Convert.ToString((int.Parse(StartIndex)) - 1);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnLUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Length = Convert.ToString((int.Parse(Length)) + 1);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnLDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Length = Convert.ToString((int.Parse(Length)) - 1);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
