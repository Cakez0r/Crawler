using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new ArgumentException("Validator cannot be null", "validator");
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
