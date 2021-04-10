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
    }
}