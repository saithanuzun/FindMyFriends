using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FindMyFriends.Models;
using Xamarin.Forms;

namespace FindMyFriends.ViewModels
{
    public class MapPageViewModel
    {
        public ObservableCollection<Location> Locations = new ObservableCollection<Location>();

        private ICommand _backButtonCommand;
        public ICommand BackButtonCommand
        {
            get
            {
                if (_backButtonCommand == null)
                {
                    _backButtonCommand = new Command(BackButton);
                }
                return _backButtonCommand;
            }
            set
            {
                _backButtonCommand = value;
            }

        }


        public MapPageViewModel()
        {
        }

        private async void BackButton()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

    }
}

