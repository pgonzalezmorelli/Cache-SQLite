using System;
using System.Collections.Generic;
using CacheSQLite.ViewModels;
using Xamarin.Forms;

namespace CacheSQLite.Views
{
    public partial class SpotifyView : ContentPage
    {
        public SpotifyView()
        {
            BindingContext = new SpotifyViewModel();
            InitializeComponent();
        }
    }
}
