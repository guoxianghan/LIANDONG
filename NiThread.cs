using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OPCDA
{
    public class NiThread : IDisposable
    {
        public delegate void ThreadMethod();

        private SemaphoreSlim threadStart;
        private SemaphoreSlim threadEnd;
        private SemaphoreSlim threadTrigger;
        private volatile bool recoverable = true;
        private volatile bool threadShouldStop;
        private volatile bool running;

        private Thread theThread;
        private readonly ThreadMethod theRuntimeDelegate;
        private readonly ThreadMethod theStoppingDelegate;
        private readonly int threadTimeoutInMs;
        public NiThread(ThreadMethod threadMethodStart,
                        ThreadMethod threadMethodStop,
                        string threadName,
                        int threadTimeoutInMs)
        {
            this.threadTimeoutInMs = threadTimeoutInMs;
            threadStart = new SemaphoreSlim(0, 1);
            threadEnd = new SemaphoreSlim(0, 1);
            threadTrigger = new SemaphoreSlim(0, 1);

            theRuntimeDelegate = threadMethodStart;
            theStoppingDelegate = threadMethodStop;
            ThreadStart delegateHelp = StartThreadExecute;

            if (delegateHelp != null)
            {
                theThread = new Thread(delegateHelp)
                {
                    IsBackground = true
                };
            }
        }

        public void Disposed()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (threadStart != null)
                {
                    threadStart.Dispose();
                    threadStart = null;
                }
                if (threadEnd != null)
                {
                    threadEnd.Dispose();
                    threadEnd = null;
                }
                if (threadTrigger != null)
                {
                    threadTrigger.Dispose();
                    threadTrigger = null;
                }
            }
        }
        private void StartThreadExecute()
        {
            int timeoutValue = threadTimeoutInMs;

            threadStart.Release();
            running = true;

            while (!threadShouldStop)
            {
                try
                {
                    theRuntimeDelegate.Invoke();

                    if (timeoutValue > 0)
                    {
                        threadTrigger.Wait(timeoutValue);
                    }
                    if (threadShouldStop)
                    {
                        break;
                    }
                }
                catch (ThreadAbortException ex)
                {
                    Thread.ResetAbort();
                    break;
                }
                catch (Exception)
                {
                    if (recoverable && !threadShouldStop)
                    {
                        Thread.Sleep(100);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            threadEnd.Release();
            running = false;
        }
        public void Release()
        {
            if (threadTrigger != null)
            {
                threadTrigger.Release();
            }
        }
        public void Start()
        {
            try
            {
                theThread.Start();
                threadStart.Wait();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Stop(int timeoutInMs)
        {
            if (theThread != null)
            {
                threadShouldStop = true;
                threadTrigger.Release();

                if (theStoppingDelegate != null)
                {
                    theStoppingDelegate.Invoke();
                }

                if (!threadEnd.Wait(timeoutInMs))
                {
                    theThread.Abort();
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
