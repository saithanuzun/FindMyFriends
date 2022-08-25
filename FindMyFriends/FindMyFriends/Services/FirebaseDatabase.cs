using System;
using FindMyFriends.Models;
using Firebase.Database;
using FoodOrderingApp.Services;

namespace FindMyFriends.Services
{
    public class FirebaseDatabase
    {

        public FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient(Constants.FirebaseApiUrl);

        public FirebaseDatabase()
        {

        }

        public void getUser()
        {
           
        }
        public void putUser()
        {
           
        }
    }
}

