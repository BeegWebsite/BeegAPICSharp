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

        public enum BeegQuality
        {
            Good,
            Fast
        }

        public BeegVideo(String ID)
        {
            WebClient client = new WebClient();
            this.ID = ID;
            source = client.DownloadString("http://beeg.com/" + ID);
        }

        public String getTitle()
        {
            string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
            return title.Substring(0, title.Length - 8);
        }

        public String getDescription()
        {
            string description = Regex.Match(source, @"<td class=""synopsis more"" colspan=""2"">(?<Description>[\s\S]*?)</td>", RegexOptions.IgnoreCase).Groups["Description"].Value;
            return description;
        }

        public Bitmap getThumbnail()
        {
            WebRequest request = WebRequest.Create("http://img.beeg.com/236x177/" + ID + ".jpg");
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            Bitmap bitmap = new Bitmap(responseStream);
            return bitmap;
        }

        public String getURLThumbnail()
        {
            return "http://img.beeg.com/236x177/" + ID + ".jpg";
        }

        public String getURLThumbnail(int width, int height)
        {
            return "http://img.beeg.com/" + width + "x" + height + "/" + ID + ".jpg";
        }

        public String getURL(BeegQuality quality)
        {
            string url = "";
            if (quality == BeegQuality.Good)
            {
                url = Regex.Match(source, @"'480p': '(?<Video>[\s\S]*?)'", RegexOptions.IgnoreCase).Groups["Video"].Value;
            }
            else
            {
                url = Regex.Match(source, @"'240p': '(?<Video>[\s\S]*?)'", RegexOptions.IgnoreCase).Groups["Video"].Value;
            }
            return url;
        }

        public String getCasting()
        {
            string cast = Regex.Match(source, @"\<th\>Cast</th\>\s*<td\b[^>]*\>\s*(?<Cast>[\s\S]*?)\</td\>", RegexOptions.IgnoreCase).Groups["Cast"].Value;
            return cast;
        }

        public String getPublishedDate()
        {
            string date = Regex.Match(source, @"\<th\>Published</th\>\s*<td\b[^>]*\>\s*(?<Date>[\s\S]*?)\</td\>", RegexOptions.IgnoreCase).Groups["Date"].Value;
            return date;
        }


    }
}
