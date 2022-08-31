using System;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;
using Xamarin.Essentials;

namespace FindMyFriends.Services
{
    public class firebaseStorage
    {
        FirebaseStorage Storage = new FirebaseStorage("findmyfriends-7a08b.appspot.com");


        public async Task<String> upload(FileResult photo)
        {

            var task = Storage
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

            return await task;
      
        }
        public async Task<string> UploadProfileImage(Stream fileStream, string fileName)
        {
            var imageUrl = await Storage
                .Child("ProfileImages")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }


    }
}

