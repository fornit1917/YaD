using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaD.Lib;

namespace YaD.UI.CLI
{
    class CliApp
    {
        static PageInfoRetriever pageInfoRetriever = new PageInfoRetriever();

        static void Main(string[] args)
        {
            Test();
            Console.ReadLine();
            /*
            while (true)
            {
                Console.Write("Enter URL: ");
                String url = Console.ReadLine();
                if (url == "q")
                {
                    break;
                }

                ProcessUrl(url);
            }
            */
        }

        static async void Test()
        {
            YandexDataApiClient apiClient = new YandexDataApiClient();
            String url = apiClient.GetTrackUrl(5533086);
            Console.WriteLine(url);

            url = "https://music.yandex.ru/album/4413792";
            PageInfo pageInfo = await pageInfoRetriever.GetPageInfoAsync(url);

            Console.WriteLine("Image: " + pageInfo.Image);
            Console.WriteLine("Owner: " + pageInfo.TracklistOwner);
            Console.WriteLine("Title: " + pageInfo.TracklistTitle);

            Console.WriteLine("---------------------");

            url = "https://music.yandex.ru/users/vit.fornit.1917/playlists/1003";
            pageInfo = await pageInfoRetriever.GetPageInfoAsync(url);
            Console.WriteLine("Image: " + pageInfo.Image);
            Console.WriteLine("Owner: " + pageInfo.TracklistOwner);
            Console.WriteLine("Title: " + pageInfo.TracklistTitle);

            Console.WriteLine("----------------------");

            url = "https://music.yandex.ru/artist/36825";
            pageInfo = await pageInfoRetriever.GetPageInfoAsync(url);
            Console.WriteLine("Image: " + pageInfo.Image);
            Console.WriteLine("Owner: " + pageInfo.TracklistOwner);
            Console.WriteLine("Title: " + pageInfo.TracklistTitle);

            url = "https://music.yandex.ru/users/vit.fornit.1917/tracks";
            pageInfo = await pageInfoRetriever.GetPageInfoAsync(url);
            Console.WriteLine("Image: " + pageInfo.Image);
            Console.WriteLine("Owner: " + pageInfo.TracklistOwner);
            Console.WriteLine("Title: " + pageInfo.TracklistTitle);

            IFileSystem fs = new FileSystem();
            TracksDownloader td = new TracksDownloader(fs) { CallHandlerOnlyOnFinish = true };

            int i = 1;
            Object sync = new Object();
            td.OnDownloadProgress += (o, e) =>
            {
                lock (o)
                {
                    String s = $"{e.Track.Title}: {i} / {pageInfo.Tracks.TotalCount}";
                    Console.WriteLine(s);
                    i++;
                }
            };
            td.StartDownload("D:\\tmp\\music", pageInfo, 10);

            Console.WriteLine("----------------------");


            Console.ReadLine();
        }

        static async void ProcessUrl(String url)
        {
            PageInfo pageInfo = await pageInfoRetriever.GetPageInfoAsync(url);
            if (pageInfo == null)
            {
                Console.WriteLine("Unknown page!");
                return;
            }

            Console.WriteLine("Page: {0}", pageInfo.TracklistTitle);
        }
    }
}
