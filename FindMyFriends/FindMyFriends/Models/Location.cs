using System;
using Plugin.Geolocator.Abstractions;

namespace FindMyFriends.Models
{
    public class Location :AccesToken
    {
        public double PositionLatitude { get; set; }
        public double PositionLongitude { get; set; }
        public string IssuedDate { get; set; }

    }
}


