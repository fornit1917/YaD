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
            if (args.Length == 2)
            {
                DownloadWithoutInteractive(args[0], args[1]);
            }
            else
            {
                RunInteractiveMode();
            }
        }

        private static void RunInteractiveMode()
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
                PageInfo pageInfo;
                try
                {
                    pageInfo = pageInfoRetriever.GetPageInfoAsync(url).Result;
                    if (pageInfo == null)
                    {
                        Console.WriteLine("Incorrect or unsupported url");
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error! Cannot load inforamtion");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("-----------");
                    continue;
                }

                Console.WriteLine("Owner: " + pageInfo.TracklistOwner);
                Console.WriteLine("Title: " + pageInfo.TracklistTitle);
                Console.WriteLine("Tracks Count: " + pageInfo.Tracks.TotalCount);

                Console.Write("Enter path for downloading or /cancel: ");
                String path = Console.ReadLine();
                if (path == "/cancel")
                {
                    continue;
                }

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

                Download(pageInfo, fs, path);
            }
        }

        private static void DownloadWithoutInteractive(String url, String path)
        {
            PageInfoRetriever pageInfoRetriever = new PageInfoRetriever();
            IFileSystem fs = new FileSystem();
            Console.WriteLine("Load information......");
            PageInfo pageInfo;
            try
            {
                pageInfo = pageInfoRetriever.GetPageInfoAsync(url).Result;
                if (pageInfo == null)
                {
                    Console.WriteLine("Incorrect or unsupported url");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! Cannot load inforamtion");
                Console.WriteLine(e.Message);
                Console.WriteLine("-----------");
                return;
            }

            Download(pageInfo, fs, path);
        }

        private static void Download(PageInfo pageInfo, IFileSystem fs, String path)
        {
            TracksDownloader td = new TracksDownloader(fs) { CallHandlerOnlyOnFinish = true };
            ProgressPrinter progressPrinter = new ProgressPrinter(pageInfo.Tracks.TotalCount);
            td.OnDownloadProgress += progressPrinter.OnTrackDownloaded;
            Task downloadTask = td.StartDownload(path, pageInfo);
            try
            {
                downloadTask.Wait();
            }
            catch (Exception e)
            {
                td.Cancel();
                Console.WriteLine("Error! Cannot download track!");
                if (e != null)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
