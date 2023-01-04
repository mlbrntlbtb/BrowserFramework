using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using Recorder.View;

namespace Recorder
{
    public partial class Main : Form, IMainView
    {
        private Presenter.MainPresenter m_Presenter;
        private List<Model.Action> m_ActionList = new List<Model.Action>();
        private Model.Action m_NewAction;
        private Model.Step m_NewStep;
        private bool m_IsRecording = false;
        private int m_CurrentBlock = 0;

        public Main()
        {
            InitializeComponent();
        }


        public Presenter.MainPresenter Presenter
        {
            get
            {
                return m_Presenter;
            }
        }

        #region EVENTS
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_IsRecording)
                {
                    m_Presenter.StopInspecting();
                }
                else
                {
                    m_Presenter.StartInspecting();
                }
            }
            catch
            {

            }
        }
        #endregion  

        private void Main_Load(object sender, EventArgs e)
        {
            Initialize();
            Presenter.StartBrowser();
        }


        public void Initialize()
        {
            m_Presenter = new Presenter.MainPresenter(this);
        }



        public Model.Action NewAction
        {
            get
            {
                return m_NewAction;
            }
            set
            {
                m_NewAction = value;

                /* Deep copy reference types */
                Model.Control controlToAddToMyList = new Model.Control();
                controlToAddToMyList.Descriptor = m_NewAction.Target.Descriptor;
                controlToAddToMyList.Type = m_NewAction.Target.Type;
                controlToAddToMyList.Value = m_NewAction.Target.Value;

                Model.Action actionToAddToMyList = new Model.Action();
                actionToAddToMyList.Target = controlToAddToMyList;
                actionToAddToMyList.Type = m_NewAction.Type;
                actionToAddToMyList.Screen = m_NewAction.Screen;
                actionToAddToMyList.Block = m_NewAction.Block;

                ListViewItem itm = new ListViewItem(Enum.GetName(typeof(Model.Enumerations.ActionType), m_NewAction.Type));
                itm.SubItems.Add(m_NewAction.Screen.Name);
                itm.SubItems.Add(Enum.GetName(typeof(Model.Enumerations.ControlType), m_NewAction.Target.Type));
                itm.SubItems.Add(Enum.GetName(typeof(Model.Enumerations.DescriptorType), m_NewAction.Target.Descriptor.Type));
                itm.SubItems.Add(m_NewAction.Target.Descriptor.Value);
                itm.SubItems.Add(m_NewAction.Target.Value);
                itm.SubItems.Add(m_NewAction.Block.ToString());

                if (m_CurrentBlock > 1 && m_CurrentBlockChanged && chkPreview.Checked)
                {
                    m_Presenter.StartConverting();
                }

                /* Cache */
                Actions.Add(actionToAddToMyList);

                itm.Tag = actionToAddToMyList;
                listView1.Items.Add(itm);
                //itm.Selected = true;
                listView1.Refresh();
                itm.EnsureVisible();

                /* Reduduncy for fault keystroke and combobox */
                if (!HasLooped && 
                    (actionToAddToMyList.Target.Type == Model.Enumerations.ControlType.ComboBox 
                    || actionToAddToMyList.Type == Model.Enumerations.ActionType.Keystroke))
                {
                    HasLooped = true;
                    m_NewAction.Target.Value = m_Presenter.GetControlValue(m_NewAction.Target);
                    NewAction = m_NewAction;
                }
                else
                {
                    HasLooped = false;
                }

                // update status UI
                txtNewAction.Clear();
                txtNewAction.Text = "[" + Enum.GetName(typeof(Model.Enumerations.ActionType), actionToAddToMyList.Type).ToUpper() + "]" +
                    "[@" + actionToAddToMyList.Screen.Name +
                    "{" + Enum.GetName(typeof(Model.Enumerations.ControlType), actionToAddToMyList.Target.Type) + ": " +
                    Enum.GetName(typeof(Model.Enumerations.DescriptorType), actionToAddToMyList.Target.Descriptor.Type) + "=" +
                    actionToAddToMyList.Target.Descriptor.Value +
                    ", Value=" + actionToAddToMyList.Target.Value + "}]";
            }
        }

        private bool HasLooped = false;

        //private string GetDescriptor()


        public List<Model.Action> Actions
        {
            get
            {
                return m_ActionList;
            }
            set
            {
                m_ActionList = value;
            }
        }


        public void RecordingStopped()
        {
            button1.Text = "Record";
            m_IsRecording = false;
        }

        public void RecordingStarted()
        {
            m_IsRecording = true;
            button1.Text = "Stop";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            m_ActionList.Clear();
            txtNewAction.Clear();
            CurrentBlock = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.Height > 75)
            {
                this.Width = 1024;
                this.Height = 70;
            }
            else
            {
                this.Width = 1024;
                this.Height = 768;
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            this.button3.Enabled = !(this.WindowState == FormWindowState.Maximized);
        }


        public Model.Step NewStep
        {
            get
            {
                return m_NewStep;
            }
            set
            {
                if (m_NewStep == null)
                {
                    // do nothing
                }
                else if (value == null || value.Screen == "Unknown" || string.IsNullOrEmpty(value.Screen)
                    || (value.Screen ==  m_NewStep.Screen && value.Control == m_NewStep.Control && value.Keyword == m_NewStep.Keyword
                    && value.Parameters == m_NewStep.Parameters))
                {
                    return;
                }
                m_NewStep = value;

                ListViewItem itm = new ListViewItem((listView2.Items.Count + 1).ToString());
                itm.SubItems.Add(m_NewStep.Execute);
                itm.SubItems.Add(m_NewStep.Screen);
                itm.SubItems.Add(m_NewStep.Control);
                itm.SubItems.Add(m_NewStep.Keyword);
                itm.SubItems.Add(m_NewStep.Parameters);
                itm.SubItems.Add(m_NewStep.Delay);

                Model.Step stepToCache = new Model.Step();
                stepToCache.Execute = m_NewStep.Execute;
                stepToCache.Screen = m_NewStep.Screen;
                stepToCache.Control = m_NewStep.Control;
                stepToCache.Keyword =  m_NewStep.Keyword;
                stepToCache.Parameters = m_NewStep.Parameters;
                stepToCache.Delay = m_NewStep.Delay;

                itm.Tag = stepToCache;
                listView2.Items.Add(itm);
                listView2.Refresh();
                itm.EnsureVisible();
            }
        }

        public int CurrentBlock
        {
            get
            {
                return m_CurrentBlock;
            }
            set
            {
                //if (!m_Converting && value > 1)
                //{
                //    m_Presenter.StartConverting();
                //    m_Converting = true;
                //}
                m_CurrentBlockChanged = false;
                if (value != m_CurrentBlock)
                {
                    m_CurrentBlockChanged = true;
                }
                m_CurrentBlock = value;
            }
        }

        private bool m_CurrentBlockChanged = false;

        private void button4_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Presenter.KillBrowser();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DlkTest myTest = new DlkTest(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "template.dat"));

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            //sfd.InitialDirectory = System.IO.Path.GetDirectoryName(this.Title.Replace("Test Editor : ", ""));
            sfd.FileName = "NewTest.xml";
            sfd.Filter = "Xml Files (*.xml)|*.xml";
            sfd.AddExtension = true;
            sfd.DefaultExt = ".xml";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                myTest.mTestName = sfd.FileName;
                myTest.mTestDescription = "Auto-generated test";
                //myTest.mTestSteps.Clear();
                foreach (ListViewItem lvi in listView2.Items)
                {
                    DlkTestStepRecord step = new DlkTestStepRecord();
                    step.mStepNumber = Convert.ToInt32(lvi.Text);
                    step.mExecute = true;
                    step.mScreen = lvi.SubItems[2].Text;
                    step.mControl = lvi.SubItems[3].Text;
                    step.mKeyword = lvi.SubItems[4].Text;
                    List<string> prms = new List<string>();
                    prms.Add(lvi.SubItems[5].Text);
                    step.mParameters = prms;
                    step.mStepDelay = 0;
                    step.mStepElapsedTime = myTest.mTestSteps.First().mStepElapsedTime;
                    step.mStepEnd = myTest.mTestSteps.First().mStepEnd;
                    step.mStepLogMessages = new List<DlkLoggerRecord>();
                    step.mStepStart = myTest.mTestSteps.First().mStepStart;
                    step.mStepStatus = myTest.mTestSteps.First().mStepStatus;

                    myTest.mTestSteps.Add(step);
                }

                myTest.mTestSteps.RemoveAt(0);
                myTest.WriteTest(sfd.FileName, true);
                MessageBox.Show("Saved: " + myTest.mTestName);
            }

        }
    }
}
