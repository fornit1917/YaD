using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YaD.Lib
{
    abstract class UrlParams
    {
        public PageType Type { get; set; }

        public UrlParams(PageType type)
        {
            this.Type = type;
        }
    }

    class UrlParamsArtist : UrlParams
    {
        public string ArtistId { get; set; }

        public UrlParamsArtist(String artistId) : base(PageType.Artist)
        {
            this.ArtistId = artistId;
        }
    }

    class UrlParamsAlbum : UrlParams
    {
        public string AlbumId { get; set; }

        public UrlParamsAlbum(String albumId) : base(PageType.Album)
        {
            this.AlbumId = albumId;
        }
    }

    class UrlParamsPlaylist : UrlParams
    {
        public String UserId { get; set; }
        public String PlaylistId { get; set; }
            
        public UrlParamsPlaylist(String userId, String playlistId) : base(PageType.Playlist)
        {
            this.UserId = userId;
            this.PlaylistId = playlistId;
        }
    }

    class UrlParamsUser : UrlParams
    {
        public String UserId { get; set; }

        public UrlParamsUser(String userId) : base(PageType.User)
        {
            this.UserId = userId;
        }
    }
}
