﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorldCountries.Common.Models
{
    public class CountriesResponse
    {
        public string name { get; set; }
        public List<string> topLevelDomain { get; set; }
        public string alpha2Code { get; set; }
        public string alpha3Code { get; set; }
        public List<string> callingCodes { get; set; }
        public string capital { get; set; }
        public List<object> altSpellings { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public int population { get; set; }
        public List<object> latlng { get; set; }
        public string demonym { get; set; }
        public double? area { get; set; }
        public double? gini { get; set; }
        public List<string> timezones { get; set; }
        public List<object> borders { get; set; }
        public string nativeName { get; set; }
        public string numericCode { get; set; }
        public List<CurrencyResponse> currencies { get; set; }
        public List<LanguageResponse> languages { get; set; }
        public TranslationsResponse translations { get; set; }
        public string flag { get; set; }
        public List<object> regionalBlocs { get; set; }
        public string cioc { get; set; }
    }
}
