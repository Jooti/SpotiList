using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotiList.Models
{
    public class SavePlaylistFormViewModel
    {
        public List<string> Tracks { get; set; }
        public string Name { get; set; }

        internal IEnumerable<string> GenerateSpotifyUris()
        {
            return Tracks.Select(x => $"spotify:track:{x}");
        }
    }
}
