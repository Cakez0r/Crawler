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
            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }

            m_crawlRules.Add(rule);
        }

        public void RemoveRule(CrawlRule rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException("rule");
            }

            m_crawlRules.Remove(rule);
        }

        public bool DoesUriPassAllRules(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            return m_crawlRules.All(r => r.Validate(uri));
        }
    }
}
