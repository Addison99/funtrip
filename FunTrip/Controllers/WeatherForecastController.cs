using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunTrip.Models;
using BusinessObject.Models;
namespace FunTrip.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/getforecasts")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("/getcars")]
        public object GetCars()
        {
            var listInt = Enumerable.Range(1, 5).Select(index => new Car
            {
                Name = "Car",
                Speed = 100,
                Id = 1
            });
            var listString = Enumerable.Range(1, 5).Select(index => new Account
            {
                Email = "tqdat123",
                Id = 1,
                Password ="123",
                RoleId = 1,
                Username = "123",
                
            })
            .ToArray(); ;

            return new { listInt, listString };
        }

    }
}
