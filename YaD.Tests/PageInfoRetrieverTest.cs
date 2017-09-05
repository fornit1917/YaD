using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YaD.Lib;
using YaD.Tests.Mocks;

namespace YaD.Tests
{
    [TestClass]
    public class PageInfoRetrieverTest
    {
        private static PageInfoRetriever pageInfoRetriever = new PageInfoRetriever(new DataApiClientMock());

        [TestMethod]
        public void TestGetPageInfoForInvalidUrl()
        {
            Assert.IsNull(pageInfoRetriever.GetPageInfoAsync("invalid url").Result);
            Assert.IsNull(pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru").Result);
            Assert.IsNull(pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/genre/all/tracks").Result);
        }

        [TestMethod]
        public void TestGetPageInfoForAlbum()
        {
            PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/album/123").Result;
            Assert.IsNotNull(pageInfo);
            Assert.AreEqual("Album", pageInfo.TracklistTitle);
            Assert.AreEqual("Artist", pageInfo.TracklistOwner);
            Assert.AreEqual("http://test.com/123", pageInfo.Image);
            CheckTracks(pageInfo.Tracks);
        }

        
        [TestMethod]
        public void TestGetPageInfoForArtist()
        {
            PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/artist/12345").Result;
            Assert.IsNotNull(pageInfo);
            Assert.AreEqual("All tracks", pageInfo.TracklistTitle);
            Assert.AreEqual("Artist", pageInfo.TracklistOwner);
            Assert.AreEqual("http://test.com/12345", pageInfo.Image);
            CheckTracks(pageInfo.Tracks);
        }

        [TestMethod]
        public void TestGetPageInfoForAllUserTracks()
        {
            PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/users/user.name/tracks").Result;
            Assert.IsNotNull(pageInfo);
            Assert.AreEqual("All user tracks", pageInfo.TracklistTitle);
            Assert.AreEqual("UserName / UserLogin", pageInfo.TracklistOwner);
            CheckTracks(pageInfo.Tracks);
        }
        

        [TestMethod]
        public void TestGetPageInfoForPlaylist()
        {
            PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/users/user.name/playlists/12345").Result;
            Assert.IsNotNull(pageInfo);
            Assert.AreEqual("Playlist", pageInfo.TracklistTitle);
            Assert.AreEqual("User", pageInfo.TracklistOwner);
            Assert.AreEqual("http://test.com/user.name/12345", pageInfo.Image);
            CheckTracks(pageInfo.Tracks);
        }

        private void CheckTracks(ITrackList tracks)
        {
            Assert.AreEqual(5, tracks.TotalCount);
            int i = 1;
            foreach (var t in tracks)
            {
                Assert.AreEqual(i, t.Id);
                Assert.AreEqual(i.ToString(), t.Title);
                i++;
            }
        }
    }
}
