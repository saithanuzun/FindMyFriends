using System;
using System.Collections.Generic;
using FindMyFriends.ViewModels;

using Xamarin.Forms;

namespace FindMyFriends.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpPageViewModel();
        }


        
    }
}

