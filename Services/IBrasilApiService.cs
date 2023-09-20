using System;

namespace BrasilApiApp.Services
{
    public interface IBrasilApiService
    {
        Task<WeatherResponse> GetWeatherByCity(string cityName);
        Task<WeatherResponse> GetWeatherByAirport(string icaoCode);
    }
}
