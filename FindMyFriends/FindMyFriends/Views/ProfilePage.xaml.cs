﻿using System;
using System.Collections.Generic;
using FindMyFriends.ViewModels;
using Xamarin.Forms;

namespace FindMyFriends.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfilePageViewModel();
        }
    }
}

