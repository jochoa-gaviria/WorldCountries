using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private string _filter;
        private ObservableCollection<CountriesItemViewModel> _countries;
        private DelegateCommand _searchCommand;
        private DelegateCommand _sortByNameCommand;
        private DelegateCommand _sortBySizeCommand;
        private DelegateCommand _sortByPopulationCommand;
        private bool _sortName = true;
        private bool _sortSize = true;
        private bool _sortPopulation = true;

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
        public string Filter
        {
            get { return _filter; }
            set
            {
                SetProperty(ref _filter, value);
                SearchCountryAsync();
            }
        }
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(SearchCountryAsync));
        public DelegateCommand SortByNameCommand => _sortByNameCommand ?? (_sortByNameCommand = new DelegateCommand(SortByName));
        public DelegateCommand SortBySizeCommand => _sortBySizeCommand ?? (_sortBySizeCommand = new DelegateCommand(SortBySize));
        public DelegateCommand SortByPopulationCommand => _sortByPopulationCommand ?? (_sortByPopulationCommand = new DelegateCommand(SortByPopulation));

        public ObservableCollection<CountriesItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private async void SearchCountryAsync()
        {
            var response = LoadPersistenceCountries();
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"The filter has an error, try again please", "Accept");
                return;
            }
            Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).Where(c => c.Name.ToLower().Contains(Filter.ToLower()) ||
            c.Capital.ToLower().Contains(Filter.ToLower())));
        }

        private async void SortByName()
        {
            var response = LoadPersistenceCountries();
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "The filter has an error, try again please", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(Filter))
            {
                if (_sortName)
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).OrderBy(c => c.Name));
                    _sortName = false;
                }
                else
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).OrderByDescending(c => c.Name));
                    _sortName = true;
                }
                
            }
            else
            {
                if (_sortName)
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).Where(c => c.Name.ToLower().Contains(Filter.ToLower()) ||
                    c.Capital.ToLower().Contains(Filter.ToLower())).OrderBy(c => c.Name));
                    _sortName = false;
                }
                else
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).Where(c => c.Name.ToLower().Contains(Filter.ToLower()) ||
                    c.Capital.ToLower().Contains(Filter.ToLower())).OrderByDescending(c => c.Name));
                    _sortName = true;
                }
            }
        }

        private async void SortBySize()
        {
            var response = LoadPersistenceCountries();
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "The filter has an error, try again please", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(Filter))
            {
                if (_sortSize)
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).OrderBy(c => c.Area));
                    _sortSize = false;
                }
                else
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).OrderByDescending(c => c.Area));
                    _sortSize = true;
                }

            }
            else
            {
                if (_sortSize)
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).Where(c => c.Name.ToLower().Contains(Filter.ToLower()) ||
                    c.Capital.ToLower().Contains(Filter.ToLower())).OrderBy(c => c.Area));
                    _sortSize = false;
                }
                else
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).Where(c => c.Name.ToLower().Contains(Filter.ToLower()) ||
                    c.Capital.ToLower().Contains(Filter.ToLower())).OrderByDescending(c => c.Area));
                    _sortSize = true;
                }
            }
        }

        private async void SortByPopulation()
        {
            var response = LoadPersistenceCountries();
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "The filter has an error, try again please", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(Filter))
            {
                if (_sortPopulation)
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).OrderBy(c => c.Population));
                    _sortPopulation = false;
                }
                else
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).OrderByDescending(c => c.Population));
                    _sortPopulation = true;
                }

            }
            else
            {
                if (_sortPopulation)
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).Where(c => c.Name.ToLower().Contains(Filter.ToLower()) ||
                    c.Capital.ToLower().Contains(Filter.ToLower())).OrderBy(c => c.Population));
                    _sortPopulation = false;
                }
                else
                {
                    Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response).Where(c => c.Name.ToLower().Contains(Filter.ToLower()) ||
                    c.Capital.ToLower().Contains(Filter.ToLower())).OrderByDescending(c => c.Population));
                    _sortPopulation = true;
                }
            }
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
                ShowCountriesAsync(response);
            }
            else
            {
                var response = await _apiService.GetCountriesAsync(url, "/rest", "/v2", "/all");
                ShowCountriesAsync(response);
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

        private async void ShowCountriesAsync(Response<CountriesResponse> response)
        {
            if (!response.IsSuccess)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "The countries didn't charge suscessful", "Accept");
                return;
            }
            var countries = response.Result;
            Settings.Countries = JsonConvert.SerializeObject(countries);
            Countries = new ObservableCollection<CountriesItemViewModel>(ToListCountries(response));
            IsRunning = false;
        }
        private ObservableCollection<CountriesItemViewModel> ToListCountries(Response<CountriesResponse> response)
        {
            var countries = response.Result;
            return new ObservableCollection<CountriesItemViewModel>(countries.Select(c => new CountriesItemViewModel(_navigationService)
            {
                Name = c.Name,
                Capital = c.Capital,
                Region = c.Region,
                Alpha3Code = c.Alpha3Code,
                Population = c.Population,
                Flag = c.Flag,
                Alpha2Code = c.Alpha2Code,
                Subregion = c.Subregion,
                Demonym = c.Demonym,
                Area = c.Area,
                Gini = c.Gini,
                NativeName = c.NativeName,
                NumericCode = c.NumericCode,
                Cioc = c.Cioc,
                TopLevelDomain = c.TopLevelDomain,
                CallingCodes = c.CallingCodes,
                AltSpellings = c.AltSpellings,
                Latlng = c.Latlng,
                Timezones = c.Timezones,
                Borders = c.Borders,
                Currencies = c.Currencies,
                Languages = c.Languages,
                Translations = c.Translations,
                RegionalBlocs = c.RegionalBlocs
            }).ToList());
        }
    }
}
