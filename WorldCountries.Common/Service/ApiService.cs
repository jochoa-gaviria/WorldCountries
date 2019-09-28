using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorldCountries.Common.Models;

namespace WorldCountries.Common.Service
{
    class ApiService : IApiService
    {
        public async Task<bool> CheckConnectionAsync(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }

            return await CrossConnectivity.Current.IsRemoteReachable(url);
        }

        public async Task<Response<CountriesResponse>> GetCountriesAsync(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject("all");
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<CountriesResponse>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var token = JsonConvert.DeserializeObject<CountriesResponse>(result);
                return new Response<CountriesResponse>
                {
                    IsSuccess = true,
                    Result = token
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
