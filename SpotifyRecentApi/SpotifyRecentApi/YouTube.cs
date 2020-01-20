using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using Newtonsoft.Json;

namespace SpotifyRecentApi
{
    public static class YouTube
    {
        private static string youtubeSearchUrl = "https://www.youtube.com/results?search_query=";
        private static string cacheFile = "ytcache.json";

        public static string GetFirstSongLink(string searchTerm)
        {
            searchTerm = searchTerm.Replace(' ', '+');
            if (!File.Exists(cacheFile))
                File.WriteAllText(cacheFile, "[]");

            List<CacheYTSong> cachedSongs = JsonConvert.DeserializeObject<List<CacheYTSong>>(File.ReadAllText(cacheFile));
            if (cachedSongs.Any(s => s.Query.Equals(searchTerm)))
                return cachedSongs.First(s => s.Query.Equals(searchTerm)).YoutubeLink;

            string searchLink = youtubeSearchUrl + searchTerm;

            Console.WriteLine($"[YouTube] Searching for {searchLink}");
            string searchPageHtml = new WebClient().DownloadString(searchLink);

            Regex ABlockRegex = new Regex("<a.*class=\"yt-uix-tile-link.*\".*dir=\"ltr\">");
            string firstATag = ABlockRegex.Matches(searchPageHtml).FirstOrDefault().Value;
            Regex hrefParser = new Regex(@"\/watch\?v\=.{11}");
            string link = $"https://www.youtube.com{hrefParser.Match(firstATag).Value}";

            cachedSongs.Add(new CacheYTSong { Query = searchTerm, YoutubeLink = link});
            File.WriteAllText(cacheFile, JsonConvert.SerializeObject(cachedSongs, Formatting.Indented));

            return link;
        }
    }

    public class CacheYTSong
    {
        public string Query { get; set; }
        public string YoutubeLink { get; set; }

        public CacheYTSong()
        {

        }
    }
}
