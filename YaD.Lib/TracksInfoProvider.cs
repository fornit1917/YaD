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

        public TracksInfoProvider(TrackDto[] tracks)
        {
            totalCount = tracks.Length;
            hasAllData = true;
            iterator = new TracksSimpleIterator(tracks);
        }

        public TracksInfoProvider(int[] trackIds, TrackDto[] tracks, IDataApiClient apiClient)
        {
            totalCount = trackIds.Length;
            hasAllData = this.totalCount == tracks.Length;
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
