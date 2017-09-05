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
            fs.BaseDir = $"{testDir}\\empty";
            Assert.IsTrue(fs.BaseDirIsEmpty);
        }

        [TestMethod]
        public void TestBaseDirWithOtherDirIsNotEmpty()
        {
            fs.BaseDir = $"{testDir}\\onlyFolders";
            Assert.IsFalse(fs.BaseDirIsEmpty);
        }

        [TestMethod]
        public void TestBaseDirWithFilesIsNotEmpty()
        {
            fs.BaseDir = $"{testDir}\\withFiles";
            Assert.IsFalse(fs.BaseDirIsEmpty);
        }

        [TestMethod]
        public void TestCleanBaseDir()
        {
            String dir = $"{testDir}\\withFiles";
            Assert.IsTrue(Directory.EnumerateFileSystemEntries(dir).Any());

            fs.BaseDir = dir;
            fs.CleanBaseDir();
            Assert.IsFalse(Directory.EnumerateFileSystemEntries(dir).Any());
        }

        [TestMethod]
        public void TestIsDownloadedTrackForNotExistedFile()
        {
            fs.BaseDir = $"{testDir}\\withFiles";
            Assert.IsFalse(fs.IsDownloadedTrack("notExistedFile.mp3", new TrackDto { FileSize = 3 }));
        }

        [TestMethod]
        public void TestIsDownloadedTrackForExistedFileWithSmallSize()
        {
            fs.BaseDir = $"{testDir}\\withFiles";
            Assert.IsFalse(fs.IsDownloadedTrack("file.mp3", new TrackDto { FileSize = 3 }));
        }

        [TestMethod]
        public void TestIsDownloadedTrack()
        {
            fs.BaseDir = $"{testDir}\\withFiles";
            File.WriteAllText($"{fs.BaseDir}\\fileBig.mp3", "some content");
            Assert.IsTrue(fs.IsDownloadedTrack("fileBig.mp3", new TrackDto { FileSize = 3 }));
        }
    }
}
