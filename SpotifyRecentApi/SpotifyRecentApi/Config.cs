using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecentApi
{
    public class Config
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ValidEmail { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public long ExpiresEpoch { get; set; } //DateTimeOffset.UnixEpoch.ToUnixTimeSeconds()

        public Config()
        {
        }

        public Config Load(string filename = "config.json")
        {
            if (!File.Exists(filename))
            {
                Save();
                return this;
            }
            var tmpConfig = JsonConvert.DeserializeObject<Config>(File.ReadAllText(filename));
            return tmpConfig;
        }

        public void Save()
        {
            File.WriteAllText("config.json", JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
