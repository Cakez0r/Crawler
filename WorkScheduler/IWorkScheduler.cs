using System;

namespace Crawler
{
    public interface IWorkScheduler
    {
        void DoWork(Action work);

        bool HasRunningJobs();

        void AbortAll();
    }
}
