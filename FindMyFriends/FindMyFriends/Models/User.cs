using System;
namespace FindMyFriends.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Friends { get; set; }

    }
}


