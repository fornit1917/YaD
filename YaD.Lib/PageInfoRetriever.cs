using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class PageInfoRetriever
    {
        private IDataApiClient apiClient;
        private UrlParser urlParser = new UrlParser();

        public PageInfoRetriever()
        {
            this.apiClient = new YandexDataApiClient();
        }

        public PageInfoRetriever(IDataApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Task<PageInfo> GetPageInfoAsync(String url)
        {
            UrlParams urlParams = urlParser.Parse(url);
            if (urlParams == null)
            {
                return Task.FromResult<PageInfo>(null);
            }

            PageInfo pageInfo = null;
            switch (urlParams.Type)
            {
                case PageType.Album:
                    UrlParamsAlbum urlParamsAlbum = urlParams as UrlParamsAlbum;
                    pageInfo = new PageInfo() { Title = String.Format("Album: {0}", urlParamsAlbum.AlbumId) };
                    break;
                case PageType.Playlist:
                    UrlParamsPlaylist urlParamsPlaylist = urlParams as UrlParamsPlaylist;
                    pageInfo = new PageInfo() { Title = String.Format("Playlist: {0}, {1}", urlParamsPlaylist.UserId, urlParamsPlaylist.PlaylistId) };
                    break;
                case PageType.Artist:
                    UrlParamsArtist urlParamsArtist = urlParams as UrlParamsArtist;
                    pageInfo = new PageInfo() { Title = String.Format("Artist: {0}", urlParamsArtist.ArtistId) };
                    break;
                case PageType.User:
                    UrlParamsUser urlParamsUser = urlParams as UrlParamsUser;
                    pageInfo = new PageInfo() { Title = String.Format("User: {0}", urlParamsUser.UserId) };
                    break;
            }

            return Task.FromResult<PageInfo>(pageInfo);
        }
    }
}
