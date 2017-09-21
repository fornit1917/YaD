using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaD.Lib;

namespace YaD.Tests.Mocks
{
    class DataApiClientMock : IDataApiClient
    {
        public async Task<AlbumTracksDto> GetAlbumAsync(string albumId)
        {
            return new AlbumTracksDto()
            {
                Artist = "Artist",
                Image = $"http://test.com/{albumId}",
                Title = "Album",
                Tracks = GetTracks(new int[] { 1, 2, 3, 4, 5 })
            };
        }

        public async Task<ArtistTracksDto> GetArtistAsync(string artistId)
        {
            return new ArtistTracksDto()
            {
                Name = "Artist",
                Image = $"http://test.com/{artistId}",
                TrackIds = new int[] { 1, 2, 3, 4, 5 },
                Tracks = GetTracks(new int[] { 1, 2, 3, 4, 5 })
            };
        }

        public async Task<PlaylistTracksDto> GetPlaylistAsync(string userId, string playlistId)
        {
            return new PlaylistTracksDto()
            {
                Title = "Playlist",
                Owner = "User",
                Image = $"http://test.com/{userId}/{playlistId}",
                TrackIds = new int[] {1, 2, 3, 4, 5},
                Tracks = GetTracks(new int[] { 1, 2 })
            };
        }

        public List<TrackDto> GetTracks(int[] trackIds)
        {
            return (
                from id in trackIds
                select new TrackDto()
                {
                    AlbumTitle = "Album",
                    AlbumYear = 2017,
                    Artist = "Artist",
                    Id = id,
                    Title = id.ToString(),
                }
            ).ToList();
        }

        public String GetTrackUrl(int trackId)
        {
            return $"http://track/{trackId}";
        }

        public async Task<UserTracksDto> GetUserAsync(string userId)
        {
            return new UserTracksDto()
            {
                Login = "UserLogin",
                Name = "UserName",
                Image = $"http://test.com/{userId}",
                TrackIds = new int[] { 1, 2, 3, 4, 5 },
                Tracks = GetTracks(new int[] { 1, 2, 3, 4, 5 })
            };
        }
    }
}
