using System;
using FindMyFriends.Services;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;
using FindMyFriends.Models;

namespace FindMyFriends.Helpers
{
    public class Geolocation
    {
        //Singleton
        private static Geolocation _instance;
        public static Geolocation Instance
        {
            get
            {
                if(_instance==null)
                {
                    _instance = new Geolocation();
                }
                return _instance;
            }
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

            await Plugin.Geolocator.CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(20), 300);

            Plugin.Geolocator.CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
        }


        private void Current_PositionChanged(object sender, PositionEventArgs e)
        {
            Console.WriteLine(" " + e.Position.Latitude + "  " + e.Position.Longitude);
            FirebaseDatabase firebaseDatabase = new FirebaseDatabase();
            firebaseDatabase.putLocation(new Models.Location
            {
                UserID = Preferences.Get("AccesToken", String.Empty),
                PositionLongitude = e.Position.Longitude,
                PositionLatitude = e.Position.Latitude,
                IssuedDate = DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss"),

            }, Preferences.Get("AccesToken", String.Empty));
        }
    }
}

