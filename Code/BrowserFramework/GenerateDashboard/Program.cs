using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDashboard
{
    /// <summary>
    /// This is the entry point for command line execution; typically used for debugging
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DlkCore.DlkGenerateDashboard.Execute();
        }
    }

    /// <summary>
    /// this is the entry point for execution from within an application
    /// </summary>
    public static class GenerateDashboardApi
    {
        /// <summary>
        /// this can be monitored to see if the dashboard generation is currently running
        /// </summary>
        public static Boolean mIsDashboardGenerationRunning
        {
            get
            {
                return _IsDashboardGenerationRunning;
            }
        }
        static Boolean _IsDashboardGenerationRunning = false;

        /// <summary>
        /// This generates a new dashboard
        /// </summary>
        public static void GenerateDashboard()
        {
            DlkCore.DlkGenerateDashboard.Execute();
            _IsDashboardGenerationRunning = false;
        }

        /// <summary>
        /// prepare for generating a new dashboard 
        /// </summary>
        public static void Initialize()
        {
            _IsDashboardGenerationRunning = true;
        }
    }
}