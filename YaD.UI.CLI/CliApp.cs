using System;
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
