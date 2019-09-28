using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldCountries.Common.Models;

namespace WorldCountries.Common.Service
{
    public interface IApiService
    {
        Task<Response<CountriesResponse>> GetCountriesAsync(
            string urlBase,
            string servicePrefix,
            string controller);


        Task<bool> CheckConnectionAsync(string url);
    }
}
