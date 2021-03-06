using System.Threading;

namespace Hibernator.General
{
    public class BaseWorker
    {
        protected readonly Watcher _idleWatcher;
        protected  Thread _watcherThread;
        protected  Thread _listenerThread;
        protected readonly ManualResetEvent _suspendEvent = new ManualResetEvent(true);

        public BaseWorker()
        {

        }

        public BaseWorker(Watcher idleWatcher)
        {
            _idleWatcher = idleWatcher;
        }

        public void Update(int timeout)
        {
            _idleWatcher.UpdateParams(timeout);
        }

        public void Work()
        {
            _watcherThread.Start();
            if (_listenerThread != null)
            {
                _listenerThread.Start();
            }
            
        }

        public void CheckIdleTime()
        {
            while (true)
            {
                _suspendEvent.WaitOne(Timeout.Infinite);
                _idleWatcher.Watch();
            }
        }


    }
}