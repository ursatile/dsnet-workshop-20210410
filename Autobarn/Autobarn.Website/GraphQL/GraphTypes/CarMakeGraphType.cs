using Autobarn.Data.Entities;
using GraphQL.Types;

namespace Autobarn.Website.GraphQL.GraphTypes {
	public sealed class CarMakeGraphType : ObjectGraphType<Make> {
		public CarMakeGraphType() {
			Name = "make";
			Field(c => c.Name).Description("The name of the manufacturer, e.g. Tesla, Volkswagen, Ford");
		}
	}
}