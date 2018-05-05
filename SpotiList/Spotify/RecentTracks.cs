using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SpotiList.Models;

namespace SpotiList.Spotify
{
    public class RecentTracks : SpotifyGetData, IRecentTracks
    {
        private readonly string _url = "https://api.spotify.com/v1/me/player/recently-played";
        public RecentTracks(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<List<MiniTrack>> GetTracksAsync()
        {
            List<MiniTrack> tracks = new List<MiniTrack>();
            var result = await GetSpotifyDataAsync(_url);
            if (result == null)
                return null;
            tracks = result["items"]
                .Select(x => x["track"].ToObject<MiniTrack>()).ToList();

            return tracks;
        }
    }
}
