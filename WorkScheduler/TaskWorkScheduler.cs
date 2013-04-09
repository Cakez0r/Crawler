using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler
{
    public class TaskWorkScheduler : IWorkScheduler
    {
        private static Logger s_logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Maximum number of concurrently running tasks allowed.
        /// </summary>
        public int MaxConcurrentTasks
        {
            get;
            private set;
        }

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private object m_freeTaskLock = new object();
        private int m_freeTaskCount;

        /// <summary>
        /// Create a new work scheduler that will use Tasks to handle concurrency.
        /// </summary>
        /// <param name="maxConcurrentTasks">The maximum number of concurrently running tasks allowed</param>
        public TaskWorkScheduler(int maxConcurrentTasks)
        {
            if (maxConcurrentTasks <= 0)
            {
                throw new ArgumentException("Max concurrent tasks must be greater than 0.", "maxConcurrentTasks");
            }

            m_freeTaskCount = maxConcurrentTasks;
            MaxConcurrentTasks = maxConcurrentTasks;
        }

        /// <summary>
        /// Wait for a task to become available and then perform the specified action.
        /// </summary>
        public void DoWork(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                throw new InvalidOperationException("Cannot call DoWork() after AbortAll() or Dispose() have been called.");
            }

            //Spin until we can create a new task without exceeding the limit
            while (true)
            {
                if (m_freeTaskCount > 0)
                {
                    lock (m_freeTaskLock)
                    {
                        if (m_freeTaskCount > 0)
                        {
                            m_freeTaskCount--;
                            break;
                        }
                    }
                }

                //Yield so that we don't starve other threads
                Thread.Sleep(0);
            }

            s_logger.Debug("Starting up a task on thread id {0}.", Thread.CurrentThread.ManagedThreadId);

            Task workTask = new Task(action, _cancellationTokenSource.Token);
            workTask.ContinueWith(ReleaseTask);
            workTask.Start();
        }

        /// <summary>
        /// Whether there are any tasks currently executing
        /// </summary>
        public bool HasRunningJobs()
        {
            return m_freeTaskCount < MaxConcurrentTasks;
        }

        /// <summary>
        /// Stop all running tasks.
        /// </summary>
        public void AbortAll()
        {
            _cancellationTokenSource.Cancel();
        }

        private void ReleaseTask(Task t)
        {
            lock (m_freeTaskLock)
            {
                m_freeTaskCount++;
            }

            s_logger.Debug("Task complete");
        }
    }
}
