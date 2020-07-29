using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyRecentApi
{
    public static class YouTube
    {
        private static string youtubeSearchUrl = "https://www.youtube.com/results?search_query=";
        private static string cacheFile = "files/ytcache.json";

        public static string GetFirstSongLink(string searchTerm)
        {
            searchTerm = searchTerm.Replace(' ', '+');
            searchTerm = searchTerm.Replace("&", "%26");
            searchTerm = searchTerm.Replace("?", "%3F");
            searchTerm = searchTerm.Replace("+", "%2B");

            if (!File.Exists(cacheFile))
            {
                Directory.CreateDirectory("files");
                File.WriteAllText(cacheFile, "[]");
            }

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
            if (link != null)
            {
                cachedSongs.Add(new CacheYTSong { Query = searchTerm, YoutubeLink = link });
                File.WriteAllText(cacheFile, JsonConvert.SerializeObject(cachedSongs, Formatting.Indented));

                return link;
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                return GetFirstSongLink(searchTerm);
            }
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
