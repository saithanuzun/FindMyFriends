using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FindMyFriends.Models;
using FindMyFriends.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        private ICommand _textChangedCommand;
        private string _imageUrl;
        private string _username;
        private string _email;
        private string _userId;
        private string _about;
        private int _friendsCount;

        public ICommand DeleteAccountCommand
        {
            get
            {
                if (_textChangedCommand == null)
                {
                    _textChangedCommand = new Command(TextChanged);
                }
                return _textChangedCommand;
            }
            set
            {
                _textChangedCommand = value;
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
        private void TextChanged()
        {

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

