using System.Collections.Generic;
using Autobarn.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Autobarn.Data.Entities;
using Autobarn.Website.Models;
using Autobarn.Website.Models.Api;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;

namespace Autobarn.Website.Controllers.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase {
        
        ICarDatabase database;
        
        public CarsController(ICarDatabase database) {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get(int index = 0, int count = 10) {
            var cars = database.Cars.Skip(index).Take(count);
            var total = database.Cars.Count();
            dynamic links = new ExpandoObject();
            links.first = new { href = $"/api/cars" };
            links.final = new { href = $"/api/cars?index={total - (total % count)}" };
            if (index > 0) {
                links.previous = new { href = $"/api/cars?index={index - count}"};
            }
            if (index+count < total) {
                links.next = new { href = $"/api/cars?index={index+count}"};
            }

            var result = new {
                _links = links,
                index = index,
                count = count,
                total = total,
                items = cars
            };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id) {
            var car = database.Cars.FirstOrDefault(c => c.Registration.Equals(id, System.StringComparison.InvariantCultureIgnoreCase));
            if (car == default) return NotFound($"Sorry, we don't have a car with registration {id}");
            return Ok(car);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, PutCar body) {
	        var existingCar = database.FindCar(id);
	        var model = database.Models.FirstOrDefault(m => m.Code == body.ModelCode);
	        if (model == default) return BadRequest($"Sorry; {body.ModelCode} is not a valid model code");
	        if (existingCar == default) {
		        var car = new Car() {
			        Registration = id,
			        Color = body.Color,
			        Year = body.Year,
			        CarModel = model
		        };
		        this.database.AddCar(car);
		        return Created("TODO", car);
	        }
	        else {
		        existingCar.Color = body.Color;
		        existingCar.Year = body.Year;
		        existingCar.CarModel = model;
		        return Accepted(existingCar);
	        }
        }
    }
}