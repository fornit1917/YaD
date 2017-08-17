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
        public async Task<AlbumDto> GetAlbum(string albumId)
        {
            return new AlbumDto()
            {
                Artist = "Some Artist",
                Image = $"http://test.com/{albumId}",
                Title = albumId
            };
        }

        public async Task<ArtistDto> GetArtist(string artistId)
        {
            return new ArtistDto()
            {
                Name = artistId,
                Image = $"http://test.com/{artistId}",
            };
        }

        public async Task<PlaylistDto> GetPlaylist(string userId, string playlistId)
        {
            return new PlaylistDto()
            {
                Title = $"{userId} - ${playlistId}",
                Owner = userId,
                Image = $"http://test.com/{userId}/{playlistId}"
            };
        }

        public async Task<UserDto> GetUser(string userId)
        {
            return new UserDto()
            {
                Login = userId,
                Name = $"Name - {userId}",
                Image = $"http://test.com/{userId}",
            };
        }
    }
}
