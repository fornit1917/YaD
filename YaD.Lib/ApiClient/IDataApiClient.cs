using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public interface IDataApiClient
    {
        Task<PlaylistDto> GetPlaylistAsync(String userId, String playlistId);
        Task<AlbumDto> GetAlbumAsync(String albumId);
        Task<UserDto> GetUserAsync(String userId);
        Task<ArtistDto> GetArtistAsync(String artistId);

        List<TrackDto> GetTracks(int[] trackIds);
        Task<String> GetTrackUrlAsync(int trackId);
    }
}
