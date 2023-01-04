using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;
using Recorder.View;
using Recorder.Model;

namespace Recorder.Presenter
{
    static class AppClassFactory
    {
        public static MainPresenter GetMainPresenter(IMainView view)
        {
            var aut = DlkTestRunnerSettingsHandler.ApplicationUnderTest;
            switch (aut.ID)
            {
                case "1":  // CP 7.0.1
                case "2":  // CP 7.1.1
                case "11": // TE
                case "12": // BnP
                    return new CP_MainPresenter(view);
                default:
                    return null;
            }

        }

        public static Inspector GetInspector()
        {
            var aut = DlkTestRunnerSettingsHandler.ApplicationUnderTest;
            switch (aut.ID)
            {
                case "1":  // CP 7.0.1
                case "2":  // CP 7.1.1
                case "11": // TE
                case "12": // BnP
                    return new CP_Inspector();
                default:
                    return null;
            }
        }
    }
}
