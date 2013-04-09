using System;

namespace Crawler
{
    public class CrawlRule
    {
        private Func<Uri, bool> m_validator;

        public bool Enabled { get; set; }

        public CrawlRule(Func<Uri, bool> validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            m_validator = validator;
            Enabled = true;
        }

        public bool Validate(Uri uri)
        {
            return Enabled && m_validator(uri);
        }
    }
}
