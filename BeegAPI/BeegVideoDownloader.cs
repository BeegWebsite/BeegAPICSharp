using System;
using System.ComponentModel;
using System.Net;

namespace BeegAPI
{
    public class BeegVideoDownloader
    {
        private string ID;

        private WebClient client;

        public event DownloadProgressChangedEventHandler downloadProgress;
        public event AsyncCompletedEventHandler downloadFinish;

        private BeegVideo video;

        private int percentage, nperc;

        public BeegVideoDownloader(BeegVideo video)
        {
            this.video = video;
            ID = this.video.getID();
            load();
        }

        public BeegVideoDownloader(String ID)
        {
            this.ID = ID;
            video = new BeegVideo(ID);
            load();
        }

        private void load()
        {
            video.load();
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

        public void download(String path, BeegVideo.BeegQuality quality)
        {
            if(!client.IsBusy)
                client.DownloadFileAsync(new Uri(video.getURL(quality)), path + "/" + ID + ".mp4");
        }

        public void download(String path, String name, BeegVideo.BeegQuality quality)
        {
            if (!client.IsBusy)
                client.DownloadFileAsync(new Uri(video.getURL(quality)), path + "/" + name + ".mp4");
        }

        public String getID()
        {
            return ID;
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
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

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if(downloadFinish != null)
                downloadFinish.Invoke(sender, e);
        }

    }
}
