namespace CoffeeAPI.Models
{
    public class OpenMeteoResponse
    {
        public class Current
        { 
            public decimal temperature_2m { get; set; }
        }
        public Current current { get; set; }
    }

    //Example JSON Response below based off the URI below, we only need current.temperature_2m
    //https://api.open-meteo.com/v1/forecast?latitude=-33.8688&longitude=151.2093&current=temperature_2m
    /*
{
  "latitude": -33.75,
  "longitude": 151.125,
  "generationtime_ms": 0.025033950805664062,
  "utc_offset_seconds": 0,
  "timezone": "GMT",
  "timezone_abbreviation": "GMT",
  "elevation": 86.0,
  "current_units": {
    "time": "iso8601",
    "interval": "seconds",
    "temperature_2m": "°C"
  },
  "current": {
    "time": "2024-02-29T05:45",
    "interval": 900,
    "temperature_2m": 34.3
  }
}
     */


}
