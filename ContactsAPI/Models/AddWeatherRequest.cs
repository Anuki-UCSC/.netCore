namespace ContactsAPI.Models
{
    public class AddWeatherRequest
    {
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public WeatherName? Summary { get; set; }
    }
}
