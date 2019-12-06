using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecentApi
{
    public class History
    {
        public Track CurrentTrack { get; set; }
        public List<Track> Tracks { get; set; }
        public History()
        {
            Tracks = new List<Track>();
        }
    }

    public class Track
    {
        public string Name { get; set; }
        public List<string> Artists { get; set; }

        public Track()
        {
        }
    }
}
