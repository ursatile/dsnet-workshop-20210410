using System.Collections.Generic;
using Autobarn.Data.Entities;

namespace Autobarn.Data {
	public interface ICarDatabase {
		IList<Car> Cars { get; }
		IList<Make> Makes { get; }
		IList<CarModel> Models { get; }
		Car FindCar(string registration);
		void AddCar(Car car);
	}
}