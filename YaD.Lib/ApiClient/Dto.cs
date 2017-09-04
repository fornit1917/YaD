using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class PlaylistDto
    {
        public String Image { get; set; }
        public String Owner { get; set; }
        public String Title { get; set; }
        public int[] TrackIds { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class AlbumDto
    {
        public String Image { get; set; }
        public String Artist { get; set; }
        public String Title { get; set; }
        public int Year { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class UserDto
    {
        public String Login { get; set; }
        public String Name { get; set; }
        public String Image { get; set; }
        public int[] TrackIds { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class ArtistDto
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public int[] TrackIds { get; set; }
        public List<TrackDto> Tracks { get; set; }
    }

    public class TrackDto
    {
        public int Id { get; set; }
        public String Artist { get; set; }
        public String Title { get; set; }
        public int AlbumYear { get; set; }
        public String AlbumTitle { get; set; }
        public int FileSize { get; set; }
    }
}
