using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace GovWinLib.DlkUtility
{
    public class DlkMethodCallHelper
    {
        public DlkMethodCallHelper() { }

        public static void CallWithTimeout<T1>(Action<T1> action, int timeoutSecs, T1 input)
        {
            var timeoutMilliseconds = timeoutSecs * 1000;
            Thread threadToKill = null;
            Action<T1> wrappedAction = (T1 obj) =>
            {
                threadToKill = Thread.CurrentThread;
                action(obj);
            };

            IAsyncResult result = wrappedAction.BeginInvoke(input, null, null);
            if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
            {
                wrappedAction.EndInvoke(result);
            }
            else
            {
                threadToKill.Abort();
                throw new TimeoutException(string.Format("Timeout call in {0}.", action.Method.Name));
            }
        }
    }
}
