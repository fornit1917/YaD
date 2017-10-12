using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaD.Lib;
using YaD.Tests.Mocks;

namespace YaD.Tests
{
    [TestClass]
    public class TrackListLazyLoadingTest
    {
        [TestMethod]
        public void TestLazyLoading()
        {
            var initialTracks = new List<TrackDto>();
            initialTracks.Add(new TrackDto() { Id = 1, Title = "1" });

            var list = TrackListFactory.Create(
                new int[5] { 1, 2, 3, 4, 5 },
                initialTracks,
                new DataApiClientMock()
            );

            Assert.AreEqual(5, list.TotalCount);
            var tracks = list.ToList<TrackDto>();

            Assert.AreEqual(1, tracks[0].Id);
            Assert.AreEqual("1", tracks[0].Title);
            Assert.AreEqual(2, tracks[1].Id);
            Assert.AreEqual("2", tracks[1].Title);
            Assert.AreEqual(3, tracks[2].Id);
            Assert.AreEqual("3", tracks[2].Title);
            Assert.AreEqual(4, tracks[3].Id);
            Assert.AreEqual("4", tracks[3].Title);
            Assert.AreEqual(5, tracks[4].Id);
            Assert.AreEqual("5", tracks[4].Title);
        }
    }
}
