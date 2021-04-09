using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Autobarn.Data.Entities {
	public class Make {
		public string Code { get; set; }
		public string Name { get; set; }

		[JsonIgnore]
		public List<CarModel> Models { get; set; } = new List<CarModel>();
	}
}