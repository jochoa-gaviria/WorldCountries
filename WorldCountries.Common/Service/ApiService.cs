using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorldCountries.Common.Models;

namespace WorldCountries.Common.Service
{
    public class ApiService : IApiService
    {
        public async Task<bool> CheckConnectionAsync(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }

            return await CrossConnectivity.Current.IsRemoteReachable(url);
        }

        public async Task<Response<CountriesResponse>> GetCountriesAsync(string urlBase,
            string servicePrefix,
            string controller,
            string request)
        {
            try
            {
                /* var requestString = JsonConvert.SerializeObject(request);
                 var content = new StringContent(requestString, Encoding.UTF8, "application/json");*/
                var client = new HttpClient();
              //  {
                //    BaseAddress = new Uri(urlBase)
               // };

                var url = $"{urlBase}{servicePrefix}{controller}{request}";
                //var url = "/rest/v2/all";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<CountriesResponse>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var countries = JsonConvert.DeserializeObject<List<CountriesResponse>>(result);
                return new Response<CountriesResponse>
                {
                    IsSuccess = true,
                    Result = countries
                    //Message = "Ok"
                };
            }
            catch (Exception ex)
            {
                return new Response<CountriesResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
