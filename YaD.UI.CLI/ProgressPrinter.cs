using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaD.Lib;

namespace YaD.UI.CLI
{
    class ProgressPrinter
    {
        private Object sync = new Object();
        private int counter = 1;
        private int totalCount;

        public bool Completed => counter >= totalCount;

        public ProgressPrinter(int tracksCount)
        {
            totalCount = tracksCount;
        }

        public void OnTrackDownloaded(Object sender, TrackDownloadProgressEventArgs e)
        {
            lock (sync)
            {
                String s = $"{e.Track.Title}: {counter} / {totalCount}";
                Console.WriteLine(s);
                counter++;
            }
        }
    }
}
