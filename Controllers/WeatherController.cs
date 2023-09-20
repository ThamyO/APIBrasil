using Microsoft.AspNetCore.Mvc;

namespace BrasilApiApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using BrasilApiApp.Services;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using BrasilApiApp.Models;

namespace BrasilApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IBrasilApiService _brasilApiService;
        private readonly string _connectionString;

        public WeatherController(IBrasilApiService brasilApiService, IConfiguration configuration)
        {
            _brasilApiService = brasilApiService;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("city/{cityName}")]
        public async Task<IActionResult> GetWeatherByCity(string cityName)
        {
            try
            {
                var result = await _brasilApiService.GetWeatherByCity(cityName);
                // Persistindo dados no SQL Server
                using var connection = new SqlConnection(_connectionString);
                var query = "INSERT INTO Weather (City, Temperature) VALUES (@City, @Temperature)";
                await connection.ExecuteAsync(query, result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Salvar logs no Sql Server caso aconteça algum erro
                using var connection = new SqlConnection(_connectionString);
                var logQuery = "INSERT INTO Logs (Message, Timestamp) VALUES (@Message, @Timestamp)";
                await connection.ExecuteAsync(logQuery, new Log { Message = ex.Message, Timestamp = DateTime.UtcNow });
                return BadRequest("An error occurred. Check the logs for details.");
            }
        }

        [HttpGet("airport/{icaoCode}")]
        public async Task<IActionResult> GetWeatherByAirport(string icaoCode)
        {
            try
            {
                var result = await _brasilApiService.GetWeatherByAirport(icaoCode);
                // Persistindo dados no SQL Server
                using var connection = new SqlConnection(_connectionString);
                var query = "INSERT INTO Weather (City, Temperature) VALUES (@City, @Temperature)";
                await connection.ExecuteAsync(query, result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Salvar logs no Sql Server caso aconteça algum erro
                using var connection = new SqlConnection(_connectionString);
                var logQuery = "INSERT INTO Logs (Message, Timestamp) VALUES (@Message, @Timestamp)";
                await connection.ExecuteAsync(logQuery, new Log { Message = ex.Message, Timestamp = DateTime.UtcNow });
                return BadRequest("An error occurred. Check the logs for details.");
            }
        }
    }
}
