using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FindMyFriends.Views;
using Xamarin.Essentials;

namespace FindMyFriends
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Preferences.Clear();
            var AccesToken = Preferences.Get("AccesToken",String.Empty);
            if(AccesToken!=String.Empty)
            {
                MainPage = new NavigationPage(new SignUpPage());
            }
            else
            {
                MainPage = new MyTabbedPage();
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
