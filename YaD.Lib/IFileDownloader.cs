using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public interface IFileDownloader
    {
        void Download(String url, String dest, DownloadProgressHandler OnDownloadProgess = null);
    }
}
