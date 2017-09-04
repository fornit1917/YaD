using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace YaD.Lib
{
    public class YandexDataApiClient : IDataApiClient
    {
        public async Task<AlbumDto> GetAlbum(string albumId)
        {
            String url = $"https://music.yandex.ru/handlers/album.jsx?album={albumId}&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.3792734650109968";
            JObject data = await RequestJsonObject(url);

            String title = (String)data["title"];
            int year = (int)data["year"];

            return new AlbumDto()
            {
                Image = GetImageUrl((String)data["coverUri"]),
                Title = title,
                Artist = String.Join(" & ", from a in data["artists"] select a["name"]),
                Year = year,
                Tracks = (from volume in data["volumes"]
                         from track in volume
                         select new TrackDto()
                         {
                             Id = Convert.ToInt32(track["id"]),
                             Title = (String)track["title"],
                             Artist = String.Join(" & ", from a in track["artists"] select a["name"]),
                             AlbumTitle = title,
                             AlbumYear = year,
                         }).ToList(),
            };
        }

        public async Task<PlaylistDto> GetPlaylist(string userId, string playlistId)
        {
            String url = $"https://music.yandex.ru/handlers/playlist.jsx?owner={userId}&kinds={playlistId}&light=true&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.5872919215747372";
            JObject data = await RequestJsonObject(url);
            return new PlaylistDto()
            {
                Image = GetImageUrl((String)data["playlist"]["ogImage"]),
                Owner = (String)data["playlist"]["owner"]["name"],
                Title = (String)data["playlist"]["title"],
                TrackIds = GetTrackIdsFromJToken(data["playlist"]["trackIds"]),
                Tracks = GetTracksFromJToken(data["playlist"]["tracks"]),
            };
        }

        public async Task<UserDto> GetUser(string userId)
        {
            String url = $"https://music.yandex.ru/handlers/library.jsx?owner={userId}&filter=tracks&likeFilter=favorite&sort=&dir=&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.7220692948046592";
            JObject data = await RequestJsonObject(url);
            return new UserDto()
            {
                Login = userId,
                Name = (String)data["owner"]["name"],
                Image = "https://yapic.yandex.ru/get/" + (String)data["owner"]["uid"] + "/islands-retina-middle",
                TrackIds = GetTrackIdsFromJToken(data["trackIds"]),
                Tracks = GetTracksFromJToken(data["tracks"]),
            };
        }

        public async Task<ArtistDto> GetArtist(string artistId)
        {
            String url = $"https://music.yandex.ru/handlers/artist.jsx?artist={artistId}&what=tracks&sort=&dir=&lang=ru&external-domain=music.yandex.ru&overembed=false&ncrnd=0.329131147428392";
            JObject data = await RequestJsonObject(url);

            return new ArtistDto()
            {
                Name = (String)data["artist"]["name"],
                Image = GetImageUrl((String)data["artist"]["cover"]["uri"]),
                TrackIds = GetTrackIdsFromJToken(data["trackIds"]),
                Tracks = GetTracksFromJToken(data["tracks"]),
            };
        }

        private HttpWebRequest CreateRequest(String url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers.Clear();
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            req.Host = "music.yandex.ru";
            req.Method = "GET";

            req.Headers.Add("Accept-Language", "en-US,en;q=0.8,ru;q=0.6");
            req.Headers.Add("Accept-Encoding", "identity");
            req.Headers.Add("Cache-Control", "max-age=0");
            req.Headers.Add("DNT", "1");
            req.Headers.Add("Upgrade-Insecure-Requests", "1");
            req.Headers.Add("Cookie", "_ym_uid=1502906756176177940; mda=0; _ym_isad=2; yandexuid=5733988641502906756; yp=1818266756.yrts.1502906756; _ym_visorc_10630330=w; spravka=dD0xNTAyOTA2NzYxO2k9NDYuMTY0LjE5OC4xNzU7dT0xNTAyOTA2NzYxNTgzNzUzMTAzO2g9ZDQwZmZlNmFkYjQ1NWJhNjNhMWE5MmM3NTFjYTg5MWY=; device_id=\"bd83f65309fad0c369c4a221b8faa9991ee873936\"; _ym_visorc_1028356=b; i=UpYyqKGVjzBkESSuqqttj9x4BrOVszvLI2iE/DLIR1QNZU3zHshVTgnApfM5J8FdYT3DFTK3lftra/BrPQjqlxgsU0U=");            

            return req;
        }

        private async Task<JObject> RequestJsonObject(String url)
        {
            HttpWebRequest req = CreateRequest(url);
            WebResponse resp = await req.GetResponseAsync();
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                String respStr = sr.ReadToEnd();
                return JObject.Parse(respStr);
            }
        }

        private String GetImageUrl(String template)
        {
            return "https://" + template.Replace("/%%", "/200x200");
        }

        private List<TrackDto> GetTracksFromJToken(JToken data)
        {
            return (
                from t in data
                let albums = t["albums"]
                let album = albums != null && albums.Count() > 0 ? albums[0] : null
                select new TrackDto()
                {
                    Id = Convert.ToInt32(t["id"]),
                    Title = (String)t["title"],
                    Artist = String.Join(" & ", from a in t["artists"] select a["name"]),
                    AlbumTitle = album == null ? null : (String)album["title"],
                    AlbumYear = album == null || album["year"] == null ? 0 : (int)album["year"],
                }
             ).ToList();
        }

        private int[] GetTrackIdsFromJToken(JToken data)
        {
            return (from tId in data select Convert.ToInt32(((String)tId).Split(':')[0])).ToArray();
        }
    }
}