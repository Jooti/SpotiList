using SpotiList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotiList.Spotify
{
    public interface IPlaylistSaver
    {
        Task<string> SaveAsync(SavePlaylistFormViewModel form);
    } 
}
