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
        public async Task<AlbumDto> GetAlbumAsync(string albumId)
        {
            return new AlbumDto()
            {
                Artist = "Artist",
                Image = $"http://test.com/{albumId}",
                Title = "Album",
                Tracks = GetTracks(new int[] { 1, 2, 3, 4, 5 })
            };
        }

        public async Task<ArtistDto> GetArtistAsync(string artistId)
        {
            return new ArtistDto()
            {
                Name = "Artist",
                Image = $"http://test.com/{artistId}",
                TrackIds = new int[] { 1, 2, 3, 4, 5 },
                Tracks = GetTracks(new int[] { 1, 2, 3, 4, 5 })
            };
        }

        public async Task<PlaylistDto> GetPlaylistAsync(string userId, string playlistId)
        {
            return new PlaylistDto()
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

        public async Task<string> GetTrackUrlAsync(int trackId)
        {
            return $"http://track/{trackId}";
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            return new UserDto()
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
