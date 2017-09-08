using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YaD.Lib
{
    public class TracksDownloader
    {
        private IFileDownloader fileDownloader;
        private IFileSystem fileSystem;
        private IDataApiClient apiClient;

        public IFileSystem FileSystem => fileSystem;

        public TracksDownloader()
        {
            fileDownloader = new FileDownloader();
            fileSystem = new FileSystem();
            apiClient = new YandexDataApiClient();
        }

        public TracksDownloader(IFileDownloader fileDownloader, IFileSystem fileSystem, IDataApiClient apiClient)
        {
            this.fileDownloader = fileDownloader;
            this.fileSystem = fileSystem;
            this.apiClient = apiClient;
        }

        public void StartDownload(String path, PageInfo pageInfo, int workersCount = 3)
        {
            BlockingCollection<TrackDto> queue = new BlockingCollection<TrackDto>();

            Task producer = Task.Factory.StartNew(() =>
            {
                foreach (var track in pageInfo.Tracks)
                {
                    queue.Add(track);
                }
                queue.CompleteAdding();
            });


            // run downloaders
            for (int i = 0; i < workersCount; ++i)
            {
                Task downloader = Task.Factory.StartNew(() => { DownloadWorker(queue, pageInfo, path); });
            }
        }

        private void DownloadWorker(BlockingCollection<TrackDto> queue, PageInfo pageInfo, String path)
        {
            bool hasTracks = true;
            TrackDto track = null;
            while (hasTracks)
            {
                try
                {
                    track = queue.Take();
                }
                catch (InvalidOperationException e)
                {
                    hasTracks = false;
                }

                if (hasTracks && track != null)
                {
                    String dest = GetFilePath(pageInfo, track, path);
                    if (fileSystem.IsDownloadedTrack(dest, track))
                    {
                        // call finish callback
                    }
                    else
                    {
                        String url = apiClient.GetTrackUrl(track.Id);
                        fileDownloader.Download(url, dest, (o, e) =>
                        {
                            // call track download progress callback
                        });
                    }
                }
            }
        }

        private String GetFilePath(PageInfo pageInfo, TrackDto track, String path)
        {
            StringBuilder sb = new StringBuilder(128);
            sb.Append(path);
            sb.Append(Path.PathSeparator);
            if (pageInfo.Type == PageType.Artist)
            {
                sb.Append(track.AlbumYear);
                sb.Append(" - ");
                sb.Append(track.AlbumTitle);
                sb.Append(Path.PathSeparator);
                sb.Append(track.Title);
            }
            else
            {
                sb.Append(track.Artist);
                sb.Append(" - ");
                sb.Append(track.Title);
            }
            sb.Append(".mp3");

            return sb.ToString();
        }
    }
}
