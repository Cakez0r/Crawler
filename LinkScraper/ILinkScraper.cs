using System;
using System.Collections.Generic;

namespace Crawler
{
    public interface ILinkScraper
    {
        IReadOnlyList<Uri> GetLinks(Uri parentUri, string html);
    }
}
