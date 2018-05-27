using System.Threading.Tasks;
using CacheSQLite.Models;

namespace CacheSQLite.Services
{
    public interface ISpotifyService
    {
        Task<SpotifyAlbums> GetAlbumsAsync();
    }
}
