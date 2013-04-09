using System;

namespace Crawler
{
    public interface ICrawlQueue
    {
        int UrisRemaining { get; }

        void Enqueue(Uri uri);

        Uri GetNext();
    }
}
