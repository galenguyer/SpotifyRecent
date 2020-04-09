using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SpotifyRecentApi
{
    public class Program
    {
        private static History cachedHistory;
        private static long cacheExpiry;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://0.0.0.0:28473");
                });

        public static void EnsureTokenUpToDate()
        {
            var config = new Config().Load();
            if (config.ExpiresEpoch < DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 60)
            {
                WebClient client = new WebClient();
                client.Headers.Clear();
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers[HttpRequestHeader.Authorization] = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.ClientId}:{config.ClientSecret}"))}".Trim();
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("grant_type", "refresh_token");
                reqparm.Add("refresh_token", config.RefreshToken);

                byte[] responsebytes = client.UploadValues("https://accounts.spotify.com/api/token", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                var responseType = new
                {
                    access_token = "",
                    refresh_token = "",
                };

                var response = JsonConvert.DeserializeAnonymousType(responsebody, responseType);

                config.AccessToken = response.access_token;
                config.RefreshToken = response.refresh_token ?? config.RefreshToken;
                config.ExpiresEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600;
                config.Save();
            }
        }

        public static History GetCache()
        {
            if (cacheExpiry < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                return null;
            else return cachedHistory;
        }

        public static void SetCache(History history)
        {
            cachedHistory = history;
            cacheExpiry = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 60;
        }
    }
}
