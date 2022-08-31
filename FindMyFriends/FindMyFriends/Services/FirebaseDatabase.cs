using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FindMyFriends.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = FindMyFriends.Models.Location;
using User = FindMyFriends.Models.User;

namespace FindMyFriends.Services
{
    public class FirebaseDatabase
    {

        public FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient(Constants.FirebaseApiUrl);


        public void getUser(String UserId)
        {
            var User = new ObservableCollection<User>();
            var observable = firebaseClient
                .Child("Locations")
                .Child(UserId)
                .AsObservable<Location>()
                .Subscribe(d =>
                {

                });




        }



        public async Task<User> getUserAsync(string UserId)
        {
            User user = new User();

            var getUser = (await firebaseClient
                .Child("Users")
                .OrderByKey()
                .EqualTo(UserId)
                .OnceAsync<User>());

            foreach (var item in getUser)
            {
                user.UserID = item.Object.UserID;
                user.Username = item.Object.Username;
                user.Email = item.Object.Email;
                user.Password = item.Object.Password;
                user.ImageUrl = item.Object.ImageUrl;
                user.About = item.Object.About;
                user.FriendsCount = item.Object.FriendsCount;
            }

            return user;
        }
        public async Task<Collection<User>> getAllUserAsync()
        {
            var user = new Collection<User>();
            var getUser = (await firebaseClient
                .Child("Users")
                .OnceAsync<User>()).Select(item => new User
                {
                    UserID = item.Object.UserID,
                    Username = item.Object.Username,
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    ImageUrl = item.Object.ImageUrl,
                    About = item.Object.About,
                    FriendsCount = item.Object.FriendsCount,

                });

            foreach (var item in getUser)
            {
                user.Add(item);
            }

            return user;
        }


        public async void putUser(User user, string UserId)
        {
            await firebaseClient
               .Child("Users")
               .Child(UserId)
               .PutAsync(user);
        }


        public async void putLocation(Models.Location location, string UserId)
        {
            await firebaseClient
                .Child("Locations")
                .Child(UserId)
                .Child(UserId)
                .PutAsync(location);
        }
        public async Task<Location> getLocationAsync(string UserId)
        {
            Location location = new Location();

            var getUser = (await firebaseClient
                .Child("Locations")
                .Child(UserId)
                .OrderByKey()
                .EqualTo(UserId)
                .OnceAsync<Location>());

            foreach (var item in getUser)
            {
                location.UserID = item.Object.UserID;
                location.PositionLatitude = item.Object.PositionLatitude;
                location.PositionLongitude = item.Object.PositionLongitude;
                location.IssuedDate = item.Object.IssuedDate;
            }

            return location;
        }

        public async void putFriend(string UserId, string FriendId)
        {
            await firebaseClient
                .Child("Friends")
                .Child(UserId)
                .Child(FriendId)
                .PutAsync<AccesToken>(new AccesToken
                {
                    UserID = FriendId
                });
        }
        public async void DeleteFriend(string UserId, string FriendId)
        {
            await firebaseClient
                .Child("Friends")
                .Child(UserId)
                .Child(FriendId)
                .DeleteAsync();


        }
        public async Task<Collection<AccesToken>> getFriends(string UserId)
        {
            Collection<AccesToken> collection = new Collection<AccesToken>();
            var getFriends = (await firebaseClient
                .Child("Friends")
                .Child(UserId)
                .OnceAsync<AccesToken>()).Select(item => new AccesToken
                {
                    UserID = item.Object.UserID
                });

            foreach (var item in getFriends)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}

