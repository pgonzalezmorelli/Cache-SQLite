using System;
using System.Threading.Tasks;
using CacheSQLite.Models;

namespace CacheSQLite.Managers
{
    public interface ISpotifyManager
    {
        Task<Cached<SpotifyAlbums>> GetAlbumsAsync(Func<Cached<SpotifyAlbums>, Exception, Task> onUpdate);
    }
}
