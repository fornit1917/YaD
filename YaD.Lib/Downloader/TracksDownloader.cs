using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaD.Lib
{
    public class TracksDownloader
    {
        private ITrackList tracks;
        private IFileDownloader fileDownloader;
        private IFileSystem fileSystem;

        public IFileSystem FileSystem => fileSystem;

        public TracksDownloader(ITrackList tracks)
        {
            this.tracks = tracks;
        }

        public TracksDownloader(ITrackList tracks, IFileDownloader fileDownloader, IFileSystem fileSystem) : this(tracks)
        {
            this.fileDownloader = fileDownloader;
            this.fileSystem = fileSystem;
        }

        public void StartDownload(bool createAlbumFolders, int threadCount = 3)
        {

        }
    }
}
