using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YaD.Lib
{
    class UrlParser
    {
        private static Regex regexArtistUrl = new Regex(@"https://music.yandex.ru/artist/(\d+)");
        private static Regex regexAlbumUrl = new Regex(@"https://music.yandex.ru/album/(\d+)");
        private static Regex regexPlaylistUrl = new Regex(@"https://music.yandex.ru/users/([\d\w\.\-]+)/playlists/(\d+)");
        private static Regex regexUserUrl = new Regex(@"https://music.yandex.ru/users/([\d\w\.\-]+)/tracks");

        public UrlParams Parse(String url)
        {
            Match match = regexArtistUrl.Match(url);
            if (match.Success)
            {
                return new UrlParamsArtist(match.Groups[1].Value);
            }

            match = regexAlbumUrl.Match(url);
            if (match.Success)
            {
                return new UrlParamsAlbum(match.Groups[1].Value);
            }

            match = regexPlaylistUrl.Match(url);
            if (match.Success)
            {
                return new UrlParamsPlaylist(match.Groups[1].Value, match.Groups[2].Value);
            }

            match = regexUserUrl.Match(url);
            if (match.Success)
            {
                return new UrlParamsUser(match.Groups[1].Value);
            }

            return null;
        }
    }
}
