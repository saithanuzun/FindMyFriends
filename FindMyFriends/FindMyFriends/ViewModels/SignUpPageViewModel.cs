using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FindMyFriends.Models;
using FindMyFriends.Services;
using FindMyFriends.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class SignUpPageViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _email;
        private string _password;
        private string _password2;
        private ICommand _signUpClickedCommand;
        private ICommand _loginClickedCommand;

        public String Username
        {
            get => _username;
            set
            {
                _username = value;
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

        public String Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public String Password2
        {
            get => _password2;
            set
            {
                _password2 = value;
                OnPropertyChanged();
            }
        }
        public ICommand SignUpClickedCommand
        {
            get
            {
                if (_signUpClickedCommand == null)
                {
                    _signUpClickedCommand = new Command(SignUpClickedAsync);
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

        
        public SignUpPageViewModel()
        {
            


        }


        public async void SignUpClickedAsync()
        {
            FirebaseAuth Auth = new FirebaseAuth();
            if(Password==Password2 && Username!=null && Email!=null)
            {
                if (await Auth.SignUp(Email, Password))
                {
                    User user = new User
                    {
                         UserID = Preferences.Get("Accestoken", string.Empty),
                         Username = Username,
                         Email = Email,
                         Password = Password
                    };

                    

                    await App.Current.MainPage.Navigation.PushAsync(new MyTabbedPage());
                }
                else
                {

                }
            }
            else
            {

            }
            
            
           
        }
        public async void LoginClicked()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

