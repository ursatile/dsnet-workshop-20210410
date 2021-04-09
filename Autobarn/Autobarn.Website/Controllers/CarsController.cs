using System.Collections.Generic;
using Autobarn.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using Autobarn.Data.Entities;
using Autobarn.Website.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autobarn.Website.Controllers {
	public class CarsController : Controller {
		private readonly ICarDatabase database;
		private readonly ILogger<CarsController> logger;

		public CarsController(ICarDatabase database, ILogger<CarsController> logger) {
			this.database = database;
			this.logger = logger;
		}

		public IActionResult Index() {
			var cars = database.Cars;
			return View(cars);
		}

		public IActionResult Models() {
			var makes = database.Makes;
			return View(makes);
		}

		public IActionResult Model(string id) {
			var carModel = database.Models.FirstOrDefault(m => m.Code == id);
			if (carModel == null) return NotFound("Sorry - we don't recognise that model code!");
			return View(carModel);
		}

		[HttpPost]
		public IActionResult Add(AddCarModel data) {
			if (ModelState.IsValid) {
				var carModel = database.Models.FirstOrDefault(m => m.Code == data.CarModel);
				var car = new Car {
					CarModel = carModel,
					Registration = data.Registration,
					Color = data.Color,
					Year = data.Year
				};
				database.AddCar(car);
				return RedirectToAction("Index");
			}

			data.Models = ListModels;
			return View(data);
		}

		private IEnumerable<SelectListItem> ListModels =>
			database.Models
				.Select(m => new SelectListItem(m.ToString(), m.Code))
				.OrderBy(item => item.Text);

		[HttpGet]
		public IActionResult Add(Car car) {
			var model = new AddCarModel {
				CarModel = car.CarModel?.Code,
				Year = car.Year,
				Registration = car.Registration,
				Color = car.Color,
				Models = ListModels
			};
			return View(model);
		}
	}
}