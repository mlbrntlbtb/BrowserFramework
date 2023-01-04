using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using Recorder.Presenter;
using Recorder.Model;

namespace Recorder.View
{
    public interface IMainView
    {
        #region PROPERTIES
        MainPresenter Presenter { get; }
        List<Recorder.Model.Action> Actions { get; set; }
        List<Model.Variable> Variables { get; }
        Recorder.Model.Action NewAction { get; set; }
        Recorder.Model.Step NewStep { get; set; }
        int CurrentBlock { get; set; }
        Model.Enumerations.VerifyType VerifyType { get; set; }
        Model.Enumerations.ViewStatus ViewStatus { get; set; }
        Model.Enumerations.KeywordType KeywordType { get; set; }
        Model.Enumerations.GetValueType GetValueType { get; set; }
        #endregion

        #region METHODS
        Dispatcher UIDispatcher { get; }
        void Initialize();
        void RecordingStarted();
        void RecordingStopped();
        void VariablesUpdated();
        void VerifyStarted();
        void ResetVerifyMode();
        void OnClose(object sender, System.ComponentModel.CancelEventArgs e);
        #endregion
    }
}
