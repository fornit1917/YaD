using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    class TracksSimpleIterator : ITracksIterator
    {
        private List<TrackDto> tracks;
        private IEnumerator<TrackDto> enumerator;

        public TracksSimpleIterator(List<TrackDto> tracks)
        {
            this.tracks = tracks;
            enumerator = tracks.GetEnumerator();
        }

        public TrackDto Current => enumerator.Current;

        object IEnumerator.Current => enumerator.Current;

        public bool MoveNext() => enumerator.MoveNext();

        public void Reset()
        {
            enumerator.Reset();
        }

        public void Dispose()
        {
            enumerator.Dispose();
        }
    }
}
