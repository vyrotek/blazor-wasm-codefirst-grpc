using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Example.Shared.WeatherForecast
{
    [ServiceContract]
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecastData>> GetForecastAsync(CallContext context = default);
    }
}
