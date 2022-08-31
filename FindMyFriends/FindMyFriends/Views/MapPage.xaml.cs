using System;
using System.Collections.Generic;
using FindMyFriends.ViewModels;
using Xamarin.Forms;

namespace FindMyFriends.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage(string UserId,string Username)
        {
            InitializeComponent();
            BindingContext = new MapPageViewModel(UserId,Username);
        }
    }
}

