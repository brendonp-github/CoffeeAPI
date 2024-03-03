using CoffeeAPI.Controllers;
using CoffeeAPI.Models;
using CoffeeAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeAPI.Tests
{
    public class BrewCoffee_Tests
    {
        [Fact]
        public async Task NormalRun()
        {
            var temperature = await BrewCoffeeController.GetTemperature();
            using (var context = new DateTimeProviderContext(DateTime.Now)) 
            {
                var controller = new BrewCoffeeController();
                for (int iterationNum = 1; iterationNum <= 10; iterationNum++)
                {
                    var response = await controller.Get();
                    Assert.NotNull(response);
                    if (iterationNum % 5 == 0)
                    {
                        Assert.NotNull(response.Result);
                        Assert.True(response.Result.GetType() == typeof(ObjectResult) && ((ObjectResult)response.Result).StatusCode == 503);
                    }
                    else
                    {
                        Assert.NotNull(response.Value);
                        var expectedPreparedValue = $"{DateTimeProvider.UtcNow:yyyy-MM-ddTHH:mm:ss}{DateTimeProvider.Now:zzz}";
                        Assert.True(
                               response.Value.Message == ((temperature <= 30) ? "Your piping hot coffee is ready" : "Your refreshing iced coffee is ready")
                            && response.Value.Prepared == expectedPreparedValue
                            );
                    }
                }
            }
        }

        [Fact]
        public async Task AprilFoolsRun()
        {
            var temperature = await BrewCoffeeController.GetTemperature();
            var fakeDateTime = new DateTime(2023, 4, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond); //April fools day 2023
            using (var context = new DateTimeProviderContext(fakeDateTime))
            {
                var controller = new BrewCoffeeController();
                for (int iterationNum = 1; iterationNum <= 10; iterationNum++)
                {
                    var overallTop = await controller.Get();
                    Assert.NotNull(overallTop);
                    Assert.NotNull(overallTop.Result);
                    ActionResult<BrewCoffeeResponse> overallMiddle = overallTop.Result;
                    var response = overallMiddle.Result;
                    Assert.NotNull(response);

                    if (iterationNum % 5 == 0)
                    {
                        Assert.True(response.GetType() == typeof(ObjectResult) && ((ObjectResult)response).StatusCode == 503);
                    }
                    else
                    {
                        Assert.True(response.GetType() == typeof(ObjectResult) && ((ObjectResult)response).StatusCode == 418);
                    }
                }
            }
        }
    }
}
