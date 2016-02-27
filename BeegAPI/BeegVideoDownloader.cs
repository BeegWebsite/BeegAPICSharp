using System;
using System.ComponentModel;
using System.Net;

namespace BeegAPI
{
    public class BeegVideoDownloader
    {
        string ID;

        WebClient client;

        public event DownloadProgressChangedEventHandler downloadProgress;
        public event AsyncCompletedEventHandler downloadFinish;

        BeegVideo video;

        int percentage, nperc;

        public BeegVideoDownloader(BeegVideo video)
        {
            this.video = video;
            ID = this.video.GetID();
            Load();
        }

        public BeegVideoDownloader(string ID)
        {
            this.ID = ID;
            video = new BeegVideo(ID);
            Load();
        }

        private void Load()
        {
            video.Load();
            client = new WebClient();
            client.Proxy = null;
            downloadFinish = null;
            downloadProgress = null;
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
        }

        public BeegVideo getVideo()
        {
            return video;
        }

        public void Download(string path, BeegVideo.BeegQuality quality)
        {
            if (!client.IsBusy)
                client.DownloadFileAsync(new Uri(video.GetURL(quality)), path + "/" + ID + ".mp4");
        }

        public void Download(string path, string name, BeegVideo.BeegQuality quality)
        {
            if (!client.IsBusy)
                client.DownloadFileAsync(new Uri(video.GetURL(quality)), path + "/" + name + ".mp4");
        }

        public string GetID()
        {
            return ID;
        }

        void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (downloadProgress != null)
            {
                nperc = e.ProgressPercentage;
                if (nperc != percentage)
                {
                    downloadProgress.Invoke(sender, e);
                }
                percentage = nperc;
            }
        }

        void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (downloadFinish != null)
                downloadFinish.Invoke(sender, e);
        }

    }
}