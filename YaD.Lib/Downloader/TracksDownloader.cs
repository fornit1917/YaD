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

        public bool CallHandlerOnlyOnFinish { get; set; }
        public event TrackDownloadProgressHandler OnDownloadProgress;

        public TracksDownloader(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            fileDownloader = new FileDownloader();
            apiClient = new YandexDataApiClient();
        }

        public TracksDownloader(IFileDownloader fileDownloader, IFileSystem fileSystem, IDataApiClient apiClient)
        {
            this.fileDownloader = fileDownloader;
            this.fileSystem = fileSystem;
            this.apiClient = apiClient;
        }

        public Task StartDownload(String path, PageInfo pageInfo, int workersCount = 5)
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

            Task[] downloaderTasks = new Task[workersCount];
            // run downloaders
            for (int i = 0; i < workersCount; ++i)
            {
                downloaderTasks[i] = Task.Factory.StartNew(() => { DownloadWorker(queue, pageInfo, path); });
            }

            return Task.WhenAll(downloaderTasks);
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
                        OnDownloadProgress(this, new TrackDownloadProgressEventArgs(track, track.FileSize));
                    }
                    else
                    {
                        String url = apiClient.GetTrackUrl(track.Id);
                        fileSystem.CreateDirectoryForFilePath(dest);
                        fileDownloader.Download(url, dest, (o, e) =>
                        {
                            if (!CallHandlerOnlyOnFinish || e.BytesDownloaded >= track.FileSize)
                            {
                                OnDownloadProgress(this, new TrackDownloadProgressEventArgs(track, e.BytesDownloaded));
                            }
                        });

                        AddTagsToFile(dest, track);
                    }
                }
            }
        }

        private void AddTagsToFile(String fileName, TrackDto track)
        {
            using (TagLib.File f = TagLib.File.Create(fileName))
            {
                f.Tag.Title = track.Title;
                f.Tag.Performers = new[] { track.Artist };
                f.Tag.Year = (uint)track.AlbumYear;
                f.Tag.Album = track.AlbumTitle;
                f.Save();
            }
        }

        private String GetFilePath(PageInfo pageInfo, TrackDto track, String path)
        {
            StringBuilder sb = new StringBuilder(128);
            sb.Append(path);
            sb.Append(Path.DirectorySeparatorChar);
            if (pageInfo.Type == PageType.Artist)
            {
                sb.Append(track.AlbumYear);
                sb.Append(" - ");
                sb.Append(fileSystem.ReplaceIllegalChars(track.AlbumTitle));
                sb.Append(Path.DirectorySeparatorChar);
            }

            if (pageInfo.Type == PageType.Artist || pageInfo.Type == PageType.Album)
            {
                if (track.Position != null)
                {
                    if (track.Position.Volume != 0)
                    {
                        sb.Append(track.Position.Volume);
                    }
                    sb.Append(String.Format("{0:D2}", track.Position.Index));
                    sb.Append(" - ");
                }
                sb.Append(fileSystem.ReplaceIllegalChars(track.Title));
            }
            else
            {
                sb.Append(fileSystem.ReplaceIllegalChars(track.Artist));
                sb.Append(" - ");
                sb.Append(fileSystem.ReplaceIllegalChars(track.Title));
            }

            sb.Append(".mp3");

            return sb.ToString();
        }
    }
}
