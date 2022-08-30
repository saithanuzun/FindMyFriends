using System;
using System.Collections.Generic;
using FindMyFriends.ViewModels;
using Xamarin.Forms;

namespace FindMyFriends.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            BindingContext = new MapPageViewModel();
        }
    }
}

