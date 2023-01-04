using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace AdvancedScheduler
{
    sealed class DlkSingleInstanceLock :IDisposable
    {
        private readonly Mutex _mutex = null;
        private bool _hasAcquiredExclusiveLock, _disposed;

        public DlkSingleInstanceLock(string AppGUID)
        {
            _mutex = CreateMutex(AppGUID);
        }

        ~DlkSingleInstanceLock()
        {
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool TryAcquireExclusiveLock()
        {
            try
            {
                if (!_mutex.WaitOne(1000, false))
                    return false;
            }
            catch (AbandonedMutexException)
            {
                // Abandoned mutex. Do nothing
            }

            return _hasAcquiredExclusiveLock = true;
        }

        //Release the Mutex
        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed && _mutex != null)
            {
                try
                {
                    if (_hasAcquiredExclusiveLock)
                        _mutex.ReleaseMutex();

                    _mutex.Dispose();
                }
                finally
                {
                    _disposed = true;
                }
            }
        }

        //Creates the mutex
        private static Mutex CreateMutex(string AppGuid)
        {
            string MutexId = string.Format("Global\\{{{0}}}", AppGuid);

            var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var allowEveryoneRule = new MutexAccessRule(sid,
                MutexRights.FullControl, AccessControlType.Allow);

            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            var mutex = new Mutex(false, MutexId);
            mutex.SetAccessControl(securitySettings);

            return mutex;
        }
    }
}
