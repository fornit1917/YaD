using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    class TrackListFactory
    {
        public static ITrackList Create(List<TrackDto> tracks)
        {
            return new TrackListSimple(tracks);
        }

        public static ITrackList Create(int[] trackIds, List<TrackDto> tracks, IDataApiClient apiClient)
        {
            if (tracks.Count < trackIds.Length)
            {
                return new TrackListLazyLoading(trackIds, tracks, apiClient);
            }
            else
            {
                return new TrackListSimple(tracks);
            }
        }
    }
}
