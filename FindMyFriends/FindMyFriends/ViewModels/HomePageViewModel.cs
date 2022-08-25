using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FindMyFriends.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public HomePageViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

