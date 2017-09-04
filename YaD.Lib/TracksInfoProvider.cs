using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class TracksInfoProvider : IEnumerable<TrackDto>
    {
        private int totalCount;
        private bool hasAllData = false;
        private ITracksIterator iterator;

        public int TotalCount { get { return totalCount; } }

        public TracksInfoProvider(List<TrackDto> tracks)
        {
            totalCount = tracks.Count;
            hasAllData = true;
            iterator = new TracksSimpleIterator(tracks);
        }

        public TracksInfoProvider(int[] trackIds, List<TrackDto> tracks, IDataApiClient apiClient)
        {
            totalCount = trackIds.Length;
            hasAllData = this.totalCount == tracks.Count;
            if (hasAllData)
            {
                iterator = new TracksSimpleIterator(tracks);
            }
            else
            {
                iterator = new TracksLazyIterator(trackIds, tracks, apiClient);
            }
        }

        public IEnumerator<TrackDto> GetEnumerator()
        {
            return iterator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return iterator;
        }
    }
}
