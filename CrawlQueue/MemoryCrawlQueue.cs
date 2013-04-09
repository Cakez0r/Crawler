using System;
using System.Collections.Concurrent;

namespace Crawler
{
    public class MemoryCrawlQueue : ICrawlQueue
    {
        private ConcurrentDictionary<string, byte> m_visitedUris = new ConcurrentDictionary<string, byte>();
        private ConcurrentQueue<Uri> m_crawlQueue = new ConcurrentQueue<Uri>();

        public int UrisRemaining
        {
            get { return m_crawlQueue.Count; }
        }

        public void Enqueue(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            m_crawlQueue.Enqueue(uri);
        }

        public Uri GetNext()
        {
            Uri uri = default(Uri);

            m_crawlQueue.TryDequeue(out uri);

            return uri;
        }
    }
}
