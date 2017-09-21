using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class FileDownloadProgressEventArgs : EventArgs
    {
        public long BytesDownloaded { get; set; }
    }

    public delegate void FileDownloadProgressHandler(Object sender, FileDownloadProgressEventArgs e);

    public class TrackDownloadProgressEventArgs : EventArgs
    {
        public long BytesDownloaded { get; set; }
        public TrackDto Track { get; set; }

        public TrackDownloadProgressEventArgs(TrackDto track, long bytesDownloaded)
        {
            Track = track;
            BytesDownloaded = bytesDownloaded;
        }
    }

    public delegate void TrackDownloadProgressHandler(Object sender, TrackDownloadProgressEventArgs e);
}
