using Autobarn.Data.Entities;
using GraphQL.Types;

namespace Autobarn.Website.GraphQL.GraphTypes {
	public sealed class CarGraphType : ObjectGraphType<Car> {
		public CarGraphType() {
			Name = "car";
			Field(c => c.CarModel, nullable: false, type: typeof(CarModelGraphType))
				.Description("The model of this particular car");
			Field(c => c.Registration);
			Field(c => c.Color);
			Field(c => c.Year);
		}
	}
}