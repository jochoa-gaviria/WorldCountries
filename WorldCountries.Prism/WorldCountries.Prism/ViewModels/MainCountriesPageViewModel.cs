using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using WorldCountries.Common.Service;

namespace WorldCountries.Prism.ViewModels
{
    public class MainCountriesPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private bool _isRunning;

        public MainCountriesPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Countries main page";
            LoadCountriesAsycn();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private async void LoadCountriesAsycn()
        {
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

            var response = await _apiService.GetCountriesAsync(url, "/rest", "/v2", "/all");

            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", $"The countries didn't charge suscessful {response.Message}", "Accept");
                return;
            }

            /*var countries = response.Result;
            var parameters = new NavigationParameters
            {
                { "countries", countries }
            };

            await _navigationService.NavigateAsync("PropertiesPage", parameters);*/
            Title = "List of countries OK!!!!!!";
            IsRunning = false;
        }
    }
}
