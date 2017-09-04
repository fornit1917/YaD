﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    class TrackListSimple : ITrackList
    {
        private List<TrackDto> tracks;

        public TrackListSimple(List<TrackDto> tracks)
        {
            this.tracks = tracks;
        }

        public IEnumerator<TrackDto> GetEnumerator()
        {
            return tracks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tracks.GetEnumerator();
        }
    }
}