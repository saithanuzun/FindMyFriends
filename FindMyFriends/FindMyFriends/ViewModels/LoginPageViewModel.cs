using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FindMyFriends.Models;
using FindMyFriends.Services;
using FindMyFriends.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private ICommand _loginClickedCommand;
        private ICommand _signUpClickedCommand;

        public String Email
        {
            get => _email;
            set
            {
                _email = value;
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



        public LoginPageViewModel()
        {

        }
        public async void LoginClicked()
        {
            FirebaseAuth Auth = new FirebaseAuth();
            if(await Auth.Login(_email, Password))
            {
                User user = new User
                {
                    UserID = Preferences.Get("Accestoken", string.Empty),
                    Email = Email,
                    Password = Password
                };
                await App.Current.MainPage.Navigation.PushAsync(new MyTabbedPage());
                
            }
            else
            {

            }


        }
        public async void SignUpClicked()
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

