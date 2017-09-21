using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class PlaylistTracksDto
    {
        public String Image { get; set; }
        public String Owner { get; set; }
        public String Title { get; set; }
        public int[] TrackIds { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class AlbumTracksDto
    {
        public String Image { get; set; }
        public String Artist { get; set; }
        public String Title { get; set; }
        public int Year { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class UserTracksDto
    {
        public String Login { get; set; }
        public String Name { get; set; }
        public String Image { get; set; }
        public int[] TrackIds { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class ArtistTracksDto
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public int[] TrackIds { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class AlbumVolumeInfoDto
    {
        public int Year { get; set; }
        public String Title { get; set; }
        public int VolumeNumber { get; set; }
    }

    public class TrackDto
    {
        public int Id { get; set; }
        public String Artist { get; set; }
        public String Title { get; set; }
        public int AlbumYear { get; set; }
        public String AlbumTitle { get; set; }
        public int FileSize { get; set; }
        public TrackPosition Position { get; set; }
    }

    public class TrackPosition
    {
        public int Volume { get; set; }
        public int Index { get; set; }
    }
}
