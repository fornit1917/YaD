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
    }

    public class AlbumDto
    {
        public String Image { get; set; }
        public String Artist { get; set; }
        public String Title { get; set; }
    }
}
