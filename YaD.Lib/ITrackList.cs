using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public interface ITrackList : IEnumerable<TrackDto>
    {
        int TotalCount { get; }
        List<TrackDto> GetRange(int index, int count);
    }
}
