using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        public ObservableCollection<CardModel> Cards { get; private set; }
        = new ObservableCollection<CardModel>();

        private CardModel _selectedItem;
        private string _imageUrl, _username, _userId, _lastIssued;
        private ICommand _mapCommand, _refreshCommand;
        private bool _isRefreshing, _isVisible;

        public bool IsRefreshing
        {
            get
            {

                return _isRefreshing;

            }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public bool IsVisible
        {
            get
            {

                return _isVisible;

            }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
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
        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new Command(async () => await RefreshItemsAsync());
                }
                return _refreshCommand;
            }
            set
            {
                _refreshCommand = value;
                OnPropertyChanged();
            }

        }

        private async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(1));

            setAsync();

            IsRefreshing = false;
        }

        public CardModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
            }
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
            Cards.Clear();
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            var Friends = await firebaseDatabase.getFriends(Preferences.Get("AccesToken", String.Empty));
            if (Friends.Count == 0)
            {
                IsVisible = true;
                return;
            }
            IsVisible = false;
            var Users = await firebaseDatabase.getAllUserAsync();

            foreach (var item in Friends)
            {
                foreach (var item2 in Users)
                {
                    if (item.UserID == item2.UserID)
                    {
                        var location = await firebaseDatabase.getLocationAsync(item.UserID);
                        Cards.Add(new CardModel
                        {
                            UserId = item2.UserID,
                            Username = item2.Username,
                            ImageUrl = item2.ImageUrl,
                            About = item2.About,
                            LastIssued = location.IssuedDate,
                        });
                        break;
                    }
                }
            }
        }

        public async void Map()
        {
            if (SelectedItem == null)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Select the User", "ok");
                return;

            }

            await App.Current.MainPage.Navigation.PushAsync(new MapPage(SelectedItem.UserId, SelectedItem.Username));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

