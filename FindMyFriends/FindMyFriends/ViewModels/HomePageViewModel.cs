using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FindMyFriends.Models;
using FindMyFriends.Services;
using FindMyFriends.Views;
using Firebase.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CartModel> Carts { get; private set; }
        = new ObservableCollection<CartModel>();

        private CartModel _selectedItem;
        private string _imageUrl, _username, _userId, _lastIssued;
        private ICommand _mapCommand, _selectionChangedCommand;

        public CartModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
            }
        }


        public ICommand MapCommand
        {
            get
            {
                if (_mapCommand == null)
                {
                    _mapCommand = new Command(Map);
                }
                return _mapCommand;
            }
            set
            {
                _mapCommand = value;
            }

        }
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand == null)
                {
                    _selectionChangedCommand = new Command(SelectionChanged);
                }
                return _mapCommand;
            }
            set
            {
                _selectionChangedCommand = value;
            }

        }

        private void SelectionChanged(object obj)
        {
            
        }

        public String Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public String ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }
        public String UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }
        public String LastIssued
        {
            get => _lastIssued;
            set
            {
                _lastIssued = value;
                OnPropertyChanged();
            }
        }
        public HomePageViewModel()
        {
            setAsync();

            
            

        }

        public async void setAsync()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            var user = await firebaseDatabase.getUserAsync(Preferences.Get("AccesToken", String.Empty));
            Carts.Add(new CartModel
            {
                Username = user.Username,
                UserId = user.UserID,
                About = user.About,
                ImageUrl = user.ImageUrl,
                LastIssued = "30/08/2022 19:00:59"
            });
            Carts.Add(new CartModel
            {
                Username = user.Username,
                UserId = user.UserID,
                About = user.About,
                ImageUrl = user.ImageUrl,
                LastIssued = "30/08/2022 19:00:59"
            });
            Carts.Add(new CartModel
            {
                Username = user.Username,
                UserId = user.UserID,
                About = user.About,
                ImageUrl = user.ImageUrl,
                LastIssued = "30/08/2022 19:00:59"
            });
            Carts.Add(new CartModel
            {
                Username = user.Username,
                UserId = user.UserID,
                About = user.About,
                ImageUrl = user.ImageUrl,
                LastIssued = "30/08/2022 19:00:59"
            });
            Carts.Add(new CartModel
            {
                Username = user.Username,
                UserId = user.UserID,
                About = user.About,
                ImageUrl = user.ImageUrl,
                LastIssued = "30/08/2022 19:00:59"
            });

        }
        public async void Map()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            //await App.Current.MainPage.DisplayAlert("ok", SelectedItems.Count.ToString(), "ok");
            await App.Current.MainPage.DisplayAlert("ok",SelectedItem.Username, "s");
            //await App.Current.MainPage.Navigation.PushAsync(new MapPage());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

