using System;
using System.Threading.Tasks;
using CacheSQLite.Models;
using CacheSQLite.Services;

namespace CacheSQLite.Managers
{
    public class SpotifyManager : BaseManager, ISpotifyManager
    {
        #region Attributes & Properties

        private readonly ISpotifyService spotifyService;

        #endregion

        public SpotifyManager(ICacheManager cache = null, ISpotifyService spotifyService = null) : base(cache)
        {
            this.spotifyService = spotifyService ?? new SpotifyService();
        }

        public Task<Cached<SpotifyAlbums>> GetAlbumsAsync(Func<Cached<SpotifyAlbums>, Exception, Task> onUpdate)
        {
            return GetFromCacheAsync(spotifyService.GetAlbumsAsync, onUpdate);
        }
    }
}
