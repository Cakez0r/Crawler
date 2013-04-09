using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    public interface ICrawlQueue
    {
        int UrisRemaining { get; }

        void Enqueue(Uri uri);

        Uri GetNext();
    }
}
