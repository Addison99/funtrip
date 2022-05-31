using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FunTrip.Models;
using System.Linq;
using System;

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            var listInt = Enumerable.Range(1, 5).Select(index => new Car
            {
                Name = "Car",
                Speed = 100,
                Id = 1
            });
            var listString =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = 50,
                Summary = "Abc"
            })
            .ToArray(); ;

            return new { listInt, listString };
        }
        
    }
}
