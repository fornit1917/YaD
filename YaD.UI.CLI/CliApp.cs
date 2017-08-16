﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            String url = "https://music.yandex.ru/album/2678307";
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
