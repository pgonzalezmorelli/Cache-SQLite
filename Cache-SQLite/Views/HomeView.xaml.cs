using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CacheSQLite.Services;
using CacheSQLite.ViewModels;
using Xamarin.Forms;

namespace CacheSQLite.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SpotifyView());
        }
    }
}
