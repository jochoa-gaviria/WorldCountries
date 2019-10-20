using Prism;
using Prism.Ioc;
using WorldCountries.Common.Service;
using WorldCountries.Prism.ViewModels;
using WorldCountries.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WorldCountries.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU5NjU0QDMxMzcyZTMzMmUzMGorR2RMTlpkbDNYN09zTW5BaG9FTEJCZDJXa28rQXVyNzJRSGFUSEhMZWc9");
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainCountriesPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainCountriesPage, MainCountriesPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryTabbedPage, CountryTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryPage, CountryPageViewModel>();
            containerRegistry.RegisterForNavigation<MapCountryPage, MapCountryPageViewModel>();
        }
    }
}
