using System.Collections.Generic;

namespace SpotiList.Models
{
    public class MiniAlbum
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<MiniArtist> Artists{ get; set; }
    }
}