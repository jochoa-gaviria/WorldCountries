using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WorldCountries.Common.Helpers;
using WorldCountries.Common.Models;
using WorldCountries.Common.Service;

namespace WorldCountries.Prism.ViewModels
{
    public class MainCountriesPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private bool _isRunning;
        private ObservableCollection<CountriesItemViewModel> _countries;

        public MainCountriesPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Countries list";
            IsRunning = true;
            LoadCountriesAsycn();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        public ObservableCollection<CountriesItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }


        private async void LoadCountriesAsycn()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                if (Settings.Countries == null)
                {
                    IsRunning = false;
                    await App.Current.MainPage.DisplayAlert("Sorry", "There's not data on memory, check your internet connection", "Accept");
                    return;
                }
                var response = LoadPersistenceCountries();
                ShowCountries(response);

            }
            else
            {
                var response = await _apiService.GetCountriesAsync(url, "/rest", "/v2", "/all");
                ShowCountries(response);
            }

        }

        private Response<CountriesResponse> LoadPersistenceCountries()
        {
            var CountriesKept = JsonConvert.DeserializeObject<List<CountriesResponse>>(Settings.Countries);
            return new Response<CountriesResponse>
            {
                IsSuccess = true,
                Result = CountriesKept
            };
        }
        private async void ShowCountries(Response<CountriesResponse> response)
        {
            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", $"The countries didn't charge suscessful {response.Message}", "Accept");
                return;
            }

            var countries = response.Result;
            Settings.Countries = JsonConvert.SerializeObject(countries);
            Countries = new ObservableCollection<CountriesItemViewModel>(countries.Select(c => new CountriesItemViewModel(_navigationService)
            {
                Name = c.Name,
                Capital = c.Capital,
                Region = c.Region,
                Alpha3Code = c.Alpha3Code,
                Population = c.Population,
                Flag = c.Flag
            }).ToList());
            IsRunning = false;
        }
    }
}
