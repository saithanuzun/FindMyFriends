using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FindMyFriends.Services;
using Firebase.Storage;
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
                    _changePasswordCommand = new Command(ChangePasswordAsync);
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
                    _deleteAccountCommand = new Command(DeleteAccountAsync);
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

        async void ChangePicture()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            var user = await firebaseDatabase.getUserAsync(Preferences.Get("AccesToken", String.Empty));

            var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            if (photo == null)
                return;

            firebaseStorage firebaseStorage = new firebaseStorage();
            var task = await firebaseStorage.UploadProfileImage(await photo.OpenReadAsync(), photo.FileName);
            user.ImageUrl = task;
            firebaseDatabase.putUser(user, user.UserID);

            ImageUrl = user.ImageUrl;

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
                    item.FriendsCount = ++item.FriendsCount;
                    firebaseDatabase.putUser(item, item.UserID);
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
                    item.FriendsCount = --item.FriendsCount;
                    firebaseDatabase.putUser(item, item.UserID);
                    await App.Current.MainPage.DisplayAlert("alert", "User Has Been Deleted", "ok");
                    return;
                }
            }

            await App.Current.MainPage.DisplayAlert("alert", "User Has Not Been Found", "ok");

        }
        async void ChangePasswordAsync()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();

            var user = await firebaseDatabase.getUserAsync(Preferences.Get("AccesToken", String.Empty));
            if (CurrentPassword == user.Password)
            {
                user.Password = NewPassword;
                firebaseDatabase.putUser(user, user.UserID);
                FirebaseAuth auth = new FirebaseAuth();
                auth.changePassword(user.UserID, NewPassword);
                await App.Current.MainPage.DisplayAlert("Alert", "Password Has Been Changed", "ok");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Error", "ok");

            }
        }

        [Obsolete]
        async void DeleteAccountAsync()
        {
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();

            var user = await firebaseDatabase.getUserAsync(Preferences.Get("AccesToken", String.Empty));
            if (user.Password == Password)
            {
                FirebaseAuth auth = new FirebaseAuth();
                await App.Current.MainPage.DisplayAlert("Alert", "Account Has Been Deleted", "ok");
                auth.deleteUser(Preferences.Get("AccesToken", String.Empty));
                Preferences.Clear();
                System.Environment.Exit(0);

            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

