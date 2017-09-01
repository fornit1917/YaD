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
        private TrackDto[] tracks;

        public TracksSimpleIterator(TrackDto[] tracks)
        {
            this.tracks = tracks;
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
