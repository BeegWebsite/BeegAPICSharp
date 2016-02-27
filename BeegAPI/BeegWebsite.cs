using BeegAPI.Json;
using Newtonsoft.Json;
using System.Net;

namespace BeegAPI
{
    public static class BeegWebsite
    {
        public static string[] GetIDListFromPage(int pageNumber)
        {
            string[] result;
            string content = GetSource("http://api.beeg.com/api/v5/index/main/" + pageNumber + "/pc");
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            result = new string[CountStringOccurrences(content, "{\"title\":")];
            for (int i = 0; i < result.Length; i++)
                result[i] = jsonResult.videos[i].id;
            return result;
        }

        public static string[] GetIDListFromPageSearch(int pageNumber, string search)
        {
            string[] result;
            string content = GetSource("http://api.beeg.com/api/v5/index/search/" + pageNumber + "/pc?query=" + search);
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            result = new string[CountStringOccurrences(content, "{\"title\":")];
            for (int i = 0; i < result.Length; i++)
                result[i] = jsonResult.videos[i].id;
            return result;
        }

        public static string[] GetIDListFromPageTag(int pageNumber, string tag)
        {
            string[] result;
            string content = GetSource("http://api.beeg.com/api/v5/index/tag/" + pageNumber + "/pc?tag=" + tag);
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            result = new string[CountStringOccurrences(content, "{\"title\":")];
            for (int i = 0; i < result.Length; i++)
                result[i] = jsonResult.videos[i].id;
            return result;
        }

        public static string[] GetNameListFromPage(int pageNumber)
        {
            string[] result;
            string content = GetSource("http://api.beeg.com/api/v5/index/main/" + pageNumber + "/pc");
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            result = new string[CountStringOccurrences(content, "{\"title\":")];
            for (int i = 0; i < result.Length; i++)
                result[i] = jsonResult.videos[i].title;
            return result;
        }

        public static string[] GetNameListFromPageSearch(int pageNumber, string search)
        {
            string[] result;
            string content = GetSource("http://api.beeg.com/api/v5/index/search/" + pageNumber + "/pc?query=" + search);
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            result = new string[CountStringOccurrences(content, "{\"title\":")];
            for (int i = 0; i < result.Length; i++)
                result[i] = jsonResult.videos[i].title;
            return result;
        }

        public static string[] GetNameListFromPageTag(int pageNumber, string tag)
        {
            string[] result;
            string content = GetSource("http://api.beeg.com/api/v5/index/tag/" + pageNumber + "/pc?tag=" + tag);
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            result = new string[CountStringOccurrences(content, "{\"title\":")];
            for (int i = 0; i < result.Length; i++)
                result[i] = jsonResult.videos[i].title;
            return result;
        }

        public static string[] GetPopularTags()
        {
            string content = GetSource("http://api.beeg.com/api/v5/index/search/0/pc?query=apiusage");
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            return jsonResult.tags.popular;
        }

        public static string[] GetNonPopularTags()
        {
            string content = GetSource("http://api.beeg.com/api/v5/index/search/0/pc?query=apiusage");
            JsonBeegWebsite jsonResult = JsonConvert.DeserializeObject<JsonBeegWebsite>(content);
            return jsonResult.tags.nonpopular;
        }

        static string GetSource(string url)
        {
            string content = "";
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = null;
                content = wc.DownloadString(url
                    .Replace("\"720p\":", "\"p720\":")
                    .Replace("\"480p\":", "\"p480\":")
                    .Replace("\"240p\":", "\"p240\":"));
            }
            return content;
        }

        static int CountStringOccurrences(string text, string pattern)
        {
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
    }    
}
