using Example.Shared.WeatherForecast;
using Microsoft.AspNetCore.Authorization;
using ProtoBuf.Grpc;

namespace Example.Server.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [Authorize]
        public async Task<List<WeatherForecastData>> GetForecastAsync(CallContext context)
        {
            return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecastData
            {
                Date = DateTime.Today.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList());
        }
    }
}
