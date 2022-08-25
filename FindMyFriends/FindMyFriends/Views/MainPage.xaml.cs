using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace FindMyFriends
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationAlways>();

            if (permission == Xamarin.Essentials.PermissionStatus.Denied)
                return;

            if(Plugin.Geolocator.CrossGeolocator.Current.IsListening)
            {
                await Plugin.Geolocator.CrossGeolocator.Current.StopListeningAsync();
            }

            await Plugin.Geolocator.CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1),10);
             
            Plugin.Geolocator.CrossGeolocator.Current.PositionChanged+=Current_PositionChanged;            
        }

        private void Current_PositionChanged(object sender, PositionEventArgs e)
        {
            label.Text = " " + e.Position.Latitude + "  " + e.Position.Longitude;
            Console.WriteLine(" " + e.Position.Latitude + "  " + e.Position.Longitude);
        }
    }
}
