using CoffeeAPI.Models;
using CoffeeAPI.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace CoffeeAPI.Controllers
{
    [ApiController]
    [Route("brew-coffee")]
    public class BrewCoffeeController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<BrewCoffeeResponse>> Get()
        {
            var numCalls = IncrementNumCalls();
            if (numCalls % 5 == 0) //503 service unavailable on every 5th call
            {
                return StatusCode(503, "");
            }

            var selectedDate = DateTimeProvider.Now;
            if (selectedDate.Day == 1 && selectedDate.Month == 4) //418 error on April fools day
            {
                return StatusCode(418, "");
            }

            var temperature = await GetTemperature();
            return new BrewCoffeeResponse()
            {
                Message = (temperature <= 30)? "Your piping hot coffee is ready" : "Your refreshing iced coffee is ready",
                Prepared = DateTimeUtil.GetISO8601Format(selectedDate)
            };
        }

        //hardcoded to Sydney, docs are here: https://open-meteo.com/en/docs
        private static readonly string URI = "https://api.open-meteo.com/v1/forecast?latitude=-33.8688&longitude=151.2093&current=temperature_2m";

        public static async Task<decimal> GetTemperature()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(URI);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var parsedObject = JsonConvert.DeserializeObject<OpenMeteoResponse>(responseBody);
            return parsedObject.current.temperature_2m;
        }

        private static long IncrementNumCalls()
        {
            long temp = 0;
            lock (lockNumCalls)
            {
                numCalls++;
                temp = numCalls;
            }
            return temp;
        }
        private static object lockNumCalls = new object();
        private static long numCalls = 0;
    }
}
