using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class YandexDataApiClient : IDataApiClient
    {
        public async Task<AlbumDto> GetAlbum(string albumId)
        {
            throw new NotImplementedException();
        }

        public async Task<PlaylistDto> GetPlaylist(string userId, string playlistId)
        {
            throw new NotImplementedException();
        }
    }
}
