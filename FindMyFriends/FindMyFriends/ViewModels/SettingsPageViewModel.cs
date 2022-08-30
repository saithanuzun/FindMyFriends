using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FindMyFriends.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        private string _imageUrl, _friendUid, _currentPassword, _newPassword, _password;
        private ICommand _changePictureCommand, _addFriendCommand, _removeFriendCommand, _changePasswordCommand, _deleteAccountCommand;

        public SettingsPageViewModel()
        {
            SetImageUrl();
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
        public String FriendUid
        {
            get => _friendUid;
            set
            {
                _friendUid = value;
                OnPropertyChanged();
            }
        }
        public String CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChanged();
            }
        }
        public String NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
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
        public ICommand ChangePictureCommand
        {
            get
            {
                if (_changePictureCommand == null)
                {
                    _changePictureCommand = new Command(ChangePicture);
                }
                return _changePictureCommand;
            }
            set
            {
                _changePictureCommand = value;
            }

        }
        public ICommand AddFriendCommand
        {
            get
            {
                if (_addFriendCommand == null)
                {
                    _addFriendCommand = new Command(AddFriend);
                }
                return _addFriendCommand;
            }
            set
            {
                _addFriendCommand = value;
            }

        }
        public ICommand RemoveFriendCommand
        {
            get
            {
                if (_removeFriendCommand == null)
                {
                    _removeFriendCommand = new Command(RemoveFriendAsync);
                }
                return _removeFriendCommand;
            }
            set
            {
                _removeFriendCommand = value;
            }

        }
        public ICommand ChangePasswordCommand
        {
            get
            {
                if (_changePasswordCommand == null)
                {
                    _changePasswordCommand = new Command(ChangePassword);
                }
                return _changePasswordCommand;
            }
            set
            {
                _changePasswordCommand = value;
            }

        }
        public ICommand DeleteAccountCommand
        {
            get
            {
                if (_deleteAccountCommand == null)
                {
                    _deleteAccountCommand = new Command(DeleteAccount);
                }
                return _deleteAccountCommand;
            }
            set
            {
                _deleteAccountCommand = value;
            }
        }

        async void SetImageUrl()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            var user = await firebaseDatabase.getUserAsync(Preferences.Get("AccesToken", String.Empty));
            ImageUrl = user.ImageUrl;
        }

        void ChangePicture()
        {

        }
        async void AddFriend()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            var Users = await firebaseDatabase.getAllUserAsync();

            foreach (var item in Users)
            {
                if (item.UserID == FriendUid)
                {
                    firebaseDatabase.putFriend(Preferences.Get("AccesToken", string.Empty), FriendUid);
                    await App.Current.MainPage.DisplayAlert("alert", "User Has Been Added", "ok");
                    return;
                }
            }

            await App.Current.MainPage.DisplayAlert("alert", "User Has Not Been Found", "ok");



        }
        async void RemoveFriendAsync()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            var Users = (await firebaseDatabase.getAllUserAsync());

            foreach (var item in Users)
            {
                if (item.UserID == FriendUid)
                {
                    firebaseDatabase.DeleteFriend(Preferences.Get("AccesToken", string.Empty), FriendUid);
                    await App.Current.MainPage.DisplayAlert("alert", "User Has Been Deleted", "ok");
                    return;
                }
            }

            await App.Current.MainPage.DisplayAlert("alert", "User Has Not Been Found", "ok");

        }
        void ChangePassword()
        {

        }
        void DeleteAccount()
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

