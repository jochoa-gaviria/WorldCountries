using Xamarin.Forms;
using Xamarin.Forms.Maps;
using WorldCountries.Common.Helpers;
using Newtonsoft.Json;
using WorldCountries.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WorldCountries.Prism.Views
{
    public partial class MapCountryPage : ContentPage
    {
        public MapCountryPage()
        {
            InitializeComponent();
            MoveMapToCurrentPosition();
        }
        public CountriesResponse Country { get; set; }


        private void MoveMapToCurrentPosition()
        {
            Country = JsonConvert.DeserializeObject<CountriesResponse>(Settings.Country);
            //await _geolocatorService.GetLocationAsync();
            double latitude = Convert.ToDouble(Country.Latlng.First());
            double longitude = Convert.ToDouble(Country.Latlng.Last());
            if (latitude != 0 && longitude != 0)
            {
                var position = new Position(
                    latitude,
                    longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromKilometers(200)));
                MyMap.Pins.Add(new Pin
                {
                    Label = Country.Name,
                    Position = new Position(latitude, longitude),
                    Type = PinType.SearchResult
                });
            }
        }

    }
}
