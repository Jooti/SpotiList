using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotiList.Models
{
    public class HomeViewModel
    {
        public List<MiniTrack> RecentTracks { get; set; }
        public List<MiniArtist> SavedArtists { get; set; }
    }
}
