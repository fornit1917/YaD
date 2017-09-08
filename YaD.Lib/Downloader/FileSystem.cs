using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YaD.Lib
{
    class FileSystem : IFileSystem
    {
        public bool DirIsEmpty(String path) => !Directory.Exists(path) || !Directory.EnumerateFileSystemEntries(path).Any();

        public void CleanDir(String path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (FileInfo file in directory.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories())
            {
                subDirectory.Delete(true);
            }
        }

        public bool IsDownloadedTrack(string path, TrackDto track)
        {
            FileInfo file = new FileInfo(path);
            return file.Exists && file.Length >= track.FileSize;
        }
    }
}
