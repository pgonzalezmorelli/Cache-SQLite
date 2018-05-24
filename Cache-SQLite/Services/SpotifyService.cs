using System;
using System.Net.Http;
using System.Threading.Tasks;
using CacheSQLite.Models;
using Newtonsoft.Json;

namespace CacheSQLite.Services
{
    public class SpotifyService
    {
        public SpotifyService()
        {
        }

        public async Task<SpotifyAlbums> GetAlbums()
        {
            HttpClient client = new HttpClient();
            string fileIndex = string.Empty;
            fileIndex = FileIndexBaseOnSeconds();
            string url = string.Format("https://raw.githubusercontent.com/seba47/Cache-SQLite/master/FakeServiceResponses/spotify{0}secs.json", fileIndex);
            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SpotifyAlbums>(content);

        }

        private static string FileIndexBaseOnSeconds()
        {
            string fileIndex = string.Empty;
            int seconds = DateTime.Now.Second;

            if (seconds >= 0 && seconds <= 10)
            {
                fileIndex = "10";
            }
            else if (seconds >= 11 && seconds <= 20)
            {
                fileIndex = "20";
            }
            else if (seconds >= 21 && seconds <= 30)
            {
                fileIndex = "30";
            }
            else if (seconds >= 31 && seconds <= 40)
            {
                fileIndex = "40";
            }
            else if (seconds >= 41 && seconds <= 50)
            {
                fileIndex = "50";
            }
            else if (seconds >= 51 && seconds <= 60)
            {
                fileIndex = "60";
            }
            return fileIndex;
        }
    }
}
