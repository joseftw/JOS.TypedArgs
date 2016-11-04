using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.TypedArgs.PropertyTypeHandlers
{
	[PropertyTypeHandler(PropertyType = typeof(List<int>))]
	public class IntListPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue) {
			var separatedValues = propertyValue.ToString().Split(TypedArgsSettings.Separator).Select(int.Parse);
			var instance = Activator.CreateInstance(typeof(List<int>));
			var method = instance.GetType().GetMethod("AddRange");
			method.Invoke(instance, new[] { separatedValues });
			return instance;
		}
	}
}
