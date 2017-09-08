using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YaD.Lib;
using System.IO;
using System.Linq;

namespace YaD.Tests
{
    [TestClass]
    public class FileSystemTest
    {
        private string testDir = "testDir";
        private FileSystem fs;

        [TestInitialize]
        public void StartUp()
        {
            fs = new FileSystem();

            if (!Directory.Exists(testDir))
            {
                Directory.CreateDirectory(testDir);
            }

            Directory.CreateDirectory($"{testDir}\\empty");

            Directory.CreateDirectory($"{testDir}\\onlyFolders\\folder1\\folder1_1");
            Directory.CreateDirectory($"{testDir}\\onlyFolders\\folder1\\folder2");

            Directory.CreateDirectory($"{testDir}\\withFiles");
            File.Create($"{testDir}\\withFiles\\file.mp3").Close();

        }

        [TestCleanup]
        public void CleanUp()
        {
            DirectoryInfo directory = new DirectoryInfo(testDir);
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        [TestMethod]
        public void TestBaseDirIsEmpty()
        {
            Assert.IsTrue(fs.DirIsEmpty($"{testDir}\\empty"));
        }

        [TestMethod]
        public void TestBaseDirWithOtherDirIsNotEmpty()
        {
            Assert.IsFalse(fs.DirIsEmpty($"{testDir}\\onlyFolders"));
        }

        [TestMethod]
        public void TestBaseDirWithFilesIsNotEmpty()
        {
            Assert.IsFalse(fs.DirIsEmpty($"{testDir}\\withFiles"));
        }

        [TestMethod]
        public void TestCleanBaseDir()
        {
            String dir = $"{testDir}\\withFiles";
            Assert.IsTrue(Directory.EnumerateFileSystemEntries(dir).Any());

            fs.CleanDir(dir);
            Assert.IsFalse(Directory.EnumerateFileSystemEntries(dir).Any());
        }

        [TestMethod]
        public void TestIsDownloadedTrackForNotExistedFile()
        {
            Assert.IsFalse(fs.IsDownloadedTrack($"{testDir}\\withFiles\\notExistedFile.mp3", new TrackDto { FileSize = 3 }));
        }

        [TestMethod]
        public void TestIsDownloadedTrackForExistedFileWithSmallSize()
        {
            Assert.IsFalse(fs.IsDownloadedTrack($"{testDir}\\withFiles\\file.mp3", new TrackDto { FileSize = 3 }));
        }

        [TestMethod]
        public void TestIsDownloadedTrack()
        {
            File.WriteAllText($"{testDir}\\withFiles\\fileBig.mp3", "some content");
            Assert.IsTrue(fs.IsDownloadedTrack($"{testDir}\\withFiles\\fileBig.mp3", new TrackDto { FileSize = 3 }));
        }
    }
}
