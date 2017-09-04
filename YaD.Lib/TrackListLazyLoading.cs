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
        private LazyContainer container;

        public TrackListLazyLoading(int[] trackIds, List<TrackDto> tracks, IDataApiClient apiClient)
        {
            container = new LazyContainer(trackIds, tracks, apiClient);
        }

        public List<TrackDto> GetRange(int index, int count)
        {
            return container.GetRange(index, count);
        }

        public IEnumerator<TrackDto> GetEnumerator()
        {
            return new LazyIterator(container);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LazyIterator(container);
        }

        private class LazyContainer
        {
            private int[] trackIds;
            private Dictionary<int, TrackDto> tracksMap;
            private IDataApiClient apiClient;

            public int TotalCount => trackIds.Length;

            public LazyContainer(int[] trackIds, List<TrackDto> tracks, IDataApiClient apiClient)
            {
                this.trackIds = trackIds;
                this.apiClient = apiClient;

                tracksMap = new Dictionary<int, TrackDto>();
                foreach (var track in tracks)
                {
                    tracksMap[track.Id] = track;
                }
            }

            public List<TrackDto> GetRange(int index, int count)
            {
                int actualCount = GetActualCount(index, count);
                List<TrackDto> tracks = new List<TrackDto>(actualCount);
                for (int i = index; i < actualCount; ++i)
                {
                    int id = trackIds[i];
                    if (!IsLoaded(id))
                    {
                        LoadRange(index, actualCount);
                    }
                    tracks.Add(tracksMap[id]);
                }
                return tracks;
            }

            public TrackDto GetTrackByIndex(int index)
            {
                if (index < TotalCount)
                {
                    int trackId = trackIds[index];
                    if (!IsLoaded(trackId))
                    {
                        LoadRange(index, 10);
                    }
                    return tracksMap[trackId];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            private bool IsLoaded(int trackId)
            {
                return tracksMap.ContainsKey(trackId);
            }

            private void LoadRange(int index, int count)
            {
                int[] ids = GetIdsRange(index, count);
                List<TrackDto> tracks = apiClient.GetTracks(ids);
                foreach (var track in tracks)
                {
                    tracksMap[track.Id] = track;
                }
            }

            private int GetActualCount(int index, int count)
            {
                int actualCount = TotalCount - index;
                if (actualCount > count)
                {
                    actualCount = count;
                }
                return actualCount;
            }

            private int[] GetIdsRange(int index, int count)
            {
                int actualCount = GetActualCount(index, count);
                int[] ids = new int[actualCount];
                Array.Copy(trackIds, index, ids, 0, actualCount);
                return ids;
            }
        }

        private class LazyIterator : IEnumerator<TrackDto>
        {
            private LazyContainer container;
            private int position = -1;

            public LazyIterator(LazyContainer container)
            {
                this.container = container;
            }

            public TrackDto Current => container.GetTrackByIndex(position);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                ++position;
                return position < container.TotalCount;
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}
