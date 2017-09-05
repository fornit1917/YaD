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
        public string BaseDir { get; set; }

        public bool BaseDirIsEmpty => !Directory.Exists(BaseDir) || !Directory.EnumerateFileSystemEntries(BaseDir).Any();

        public void CleanBaseDir()
        {
            DirectoryInfo directory = new DirectoryInfo(BaseDir);
            foreach (FileInfo file in directory.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories())
            {
                subDirectory.Delete(true);
            }
        }

        public bool IsDownloadedTrack(string relPath, TrackDto track)
        {
            FileInfo file = new FileInfo($"{BaseDir}\\{relPath}");
            return file.Exists && file.Length >= track.FileSize;
        }
    }
}
