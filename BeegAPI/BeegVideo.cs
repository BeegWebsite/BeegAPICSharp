using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace BeegAPI
{
    public class BeegVideo
    {
        private string ID;
        private string source;

        public enum BeegQuality { Best, Good, Fast, Null }

        public BeegVideo(String ID)
        {
            this.ID = ID;
        }

        public void load()
        {
            if (!loaded())
            {
                WebClient client = new WebClient();
                client.Proxy = null;
                source = client.DownloadString("http://beeg.com/" + ID);
                client.Dispose();
            }
        }

        public String getTitle()
        {
            if (loaded())
            {
                string title = Regex.Match(source, @"<title>(.*)</title>", RegexOptions.IgnoreCase).Groups[1].Value;
                return title.Substring(0, title.Length - 8);
            }
            return null;
        }

        public String getDescription()
        {
            if (loaded())
            {
                string description = Regex.Match(source, @"<td class=""synopsis more"" colspan=""2"">(.*)</td>", RegexOptions.IgnoreCase).Groups[1].Value;
                return description;
            }
            return null;
        }

        public Bitmap getThumbnail()
        {
            WebRequest request = WebRequest.Create("http://img.beeg.com/236x177/" + ID + ".jpg");
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            Bitmap bitmap = new Bitmap(responseStream);
            return bitmap;
        }

        public Bitmap getThumbnail(int width, int height)
        {
            WebRequest request = WebRequest.Create("http://img.beeg.com/" + width + "x" + height + "/" + ID + ".jpg");
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            Bitmap bitmap = new Bitmap(responseStream);
            return bitmap;
        }

        public String getThumbnailURL()
        {
            return "http://img.beeg.com/236x177/" + ID + ".jpg";
        }

        public String getThumbnailURL(int width, int height)
        {
            return "http://img.beeg.com/" + width + "x" + height + "/" + ID + ".jpg";
        }

        public BeegQuality getBestQuality()
        {
            if (loaded())
            {
                Match match = new Regex("'720p': '(.*)'").Match(source);
                if (match.Success)
                    return BeegQuality.Best;
                match = new Regex("'480p': '(.*)'").Match(source);
                if (match.Success)
                    return BeegQuality.Good;
                return BeegQuality.Fast;
            }
            return BeegQuality.Null;
        }

        public int getBestQualityInPixels()
        {
            if (loaded())
            {
                BeegQuality bq = getBestQuality();
                return ((bq == BeegQuality.Best) ? 720 : ((bq == BeegQuality.Good) ? 480 : 240));
            }
            return -1;
        }

        public String getURL(BeegQuality quality)
        {
            if (loaded())
            {
                string url = "";
                if (quality == BeegQuality.Best)
                {
                    Match matcher = new Regex("'720p': '(.*)'").Match(source);
                    if (matcher.Success)
                        url = matcher.Groups[1].Value;
                }
                if (quality == BeegQuality.Good || (url == "" && quality == BeegQuality.Best))
                {
                    Match matcher = new Regex("'480p': '(.*)'").Match(source);
                    if (matcher.Success)
                        url = matcher.Groups[1].Value;
                }
                if (quality == BeegQuality.Fast || url == "")
                {
                    Match matcher = new Regex("'240p': '(.*)'").Match(source);
                    url = matcher.Groups[1].Value;
                }
                return url;
            }
            return null;
        }

        public String getCasting()
        {
            if (loaded())
            {
                string cast = Regex.Match(source, @"<th>Cast</th>\s*<td>(.*)</td>", RegexOptions.IgnoreCase).Groups[1].Value;
                return cast;
            }
            return null;
        }

        public String getPublishedDate()
        {
            if (loaded())
            {
                string date = Regex.Match(source, @"<th>Published</th>\s*<td>(.*)</td>", RegexOptions.IgnoreCase).Groups[1].Value;
                return date;
            }
            return null;
        }

        public String getID()
        {
            return ID;
        }

        private bool loaded()
        {
            if (source == null)
                return false;
            return true;
        }

    }
}
