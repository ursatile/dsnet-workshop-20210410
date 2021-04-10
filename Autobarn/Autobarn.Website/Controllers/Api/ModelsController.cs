using Autobarn.Data;
using Autobarn.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Autobarn.Website.Controllers.Api {
	[Route("api/[controller]")]
	[ApiController]
	public class ModelsController : ControllerBase {
		private ICarDatabase database;

		public ModelsController(ICarDatabase database) {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get() {
            return(Ok("It works!"));

        }

        [HttpPost("{id}")]
        public IActionResult Post(string id, Car car) {
            var carModel = database.Models.FirstOrDefault(m => m.Code == id);
            car.CarModel = carModel;
            database.AddCar(car);
            return Created($"/api/cars/{car.Registration}", car);
        }

		[HttpGet("{id}")]
		public IActionResult Get(string id) {
            var carModel = database.Models.FirstOrDefault(m => m.Code == id);
            if (carModel == default) return(NotFound($"No car model with code {id}"));
            var result = carModel.ToDynamic();
            result._actions = new {
                create = new {
                    name = "create",
                    method = "POST",
                    href = $"/api/models/{id}",
                    type = "application/json"
                }
            };
            
            return Ok(result);
		}
	}
}