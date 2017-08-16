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
            Assert.AreEqual("123", pageInfo.TracklistTitle);
            Assert.AreEqual("Some Artist", pageInfo.TracklistOwner);
            Assert.AreEqual("http://test.com/123", pageInfo.Image);
        }

        /*
        [TestMethod]
        public void TestGetPageInfoForArtist()
        {
            PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/artist/12345").Result;
            Assert.IsNotNull(pageInfo);
        }

        [TestMethod]
        public void TestGetPageInfoForAllUserTracks()
        {
            PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/users/user.name/tracks").Result;
            Assert.IsNotNull(pageInfo);
        }
        */

        [TestMethod]
        public void TestGetPageInfoForPlaylist()
        {
            PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync("https://music.yandex.ru/users/user.name/playlists/123456").Result;
            Assert.IsNotNull(pageInfo);
        }
    }
}
