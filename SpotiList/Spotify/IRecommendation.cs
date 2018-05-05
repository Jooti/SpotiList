using SpotiList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotiList.Spotify
{
    public interface IRecommendation
    {
        Task<List<MiniTrack>> GetRecommendationsAsync(RecommendFormViewModel form);
    }
}
