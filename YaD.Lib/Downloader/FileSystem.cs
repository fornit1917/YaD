using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace YaD.Lib
{
    public class FileSystem : IFileSystem
    {
        private static Regex illegalCharsRegex = new Regex(@"[\\/:*?""<>|]");

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

        public void CreateDirectoryForFilePath(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        public string ReplaceIllegalChars(string s)
        {
            return illegalCharsRegex.Replace(s, "_");
        }
    }
}
