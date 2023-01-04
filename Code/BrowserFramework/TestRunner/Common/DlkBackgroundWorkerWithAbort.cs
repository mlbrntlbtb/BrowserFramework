using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

namespace TestRunner.Common
{
    public class DlkBackgroundWorkerWithAbort : BackgroundWorker
    {
        private Thread mWorkerThread;
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            mWorkerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch(ThreadAbortException)
            {
                e.Cancel = true;
                Thread.ResetAbort();
            }
        }

        public void Abort()
        {
            if (mWorkerThread != null)
            {
                mWorkerThread.Abort();
                mWorkerThread = null;
            }
        }
    }
}
