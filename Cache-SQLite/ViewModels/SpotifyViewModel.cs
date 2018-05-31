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
        private ObservableCollection<UIItems> pairedAlbums = new ObservableCollection<UIItems>();
        private bool isBusy;
        private bool isUpdating;
        private string lastUpdate = string.Empty;

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

        public ObservableCollection<UIItems> PairedAlbums
        {
            get
            {
                return this.pairedAlbums;
            }
            set
            {
                this.pairedAlbums = value;
                this.RaisePropertyChanged();
            }
        }

        public string LastUpdate
        {
            get
            {
                return this.lastUpdate;
            }
            set
            {
                this.lastUpdate = value;
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
                var newPairedItems = Utilities.GetUIItemsList(result.Data.albums.items);
                PairedAlbums =  new ObservableCollection<UIItems>(newPairedItems.items);
                IsBusy = false;
                IsUpdating = true;
            }
            else
            {
                // Waiting for service response
                IsBusy = true;
            }
        }


        // Callback function //
        private Task SetAlbumsAsync(Cached<SpotifyAlbums> result, Exception exception)
        {
            // Result from service
            if (exception != null)
                throw exception;

            if (result != null && result.Data != null)
            {
                var newPairedItems = Utilities.GetUIItemsList(result.Data.albums.items);
                PairedAlbums = new ObservableCollection<UIItems>(newPairedItems.items);
                IsBusy = false;
            }

            if (result != null)
            {
                LastUpdate = result.LastModificationDescription();
            }

            IsBusy = false;
            IsUpdating = false;

            return Task.FromResult(false);
        }
    }
}
