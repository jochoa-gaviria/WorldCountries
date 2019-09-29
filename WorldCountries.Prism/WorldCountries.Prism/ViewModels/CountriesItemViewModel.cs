using System;
using Prism.Commands;
using Prism.Navigation;
using WorldCountries.Common.Models;

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

        private void SelectCountry()
        {
            /*var countries = response.Result;
            var parameters = new NavigationParameters
            {
                { "countries", countries }
            };

            await _navigationService.NavigateAsync("PropertiesPage", parameters*/
        }
    }
}
