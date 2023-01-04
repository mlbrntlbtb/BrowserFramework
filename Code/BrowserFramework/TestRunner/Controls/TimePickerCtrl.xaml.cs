using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestRunner.Controls
{
    /// <summary>
    /// Interaction logic for TimePickerCtrl.xaml
    /// </summary>
    public partial class TimePickerCtrl : UserControl
    {
        public TimePickerCtrl()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(txtHr, OnPaste);
            DataObject.AddPastingHandler(txtMin, OnPaste);
            AMPM = amPM;
            Hours = hours;
            Minutes = minutes;
            focusedControl = txtHr;
        }

        #region DECLARATIONS

        private TextBox focusedControl;
        private const int MAXHR = 12;
        private const int MINHR = 1;
        private const int MINMINUTESEC = 0;
        private const int MAXMINUTESEC = 59;

        private int minutes = System.DateTime.Now.Minute;
        private int hours = int.Parse(System.DateTime.Now.ToString("hh"));
        private string amPM = System.DateTime.Now.ToString("tt");

        #endregion 

        #region PROPERTIES

        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value >= MINMINUTESEC && value <= MAXMINUTESEC)
                {
                    minutes = value;
                    txtMin.Text = value.ToString("00");
                    txtMin.SelectAll();
                }
                else if (value == MINMINUTESEC - 1)
                {
                    txtMin.Text = MAXMINUTESEC.ToString("00");
                }
                else if (value == MAXMINUTESEC + 1)
                {
                    txtMin.Text = MINMINUTESEC.ToString("00");
                }
                else
                {
                    txtMin.Text = minutes.ToString("00");
                }
            }
        }
     
        public int Hours
        {
            get { return hours; }
            set
            {
                if (value >= MINHR && value <= MAXHR)
                {
                    hours = value;
                    txtHr.Text = value.ToString("00");
                }
                else if (value == MINHR - 1)
                {
                    txtHr.Text = MAXHR.ToString("00");
                    hours = MAXHR;
                }
                else if (value == MAXHR + 1)
                {
                    txtHr.Text = MINHR.ToString("00");
                    hours = MINHR;
                }
                else
                {
                    txtHr.Text = hours.ToString("00");
                }
            }
        }

        public string AMPM
        {
            get { return amPM; }
            set
            {
                amPM = value;
                txtAMPM.Text = value;
            }
        }

        #endregion

        #region EVENTHANDLERS

        /// <summary>
        /// Triggered by Pasting on txtHr (Hours) or txtMin (Minutes)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            var text = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            char[] txtChars = text.ToCharArray();
            foreach(char c in txtChars)
            {
                if(!Char.IsDigit(c))
                {
                    e.CancelCommand();
                    break;
                }
            }
        }

        /// <summary>
        /// Triggered by txtHr (Hours) or txtMin (Minutes)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void KeyDown_Event(object sender, KeyEventArgs args)
        {
            switch (((Grid)sender).Name)
            {
                case "min":
                    if (args.Key == Key.Up)
                        this.Minutes++;
                    else if (args.Key == Key.Down)
                        this.Minutes--;
                    else
                        ManipulateMinutes(args.Key);
                    break;
                case "hour":
                    if (args.Key == Key.Up)
                        this.Hours++;
                    else if (args.Key == Key.Down)
                        this.Hours--;
                    else
                        ManipulateHR(args.Key);
                    break;
                case "ampm":
                    if (AMPM == "AM")
                        this.AMPM = "PM";
                    else
                        this.AMPM = "AM";
                    focusedControl.Focus();
                    focusedControl.SelectAll();
                    break;
                default:
                    break;
            }
            args.Handled = true;
        }

        /// <summary>
        /// Triggered when txtHr (Hours) has focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHr_GotFocus(object sender, RoutedEventArgs e)
        {
            focusedControl = (TextBox)sender;
            focusedControl.SelectAll();
        }

        /// <summary>
        /// Triggered when txtMin (Minutes) has focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMin_GotFocus(object sender, RoutedEventArgs e)
        {
            focusedControl = (TextBox)sender;
            focusedControl.SelectAll();
        }

        /// <summary>
        /// Triggered when txtAMPM (AM/PM) has focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAMPM_GotFocus(object sender, RoutedEventArgs e)
        {
            focusedControl = (TextBox)sender;
        }

        /// <summary>
        /// Triggered when Down button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownTime_Click(object sender, RoutedEventArgs e)
        {
            if (focusedControl != null)
            {
                switch (focusedControl.Name)
                {
                    case "txtHr":
                        this.Hours--;
                        break;
                    case "txtMin":
                        this.Minutes--;
                        break;
                    case "txtAMPM":
                        if (AMPM == "AM")
                            this.AMPM = "PM";
                        else
                            this.AMPM = "AM";
                        break;
                    default:
                        break;
                }
                focusedControl.Focus();
                focusedControl.SelectAll();
            }

        }

        /// <summary>
        /// Triggered when Up button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpTime_Click(object sender, RoutedEventArgs e)
        {
            if (focusedControl != null)
            {
                switch (focusedControl.Name)
                {
                    case "txtHr":
                        this.Hours++;
                        break;
                    case "txtMin":
                        this.Minutes++;
                        break;
                    case "txtAMPM":
                        if (AMPM == "AM")
                            this.AMPM = "PM";
                        else
                            this.AMPM = "AM";
                        break;
                    default:
                        break;
                }
                focusedControl.Focus();
                focusedControl.SelectAll();
            }
        }

        /// <summary>
        /// Triggered by performing Cut on txtHr (Hours) or txtMin (Minutes)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if(e.Command == ApplicationCommands.Cut)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Triggered when txtHr is changing value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Triggered when txtMin is changing value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Triggered when txtHr is has changed value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtHr.Text.Length != 0)
            {
                Hours = int.Parse(txtHr.Text);
            }
            else
            {
                Hours = MINHR;
            }
        }

        /// <summary>
        /// Triggered when txtHr is has changed value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtMin.Text.Length != 0)
            {
                Minutes = int.Parse(txtMin.Text);
            }
            else
            {
                Minutes = MINMINUTESEC;
            }
        }

        /// <summary>
        /// Initially triggered when a textbox is clicked. Used to select all text and stop the event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!textBox.IsFocused)
            {
                textBox.Focus();
                textBox.SelectAll();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Triggered when a key is pressed in a text box. (Used to identify the key pressed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewKeyDown_Txts(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                request.Wrapped = true;
                focusedControl.MoveFocus(request);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Returns the time entered in the controls as DateTime in 24hr format
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetTimeAsDateTime()
        {
            DateTime thisTime = new DateTime();
            const int convertTo24HR = 12;

            if (AMPM == "PM")
            {
                if (Hours < MAXHR)
                {
                    thisTime = thisTime.AddHours(Hours + convertTo24HR);
                }
                else
                {
                    thisTime = thisTime.AddHours(convertTo24HR);
                }
            }
            else
            {
                if (Hours < MAXHR)
                {
                    thisTime = thisTime.AddHours(Hours);
                }
            }

            thisTime = thisTime.AddMinutes(Minutes);
            return thisTime;
        }

        /// <summary>
        /// Used to manipulate the HR (for appending)
        /// </summary>
        /// <param name="Input">New key pressed</param>
        private void ManipulateHR(Key Input)
        {
            if (focusedControl.SelectionLength > 0 || focusedControl.SelectionStart > 1)
            {
                if (Input <= Key.D9 && Input >= Key.D0)
                {
                    if (focusedControl.Text.EndsWith("0"))
                    {
                        focusedControl.Text = focusedControl.Text[0].ToString() + Input.ToString()[1];
                    }
                    else if (focusedControl.Text.StartsWith("0") && focusedControl.Text.EndsWith("1") 
                        && (Input.ToString()[1] == '0' || Input.ToString()[1] == '1' || Input.ToString()[1] == '2') )
                    {
                        focusedControl.Text = focusedControl.Text + Input.ToString()[1];
                    }
                    else
                    {
                        focusedControl.Text = Input.ToString()[1].ToString();
                    }
                }
                focusedControl.SelectionStart = txtHr.Text.Length;
            }
        }

        /// <summary>
        /// Used to manipulate the Minutes (for appending)
        /// </summary>
        /// <param name="Input">New key pressed</param>
        private void ManipulateMinutes(Key Input)
        {
            if(focusedControl.SelectionLength > 0 || focusedControl.SelectionStart > 0)
            {
                if (Input <= Key.D9 && Input >= Key.D0)
                {
                    if (focusedControl.Text.EndsWith("0"))
                    {
                        focusedControl.Text = focusedControl.Text[0].ToString() + Input.ToString()[1];
                    }
                    else if (focusedControl.Text.StartsWith("0") && AcceptableMinutesStartingDigit(focusedControl.Text[1]))
                    {
                        focusedControl.Text = focusedControl.Text + Input.ToString()[1];
                    }
                    else
                    {
                        focusedControl.Text = Input.ToString()[1].ToString();
                    }
                }
                focusedControl.SelectionStart = txtMin.Text.Length;
            }
        }

        /// <summary>
        /// Checks if the first digit is from 1-5
        /// </summary>
        /// <param name="ChrDigit">First digit</param>
        /// <returns></returns>
        private bool AcceptableMinutesStartingDigit(char ChrDigit)
        {
            int Digit = (int)Char.GetNumericValue(ChrDigit);
            if(Digit <= 5 && Digit >= 1)
            {
                return true;
            }
            return false;
        }

        #endregion   
    }
}
