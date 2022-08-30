using System;
using Firebase.Storage;

namespace FindMyFriends.Services
{
    public class FirebaseStorage2
    {

        

        public async void upload()
        {
            var task = new FirebaseStorage("gs://findmyfriends-7a08b.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("ProfileImages");
      
        }
        public void getImage()
        {

        }

    }
}

