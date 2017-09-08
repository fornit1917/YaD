using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace YaD.Lib
{
    class FileDownloader : IFileDownloader
    {
        public void Download(string url, string dest, FileDownloadProgressHandler OnDownloadProgess = null)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            using (Stream s = req.GetResponse().GetResponseStream())
            {
                using (FileStream fs = File.Create(dest)) {
                    byte[] buffer = new byte[131072];
                    int count = s.Read(buffer, 0, buffer.Length);
                    long sum = 0;
                    while (count > 0)
                    {
                        sum += count;
                        fs.Write(buffer, 0, count);
                        OnDownloadProgess?.Invoke(this, new FileDownloadProgressEventArgs() { BytesDownloaded = sum });

                        count = s.Read(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}
