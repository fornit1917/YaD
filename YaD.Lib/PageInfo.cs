using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class PageInfo
    {
        public PageType Type { get; set; }

        public String TracklistOwner { get; set; }
        public String TracklistTitle { get; set; }
        public String Image { get; set; }

        public ITrackList Tracks { get; set; }
    }
}
