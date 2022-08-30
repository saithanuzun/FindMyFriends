using System;
using System.Collections.Generic;
using FindMyFriends.Models;
using FindMyFriends.Services;
using FindMyFriends.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMyFriends.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel();

        }

 
    }
}

