using Autobarn.Data;
using Autobarn.Data.Entities;
using Autobarn.Website.GraphQL.GraphTypes;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autobarn.Website.GraphQL.Queries {
	public class CarQuery : ObjectGraphType {
		private readonly ICarDatabase db;

		public CarQuery(ICarDatabase db) {
			this.db = db;

			Field<ListGraphType<CarGraphType>>("cars", "Query to retrieve all cars", 
			resolve: GetAllCars);

			Field<CarGraphType>("car", "Query to retrieve a specific car",
				new QueryArguments(MakeNonNullStringArgument("registration", "The registration (licence plate) of the car")),
				resolve: GetCar);

			Field<ListGraphType<CarGraphType>>("carsByColor", "Query to retrieve all cars matching the specified color",
				new QueryArguments(MakeNonNullStringArgument("color", "The name of a color, eg 'blue', 'grey'")),
				resolve: GetCarsByColor);
		}

		private QueryArgument MakeNonNullStringArgument(string name, string description) {
			return new QueryArgument<NonNullGraphType<StringGraphType>> {
				Name = name, Description = description
			};
		}

		private List<Car> GetAllCars(IResolveFieldContext<object> context) => 
			db.Cars.ToList();

		private Car GetCar(IResolveFieldContext<object> context) {
			var registration = context.GetArgument<string>("registration");
			return db.FindCar(registration);
		}

		private List<Car> GetCarsByColor(IResolveFieldContext<object> context) {
			var color = context.GetArgument<string>("color");
			var cars = db.Cars.Where(car => car.Color.Contains(color, StringComparison.InvariantCultureIgnoreCase));
			return cars.ToList();
		}
	}
}