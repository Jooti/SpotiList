using SpotiList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotiList.Spotify
{
    public interface IRecentTracks
    {
        Task<List<MiniTrack>> GetTracksAsync();
    }
}