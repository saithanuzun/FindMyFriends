using System;
namespace FindMyFriends.Models
{
    public class User: AccesToken
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string About { get; set; }
        public int FriendsCount { get; set; }

    }
}


