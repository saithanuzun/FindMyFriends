﻿using System;
using System.Collections.Generic;
using FindMyFriends.ViewModels;
using Xamarin.Forms;

namespace FindMyFriends.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsPageViewModel();
        }
    }
}

