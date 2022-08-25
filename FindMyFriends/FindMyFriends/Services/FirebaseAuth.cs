using System;
using System.Threading.Tasks;
using FindMyFriends.Views;
using Firebase.Auth;
using Xamarin.Essentials;

namespace FindMyFriends.Services
{
    public class FirebaseAuth
    {
        public string WebApiKey = Constants.WebApiKey;

        public  async Task<bool> Login(string Username,string Password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Username, Password);
                Preferences.Set("AccesToken", auth.User.LocalId);
         
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
                return false;
            }
            return true;
        }

        public async Task<bool> SignUp(String Username,String Password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Username, Password);
                string GetToken = auth.FirebaseToken;
                Preferences.Set("AccesToken", auth.User.LocalId);
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
                return false;
            }
            return true;
        }

    }
}

