using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public interface IFileSystem
    {
        bool DirIsEmpty(String path);
        bool IsDownloadedTrack(String path, TrackDto track);
        void CleanDir(String path);
    }
}
