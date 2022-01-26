
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCClientCred.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeathers()
        {
            var url = @"https://localhost:5101/weatherforecast";
            var responseString = await _httpClient.GetStringAsync(url);
            var result = JsonSerializer.Deserialize<List<WeatherForecast>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
