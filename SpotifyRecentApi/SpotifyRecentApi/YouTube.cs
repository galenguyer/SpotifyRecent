using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static string cacheFile = "files/ytcache.json";

        public static string GetFirstSongLink(string searchTerm)
        {
            if (!File.Exists(cacheFile))
            {
                Directory.CreateDirectory("files");
                File.WriteAllText(cacheFile, "[]");
            }

            List<CacheYTSong> cachedSongs = JsonConvert.DeserializeObject<List<CacheYTSong>>(File.ReadAllText(cacheFile));
            if (cachedSongs.Any(s => s.Query.Equals(searchTerm)))
                return cachedSongs.First(s => s.Query.Equals(searchTerm)).YoutubeLink;

            Console.WriteLine($"[YouTube] Searching for {searchTerm}");

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "youtube-dl",
                    Arguments = $"\"ytsearch:{searchTerm.Replace("\"", "\\\"")}\" --get-id",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            string searchResult = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            string link = $"https://www.youtube.com/watch?v={searchResult}";

            cachedSongs.Add(new CacheYTSong { Query = searchTerm, YoutubeLink = link });
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
