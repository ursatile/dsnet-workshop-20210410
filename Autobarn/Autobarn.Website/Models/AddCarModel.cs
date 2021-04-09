using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Autobarn.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autobarn.Website.Models {
	public class AddCarModel {
		[Required(ErrorMessage = "Please enter your vehicle registration")]
		[RegularExpression(@"[a-zA-Z0-9 \-]{1,12}$",
			ErrorMessage = "Please enter 1-15 characters A-Z and 0-9 only!")]
		public string Registration { get; set; }

		[Required(ErrorMessage = "Please enter your vehicle year")]
		[Range(1920, 2021, ErrorMessage = "Please enter a 4-digit year between 1920 and 2021")]
		public int Year { get; set; }

		[Required(ErrorMessage = "Please provide a colour")]
		public string Color { get; set; }

		[Required(ErrorMessage = "Please choose a make and model")]
		public string CarModel { get; set; }

		public IEnumerable<SelectListItem> Models { get; set; }
	}
}