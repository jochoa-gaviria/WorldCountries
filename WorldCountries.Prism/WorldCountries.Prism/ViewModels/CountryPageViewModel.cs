using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorldCountries.Common.Helpers;
using WorldCountries.Common.Models;

namespace WorldCountries.Prism.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        private CountriesResponse _country;
        public CountryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Details";
        }
        public CountriesResponse Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Country = JsonConvert.DeserializeObject<CountriesResponse>(Settings.Country);
        }
    }
}
