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
            Helpers.Geolocation.Instance.LocationServices();
        }
        
    }
}
