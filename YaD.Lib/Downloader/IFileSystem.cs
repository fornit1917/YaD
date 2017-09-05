using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public interface IFileSystem
    {
        String BaseDir { get; set; }
        bool BaseDirIsEmpty { get; }

        bool IsDownloadedTrack(String relPath, TrackDto track);
        void CleanBaseDir();
    }
}
