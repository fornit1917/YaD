﻿using System;
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

        public async Task<PageInfo> GetPageInfoAsync(String url)
        {
            UrlParams urlParams = urlParser.Parse(url);
            if (urlParams == null)
            {
                return null;
            }

            PageInfo pageInfo = null;
            switch (urlParams.Type)
            {
                case PageType.Album:
                    UrlParamsAlbum urlParamsAlbum = urlParams as UrlParamsAlbum;
                    AlbumDto albumDto = await apiClient.GetAlbumAsync(urlParamsAlbum.AlbumId);
                    pageInfo = new PageInfo()
                    {
                        TracklistOwner = albumDto.Artist,
                        TracklistTitle = albumDto.Title,
                        Image = albumDto.Image,
                        Tracks = TrackListFactory.Create(albumDto.Tracks),
                    };
                    break;
                case PageType.Playlist:
                    UrlParamsPlaylist urlParamsPlaylist = urlParams as UrlParamsPlaylist;
                    PlaylistDto playlistDto = await apiClient.GetPlaylistAsync(urlParamsPlaylist.UserId, urlParamsPlaylist.PlaylistId);
                    pageInfo = new PageInfo()
                    {
                        TracklistOwner = playlistDto.Owner,
                        TracklistTitle = playlistDto.Title,
                        Image = playlistDto.Image,
                        Tracks = TrackListFactory.Create(playlistDto.TrackIds, playlistDto.Tracks, apiClient),
                    };
                    break;
                
                case PageType.Artist:
                    UrlParamsArtist urlParamsArtist = urlParams as UrlParamsArtist;
                    ArtistDto artistDto = await apiClient.GetArtistAsync(urlParamsArtist.ArtistId);
                    pageInfo = new PageInfo()
                    {
                        TracklistOwner = artistDto.Name,
                        TracklistTitle = "All tracks",
                        Image = artistDto.Image,
                        Tracks = TrackListFactory.Create(artistDto.TrackIds, artistDto.Tracks, apiClient),
                    };
                    break;
                
                case PageType.User:
                    UrlParamsUser urlParamsUser = urlParams as UrlParamsUser;
                    UserDto userDto = await apiClient.GetUserAsync(urlParamsUser.UserId);
                    pageInfo = new PageInfo()
                    {
                        TracklistOwner = $"{userDto.Name} / {userDto.Login}",
                        TracklistTitle = "All user tracks",
                        Image = userDto.Image,
                        Tracks = TrackListFactory.Create(userDto.TrackIds, userDto.Tracks, apiClient),
                    };
                    break;
                    
            }

            return pageInfo;
        }
    }
}
