using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    class TracksLazyIterator : ITracksIterator
    {
        private IDataApiClient apiClient;
        private int[] trackIds;
        private Dictionary<int, TrackDto> tracksMap;

        public TracksLazyIterator(int[] trackIds, TrackDto[] tracks, IDataApiClient apiClient)
        {
            this.trackIds = trackIds;

            tracksMap = new Dictionary<int, TrackDto>();
            foreach (var track in tracks)
            {
                tracksMap[track.Id] = track;
            }
        }

        public TrackDto Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
