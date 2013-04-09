using System;
using System.Collections.Generic;

namespace Crawler
{
    public interface ILinkScraper
    {
        IReadOnlyList<Uri> GetLinks(string html);
    }
}
