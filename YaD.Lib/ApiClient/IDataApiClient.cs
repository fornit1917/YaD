using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public interface IDataApiClient
    {
        Task<PlaylistTracksDto> GetPlaylistAsync(String userId, String playlistId);
        Task<AlbumTracksDto> GetAlbumAsync(String albumId);
        Task<UserTracksDto> GetUserAsync(String userId);
        Task<ArtistTracksDto> GetArtistAsync(String artistId);

        List<TrackDto> GetTracks(int[] trackIds);
        String GetTrackUrl(int trackId);
    }
}
