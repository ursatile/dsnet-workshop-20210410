using System.Collections.Generic;
using Autobarn.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Autobarn.Data.Entities;
using Autobarn.Website.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autobarn.Website.Controllers.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase {
        
        ICarDatabase database;
        
        public CarsController(ICarDatabase database) {
            this.database = database;
        }

        [HttpGet]
        public IEnumerable<Car> Get() {
            return database.Cars;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id) {
            var car = database.Cars.FirstOrDefault(c => c.Registration.Equals(id, System.StringComparison.InvariantCultureIgnoreCase));
            if (car == default) return NotFound($"Sorry, we don't have a car with registration {id}");
            return Ok(car);
        }
    }
}