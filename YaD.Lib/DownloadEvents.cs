using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class DownloadProgressEventArgs : EventArgs
    {

    }

    public delegate void DownloadProgressHandler(Object sender, DownloadProgressEventArgs e);
}
