using System.Runtime.Serialization;

namespace Example.Shared.WeatherForecast
{
    [DataContract]
    public class WeatherForecastData
    {
        [DataMember(Order = 1)]
        public DateTime Date { get; set; }

        [DataMember(Order = 2)]
        public int TemperatureC { get; set; }

        [DataMember(Order = 3)]
        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
