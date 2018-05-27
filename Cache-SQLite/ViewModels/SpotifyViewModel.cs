using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CacheSQLite.Managers;
using CacheSQLite.Models;
using Xamarin.Forms;

namespace CacheSQLite.ViewModels
{
    public class SpotifyViewModel : BindableObject, INotifyPropertyChanged
    {
        #region PropertyChanged

        public new event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Attributes & Properties

        private readonly ISpotifyManager manager;
        private ObservableCollection<Item> albums = new ObservableCollection<Item>();
        private bool isBusy;
        private bool isUpdating;

        public ICommand ReloadCommand
        {
            get
            {
                return new Command(async () => await GetAlbumsAsync());
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

        public bool IsUpdating
        {
            get
            {
                return this.isUpdating;
            }
            set
            {
                this.isUpdating = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<Item> Albums
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

        #endregion

        public SpotifyViewModel(ISpotifyManager manager = null)
        {
            this.manager = manager ?? new SpotifyManager();

            GetAlbumsAsync();
        }

        private Task LoadAlbumsAsync()
        {
            IsBusy = true;
            return GetAlbumsAsync();
        }

        private async Task GetAlbumsAsync()
        {
            var result = await manager.GetAlbumsAsync(SetAlbumsAsync);
            if (result != null && result.Data != null)
            {
                // Cached response
                Albums = new ObservableCollection<Item>(result.Data.albums.items);
                IsBusy = false;
                IsUpdating = true;
            }
            else
            {
                // Waiting for service response
                IsBusy = true;
            }
        }

        private Task SetAlbumsAsync(Cached<SpotifyAlbums> result, Exception exception)
        {
            // Result from service
            if (exception != null)
                throw exception;

            if (result != null && result.Data != null)
            {
                Albums = new ObservableCollection<Item>(result.Data.albums.items);
                IsBusy = false;
            }

            IsBusy = false;
            IsUpdating = false;

            return Task.FromResult(false);
        }
    }
}
