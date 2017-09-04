using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    class TrackListLazyLoading : ITrackList
    {
        private IDataApiClient apiClient;
        private int[] trackIds;
        private Dictionary<int, TrackDto> tracksMap;

        public TrackListLazyLoading(int[] trackIds, List<TrackDto> tracks, IDataApiClient apiClient)
        {
            this.trackIds = trackIds;

            tracksMap = new Dictionary<int, TrackDto>();
            foreach (var track in tracks)
            {
                tracksMap[track.Id] = track;
            }
        }

        public IEnumerator<TrackDto> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
