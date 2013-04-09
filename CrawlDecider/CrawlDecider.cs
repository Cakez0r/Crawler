using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    public class CrawlDecider
    {
        private List<CrawlRule> m_crawlRules = new List<CrawlRule>();
        public IReadOnlyCollection<CrawlRule> CrawlRules
        {
            get { return m_crawlRules.AsReadOnly(); }
        }

        public void AddRule(CrawlRule rule)
        {

        }

        public void RemoveRule(CrawlRule rule)
        {

        }

        public bool DoesUriPassAllRules(Uri uri)
        {
            return false;
        }
    }
}
