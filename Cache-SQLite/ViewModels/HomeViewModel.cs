using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CacheSQLite.Models;
using CacheSQLite.Services;
using Xamarin.Forms;

namespace CacheSQLite.ViewModels
{
    public class HomeViewModel : BindableObject, INotifyPropertyChanged
    {
        #region Property change
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private IEnumerable<Item> albums = new List<Item>();
        public ICommand ReloadCommand {
            get{
                return new Command(async () => await ReloadAlbums());
            }
        }

        public IEnumerable<Item> Albums
        {
            get
            {
                return this.albums;
            }
            set
            {
                this.albums = value;
                this.RaisePropertyChanged();
            }
        }

        public HomeViewModel()
        {
            Task.Run(async () =>
            {
                Albums = await  LoadAlbums();
            });
        }

        private async Task<List<Item>> LoadAlbums(){
            SpotifyService s = new SpotifyService();
            var list = await s.GetAlbums();
            return (List<Item>)list.albums.items;
        }

        private async Task ReloadAlbums()
        {
            Albums = await LoadAlbums();
        }
    }
}
