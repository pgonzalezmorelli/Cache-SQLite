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
    public class SpotifyViewModel : BindableObject, INotifyPropertyChanged
    {
        #region Property change
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private IEnumerable<Item> albums = new List<Item>();
        private bool isBusy;

        public ICommand ReloadCommand {
            get{
                return new Command(async () => await ReloadAlbums());
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                this.RaisePropertyChanged();
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

        public SpotifyViewModel()
        {
            
            Task.Run(async () =>
            {
                IsBusy = true;
                Albums = await  LoadAlbums();
                IsBusy = false;
            });
        }

        private async Task<List<Item>> LoadAlbums(){
            SpotifyService s = new SpotifyService();
            var list = await s.GetAlbums();
            return (List<Item>)list.albums.items;
        }

        private async Task ReloadAlbums()
        {
            IsBusy = true;
            Albums = await LoadAlbums();
            IsBusy = false;
        }
    }
}
