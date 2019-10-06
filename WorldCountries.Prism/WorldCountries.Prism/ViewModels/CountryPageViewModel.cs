using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldCountries.Prism.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        public CountryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Country Page";
        }
    }
}
