using System;

namespace Crawler
{
    public interface IPageDownloader
    {
        string DownloadPage(Uri uri);
    }
}
