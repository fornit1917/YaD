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
        static void Main(string[] args)
        {
            PageInfoRetriever pageInfoRetriever = new PageInfoRetriever();
            IFileSystem fs = new FileSystem();

            while (true)
            {
                Console.WriteLine("Enter URL of Album, Playlist, Artist or User. Or enter q for exit");
                Console.Write("URL: ");
                String url = Console.ReadLine();
                if (url == "q")
                {
                    Console.WriteLine("Bye!");
                    return;
                }

                Console.WriteLine("Load information......");
                PageInfo pageInfo = pageInfoRetriever.GetPageInfoAsync(url).Result;
                Console.WriteLine("Owner: " + pageInfo.TracklistOwner);
                Console.WriteLine("Title: " + pageInfo.TracklistTitle);
                Console.WriteLine("Tracks Count: " + pageInfo.Tracks.TotalCount);

                Console.Write("Enter path for downloading: ");
                String path = Console.ReadLine();
                if (!fs.DirIsEmpty(path))
                {
                    Console.WriteLine("Specified directory exists and is not empty!");
                    while (true)
                    {
                        Console.WriteLine("Do you want to delete all files from directory before start? y/n");
                        String flag = Console.ReadLine().ToUpper();
                        if (flag == "Y")
                        {
                            fs.CleanDir(path);
                            break;
                        }
                        else if (flag == "N")
                        {
                            break;
                        }
                    }
                }

                TracksDownloader td = new TracksDownloader(fs) { CallHandlerOnlyOnFinish = true };
                ProgressPrinter progressPrinter = new ProgressPrinter(pageInfo.Tracks.TotalCount);
                td.OnDownloadProgress += progressPrinter.OnTrackDownloaded;
                td.StartDownload(path, pageInfo).Wait();
            }
        }
    }
}
