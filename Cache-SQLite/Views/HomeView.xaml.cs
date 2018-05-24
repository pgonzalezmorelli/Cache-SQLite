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
            BindingContext = new HomeViewModel();
            InitializeComponent();
        }
    }
}
