using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeegAPI
{
    public class BeegVideoDownloader
    {
        private string ID;

        private WebClient client;

        public event DownloadProgressChangedEventHandler downloadProgress;
        public event AsyncCompletedEventHandler downloadFinish;

        private BeegVideo video;

        public BeegVideoDownloader(String ID)
        {
            this.ID = ID;
            video = new BeegVideo(ID);
            client = new WebClient();
            client.Proxy = null;
            downloadFinish = null;
            downloadProgress = null;
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
        }

        public void Download(String path, BeegVideo.BeegQuality quality)
        {
            if(!client.IsBusy)
                client.DownloadFileAsync(new Uri(video.getURL(quality)), path + "/" + ID + ".mp4");
        }

        public void Download(String path, String name, BeegVideo.BeegQuality quality)
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
            if(downloadProgress != null)
                downloadProgress.Invoke(sender, e);
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if(downloadFinish != null)
                downloadFinish.Invoke(sender, e);
        }

    }
}
