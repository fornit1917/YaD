using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public interface IDataApiClient
    {
        Task<PlaylistDto> GetPlaylist(String userId, String playlistId);
        Task<AlbumDto> GetAlbum(String albumId);
        Task<UserDto> GetUser(String userId);
        Task<ArtistDto> GetArtist(String artistId);
    }
}
