using System;
using System.Collections.Generic;
using FindMyFriends.ViewModels;
using Xamarin.Forms;

namespace FindMyFriends.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();
        }

    }
}

