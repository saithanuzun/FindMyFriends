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


        public ObservableCollection<Pin> Pins { get; set; } = new ObservableCollection<Pin>();

        private string _username, _userId;
        private ICommand _backButtonCommand;


        public string Username
        {
            get => _username;
            set
            {
                _username = value;
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


        public MapPageViewModel(String UserId, string Username)
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
                    Pins.Clear();
                    Pins.Add(new Pin
                    {
                        Position = new Position(d.Object.PositionLatitude, d.Object.PositionLongitude),
                        Label = Username,
                        Address = d.Object.IssuedDate

                    });
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

