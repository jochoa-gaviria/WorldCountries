using System;
using Prism.Commands;
using Prism.Navigation;
using WorldCountries.Common.Models;
using WorldCountries.Common.Helpers;
using Newtonsoft.Json;

namespace WorldCountries.Prism.ViewModels
{
    public class CountriesItemViewModel : CountriesResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;
        public CountriesItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ?? (_selectCountryCommand = new DelegateCommand(SelectCountry));

        private async void SelectCountry()
        {
            Settings.Country = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("CountryTabbedPage");
        }
    }
}
