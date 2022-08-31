using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FindMyFriends.Models;
using FindMyFriends.Services;
using Firebase.Database.Query;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FindMyFriends.ViewModels
{
    public class MapPageViewModel : INotifyPropertyChanged
    {

        private Position _position;
        private string _lastIssued,_username,_userId;

        public string LastIssued
        {
            get => _lastIssued;
            set
            {
                _lastIssued = value;
                OnPropertyChanged();

            }
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();

            }
        }

        public Position Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();

            }
        }

        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
                
            }
        }
        private ICommand _backButtonCommand;


        public ICommand BackButtonCommand
        {
            get
            {
                if (_backButtonCommand == null)
                {
                    _backButtonCommand = new Command(BackButton);
                }
                return _backButtonCommand;
            }
            set
            {
                _backButtonCommand = value;
                OnPropertyChanged();
            }

        }


        public MapPageViewModel(String UserId,string Username)
        {
            this.UserId = UserId;
            this.Username = Username;

            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            firebaseDatabase.firebaseClient
                .Child("Locations")
                .Child(UserId)
                .AsObservable<Models.Location>()
                .Subscribe(d =>
                {
                    Position = new Position(d.Object.PositionLatitude, d.Object.PositionLongitude);
                    LastIssued = d.Object.IssuedDate;
                });
        }

        private async void BackButton()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

      



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

