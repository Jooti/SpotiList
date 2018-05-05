using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotiList.Models
{
    public class MiniTrack
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string Uri { get; set; }
        public string PreviewUrl { get; set; }
        public int Popularity { get; set; }
        public string Name { get; set; }
        public List<MiniArtist> Artists { get; set; }
        public MiniAlbum Album { get; set; }

    }
}
