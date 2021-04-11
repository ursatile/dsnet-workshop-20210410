using System;

namespace Autobarn.Messages {
	public class NewCarMessage {   
        ///<summary>Registration number of the car we've added</summary>
		public string Registration { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public string Color { get; set; }
		public int Year { get; set; }
		public DateTimeOffset DateAdded { get; set; }
	}

    public class NewCarPriceMessage : NewCarMessage {
        public int Price {get; set; }
        public string Currency {get;set;}
    }
}

