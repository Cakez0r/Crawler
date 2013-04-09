using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    public interface IWorkScheduler
    {
        void DoWork(Action work);

        bool HasRunningJobs();

        void AbortAll();
    }
}
