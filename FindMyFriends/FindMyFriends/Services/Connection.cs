using FindMyFriends.Services;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderingApp.Services
{
    public class Connection
    {
        public FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient(Constants.FirebaseApiUrl);

    }
}
