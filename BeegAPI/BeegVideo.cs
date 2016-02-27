using BeegAPI.Json;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;

namespace BeegAPI
{
    public class BeegVideo
    {
        string ID, source;
        JsonBeegVideo json;

        public enum BeegQuality { Best, Good, Fast, Null }

        public BeegVideo(string ID)
        {
            this.ID = ID;
        }

        public void Load()
        {
            if (!Loaded())
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Proxy = null;
                    source = wc.DownloadString("http://api.beeg.com/api/v5/video/" + ID)
                        .Replace("\"720p\":", "\"p720\":")
                    .Replace("\"480p\":", "\"p480\":")
                    .Replace("\"240p\":", "\"p240\":");
                }
                json = JsonConvert.DeserializeObject<JsonBeegVideo>(source);
            }
        }

        public string GetTitle()
        {
            if (Loaded())
                return json.title;
            return null;
        }

        public string GetDescription()
        {
            if (Loaded())
                return json.desc;
            return null;
        }

        public Bitmap GetThumbnail()
        {
            return new Bitmap(WebRequest.Create("http://img.beeg.com/236x177/" + ID + ".jpg").GetResponse().GetResponseStream());
        }

        public Bitmap GetThumbnail(int width, int height)
        {
            return new Bitmap(WebRequest.Create("http://img.beeg.com/" + width + "x" + height + "/" + ID + ".jpg").GetResponse().GetResponseStream());
        }

        public string GetThumbnailURL()
        {
            return "http://img.beeg.com/236x177/" + ID + ".jpg";
        }

        public string GetThumbnailURL(int width, int height)
        {
            return "http://img.beeg.com/" + width + "x" + height + "/" + ID + ".jpg";
        }

        public BeegQuality GetBestQuality()
        {
            if (Loaded())
            {
                if (json.p720 != null)
                    return BeegQuality.Best;
                if (json.p480 != null)
                    return BeegQuality.Good;
                if (json.p240 != null)
                    return BeegQuality.Fast;
            }
            return BeegQuality.Null;
        }

        public int GetBestQualityInPixels()
        {
            if (Loaded())
            {
                BeegQuality bq = GetBestQuality();
                return ((bq == BeegQuality.Best) ? 720 : ((bq == BeegQuality.Good) ? 480 : 240));
            }
            return -1;
        }

        public string GetURL(BeegQuality quality)
        {
            if (Loaded())
            {
                string url = null;
                if (quality == BeegQuality.Best)
                    url = json.p720;
                if (quality == BeegQuality.Good || (url == "" && quality == BeegQuality.Best))
                    url = json.p480;
                if (quality == BeegQuality.Fast || url == "")
                    url = json.p240;
                return "http://" + url.Substring(2).Replace("{DATA_MARKERS}", "data=pc");
            }
            return null;
        }

        public string GetCasting()
        {
            if(Loaded())
                return json.cast;
            return null;
        }

        public string GetPublishedDate()
        {
            if (Loaded())
                return json.date;
            return null;
        }

        public string GetID()
        {
            return ID;
        }

        public bool Loaded()
        {
            return json != null;
        }
    }

}
