﻿using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FindMyFriends.Views;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class SignUpPageViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _phoneNumber;
        private string _password;
        private string _password2;
        private ICommand _signUpClickedCommand;
        private ICommand _loginClickedCommand;

        public String Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public String PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public String Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public String Password2
        {
            get => _password2;
            set
            {
                _password2 = value;
                OnPropertyChanged();
            }
        }
        public ICommand SignUpClickedCommand
        {
            get
            {
                if (_signUpClickedCommand == null)
                {
                    _signUpClickedCommand = new Command(SignUpClicked);
                }
                return _signUpClickedCommand;
            }
            set
            {
                _signUpClickedCommand = value;
            }

        }
        public ICommand LoginClickedCommand
        {
            get
            {
                if (_loginClickedCommand == null)
                {
                    _loginClickedCommand = new Command(LoginClicked);
                }
                return _loginClickedCommand;
            }
            set
            {
                _loginClickedCommand = value;
            }

        }


        public SignUpPageViewModel()
        {

        }

        
        public void SignUpClicked()
        {

        }
        public async void LoginClicked()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

