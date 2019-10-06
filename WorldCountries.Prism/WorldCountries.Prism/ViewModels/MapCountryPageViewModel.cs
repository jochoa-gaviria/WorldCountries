using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldCountries.Prism.ViewModels
{
    public class MapCountryPageViewModel : ViewModelBase
    {
        public MapCountryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Map country page";
        }
    }
}
