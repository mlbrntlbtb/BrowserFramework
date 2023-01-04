using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    public interface IKeywordAction
    {
        void Do(Action func);
    }
    public class SingleKeywordAction : IKeywordAction
    {
        public SingleKeywordAction()
        {
        }

        public void Do(Action func)
        {
            try
            {
                func();
            }
            catch
            {
                throw;
            }
        }
    }
    public class RetryKeywordAction : IKeywordAction
    {
        private int _retry;
        private TimeSpan _timeout;
        public int Retry
        {
            get { return this._retry; }
        }
        public TimeSpan TimeOut
        {
            get { return this._timeout; }
        }

        //uses default timeout and retry specified in the config
        public RetryKeywordAction()
        {
            _timeout = new TimeSpan(0, 0, 0, 0, DlkEnvironment.VerifyRetryDelay);
            _retry = DlkEnvironment.VerifyRetryCount;
        }

        //uses default retry and retry specified in the config
        public RetryKeywordAction(TimeSpan timeout)
        {
            _timeout = timeout;
            _retry = DlkEnvironment.VerifyRetryCount;
        }

        //uses default timeout and retry specified in the config
        public RetryKeywordAction(int retry)
        {
            _timeout = new TimeSpan(0, 0, 0, 0, DlkEnvironment.VerifyRetryDelay);
            _retry = retry;

        }

        //use user input timout and retry
        public RetryKeywordAction(TimeSpan timeout, int retry)
        {
            _timeout = timeout;
            _retry = retry;
        }


        public void Do(Action func)
        {
            int retryCount = this._retry - 1;
            bool attemptedRetry = false;
            do
            {
                try
                {
                    func();
                    break;
                }
                catch
                {
                    if (retryCount <= 0)
                    {
                        DlkLogger.LogInfo("Retry failed!");
                        throw;
                    }
                    else
                    {
                        DlkLogger.LogInfo("Attempting retry....");
                        attemptedRetry = true;
                        Thread.Sleep(this._timeout);
                    }
                }
            } while (retryCount-- > 0);

            if (attemptedRetry)
                DlkLogger.LogInfo("Retry Successful!");
        }
    }
    public class KeywordActionFactory
    {
        String[] _inputs;
        public String[] Inputs
        {
            get { return this._inputs; }
        }
        public KeywordActionFactory(params String[] inputs)
        {
            this._inputs = inputs;
        }

        public IKeywordAction GenerateAction()
        {
            IKeywordAction action;
            int msec = 0;
            int retry = 0;

            try
            {
                switch (this.Inputs[0])
                {
                    case "retry":
                        {
                            //msec = this.Inputs[0] == "" ? DlkEnvironment.VerifyRetryDelay : Convert.ToInt32(this.Inputs[0]);
                            //retry = this.Inputs[1] == "" ? DlkEnvironment.VerifyRetryCount : Convert.ToInt32(this.Inputs[1]);
                            msec = DlkEnvironment.VerifyRetryDelay;
                            retry = DlkEnvironment.VerifyRetryCount;
                            action = new RetryKeywordAction(new TimeSpan(0, 0, 0, 0, msec), retry);
                        }
                        break;
                    default:
                        action = new SingleKeywordAction();
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Invalid delay/retry parameter." + e.Message, e);
            }


            return action;
        }
    }
    public static class DlkControlExt
    {
        public static void PerformAction(this DlkBaseControl control, Action action, params String[] inputs)
        {
            if (action == null)
                throw new ArgumentNullException("action"); // slightly safer...

            var actionFactory = new KeywordActionFactory(inputs);
            actionFactory.GenerateAction()
                         .Do(action);
        }
    }
}
