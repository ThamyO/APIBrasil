using System;

using BrasilApiApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrasilApiApp.Services
{
    public class BrasilApiService : IBrasilApiService
    {
        private readonly HttpClient _client;

        public BrasilApiService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://brasilapi.com.br/");
        }

        public async Task<WeatherResponse> GetWeatherByCity(string cityName)
        {
            // Primeiro, obtemos o código da cidade
            var cityCodeResponse = await _client.GetAsync($"api/cptec/v1/cidade/{cityName}");
            cityCodeResponse.EnsureSuccessStatusCode();
            var cityCodeJson = await cityCodeResponse.Content.ReadAsStringAsync();
            var cityCodeObj = JsonSerializer.Deserialize<CityCodeResponse>(cityCodeJson);

            // Agora, obtemos o clima usando o código da cidade
            var weatherResponse = await _client.GetAsync($"api/cptec/v1/clima/previsao/{cityCodeObj.Code}");
            weatherResponse.EnsureSuccessStatusCode();
            var weatherJson = await weatherResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherResponse>(weatherJson);
        }

        public async Task<WeatherResponse> GetWeatherByAirport(string icaoCode)
        {
            var response = await _client.GetAsync($"api/cptec/v1/clima/aeroporto/{icaoCode}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherResponse>(jsonResponse);
        }
    }
}
