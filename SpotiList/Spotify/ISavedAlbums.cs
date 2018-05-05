using SpotiList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotiList.Spotify
{
    public interface ISavedAlbums
    {
        Task<List<MiniAlbum>> GetAlbumsAsync();
        Task<List<MiniArtist>> GetArtistsAsync();
    }
}