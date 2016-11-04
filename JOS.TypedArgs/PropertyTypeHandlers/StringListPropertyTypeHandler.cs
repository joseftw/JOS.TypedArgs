using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.TypedArgs.PropertyTypeHandlers
{
	[PropertyTypeHandler(PropertyType = typeof(List<string>))]
	public class StringListPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue) {
			var separatedValues = propertyValue.ToString().Split(TypedArgsSettings.Separator).ToArray();
			var instance = Activator.CreateInstance(typeof(List<string>));
			var method = instance.GetType().GetMethod("AddRange");
			method.Invoke(instance, new[] { separatedValues });
			return instance;
		}
	}
}
