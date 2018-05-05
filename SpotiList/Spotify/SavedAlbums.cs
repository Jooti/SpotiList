using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SpotiList.Models;

namespace SpotiList.Spotify
{
    public class SavedAlbums : SpotifyGetData, ISavedAlbums
    {
        private readonly string _url = "https://api.spotify.com/v1/me/albums";
        public SavedAlbums(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<List<MiniAlbum>> GetAlbumsAsync()
        {
            List<MiniAlbum> albums = new List<MiniAlbum>();
            var result = await GetSpotifyDataAsync(_url);
            if (result == null)
                return null;
            albums = result["items"]
                .Select(x => x["album"].ToObject<MiniAlbum>()).ToList();

            return albums;
        }

        public async Task<List<MiniArtist>> GetArtistsAsync()
        {
            var albums = await GetAlbumsAsync();

            IList<MiniArtist> artists = new List<MiniArtist>();
            albums?.ForEach(x => {
                artists = artists.Concat(x.Artists).ToList();
                });
            return artists.Distinct().ToList();
        }
    }
}
