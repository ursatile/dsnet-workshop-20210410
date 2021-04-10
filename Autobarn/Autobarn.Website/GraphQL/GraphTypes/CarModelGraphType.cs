using Autobarn.Data.Entities;
using GraphQL.Types;

namespace Autobarn.Website.GraphQL.GraphTypes {
	public sealed class CarModelGraphType : ObjectGraphType<CarModel> {
		public CarModelGraphType() {
			Name = "model";
			Field(m => m.Name).Description("The name of this model, e.g. Golf, Beetle, 5 Series, Model X");
			Field(m => m.Make, type: typeof(CarMakeGraphType)).Description("The make of this model of car");
		}
	}
}