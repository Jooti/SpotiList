using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SpotiList.Models;

namespace SpotiList.Spotify
{
    public class Recommendation : SpotifyGetData, IRecommendation
    {
        private readonly string _url = "https://api.spotify.com/v1/recommendations";

        public Recommendation(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<List<MiniTrack>> GetRecommendationsAsync(RecommendFormViewModel form)
        {
            List<MiniTrack> tracks = new List<MiniTrack>();
            form.Artists = form.Artists ?? new List<string>();
            form.Tracks = form.Tracks ?? new List<string>();

            var result = await GetSpotifyDataAsync(
                $"{_url}/?seed_artists={string.Join(",",form.Artists)}&seed_tracks={string.Join(",",form.Tracks)}");
            if (result == null)
                return null;
            tracks = result["tracks"]
                .Select(x => x.ToObject<MiniTrack>()).ToList();

            return tracks;
        }

        
    }
}
