using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    public interface ILinkScraper
    {
        IReadOnlyList<Uri> GetLinks(string html);
    }
}
