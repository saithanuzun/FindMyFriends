using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private ICommand _loginClickedCommand;
        private ICommand _signUpClickedCommand;

        public String Username
        {
            get => _username;
            set
            {
                _username = value;
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

        public void LoginClicked()
        {

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

