using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FindMyFriends.Models;
using FindMyFriends.Services;
using Plugin.Share;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        private ICommand _changeAboutCommand,_shareCommand;
        private string _entry;
        private string _imageUrl;
        private string _username;
        private string _email;
        private string _userId;
        private string _about;
        private int _friendsCount;



        public ICommand ChangeAboutCommand
        {
            get
            {
                if (_changeAboutCommand == null)
                {
                    _changeAboutCommand = new Command(ChangeAbout);
                }
                return _changeAboutCommand;
            }
            set
            {
                _changeAboutCommand = value;
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                if (_shareCommand == null)
                {
                    _shareCommand = new Command(async () =>
                    {
                        var massage = new Plugin.Share.Abstractions.ShareMessage();
                        massage.Text = UserId;
                        massage.Title = "Title";
                        await CrossShare.Current.Share(massage);
                    });
                }
                return _shareCommand;
            }
            set
            {
                _shareCommand = value;
            }
        }

        public String Entry
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public String Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
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
        public String About
        {
            get => _about;
            set
            {
                _about = value;
                OnPropertyChanged();
            }
        }

        public int FriendsCount
        {
            get => _friendsCount;
            set
            {
                _friendsCount = value;
                OnPropertyChanged();
            }
        }
        private async void ChangeAbout()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            var user = await firebaseDatabase.getUserAsync(Preferences.Get("AccesToken", String.Empty));
            user.About = Entry;

            firebaseDatabase.putUser(user, user.UserID);
            setAsync();
        }


        public ProfilePageViewModel()
        {
            setAsync();
        }

        public async void setAsync()
        {
            FirebaseDatabase firebasedatabase = new FirebaseDatabase();

            var user = await firebasedatabase.getUserAsync(Preferences.Get("AccesToken", String.Empty));

            Email = user.Email;
            ImageUrl = user.ImageUrl;
            Username = user.Username;
            Email = user.Email;
            UserId = user.UserID;
            About = user.About;
            FriendsCount = user.FriendsCount;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

