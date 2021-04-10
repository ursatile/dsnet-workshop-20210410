using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autobarn.Website.Models.Api {
	public class PutCar {
		public string ModelCode { get; set; }
		public string Registration { get; set; }
		public string Color { get; set; }
		public int Year { get; set; }
	}
}
