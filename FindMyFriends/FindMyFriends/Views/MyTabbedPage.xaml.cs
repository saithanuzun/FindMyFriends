using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindMyFriends.Models;
using FindMyFriends.Services;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Location = FindMyFriends.Models.Location;

namespace FindMyFriends.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTabbedPage : TabbedPage
    {
        public MyTabbedPage()
        {
            InitializeComponent();
            LocationServices();

        }
        public async void LocationServices()
        {
            var permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationAlways>();

            if (permission == Xamarin.Essentials.PermissionStatus.Denied)
                return;

            if (Plugin.Geolocator.CrossGeolocator.Current.IsListening)
            {
                await Plugin.Geolocator.CrossGeolocator.Current.StopListeningAsync();
            }

            await Plugin.Geolocator.CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(20),300);

            Plugin.Geolocator.CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }
        private void Current_PositionChanged(object sender, PositionEventArgs e)
        {
            Console.WriteLine(" " + e.Position.Latitude + "  " + e.Position.Longitude);
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            firebaseDatabase.putLocation(new Location
            {
                UserID = Preferences.Get("AccesToken", String.Empty),
                PositionLongitude = e.Position.Longitude,
                PositionLatitude= e.Position.Latitude,
                IssuedDate= DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss"),

        }, Preferences.Get("AccesToken", String.Empty)) ;
        }
    }
}
