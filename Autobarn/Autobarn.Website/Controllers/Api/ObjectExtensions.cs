using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace Autobarn.Website.Controllers.Api {
    public static class ObjectExtensions {
		public static dynamic ToDynamic(this object value) {
			IDictionary<string, object> expando = new ExpandoObject();
			var properties = TypeDescriptor.GetProperties(value.GetType());
			foreach (PropertyDescriptor property in properties) {
				expando.Add(property.Name, property.GetValue(value));
			}
			return (ExpandoObject)expando;
		}
	}
}