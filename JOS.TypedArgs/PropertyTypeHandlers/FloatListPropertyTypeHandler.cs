using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.TypedArgs.PropertyTypeHandlers
{
	[PropertyTypeHandler(PropertyType = typeof(List<float>))]
	public class FloatListPropertyTypeHandler : IPropertyTypeHandler
	{
		private static readonly FloatPropertyTypeHandler FloatPropertyTypeHandler = new FloatPropertyTypeHandler();
		public object GetTypedValue(object propertyValue) {
			var separatedValues = propertyValue
				.ToString()
				.Split(TypedArgsSettings.Separator)
				.Select(x => FloatPropertyTypeHandler.GetTypedValue(x))
				.ToArray();
			var floatValues = separatedValues.Select(x => (float) x);
			var instance = Activator.CreateInstance(typeof(List<float>));
			var method = instance.GetType().GetMethod("AddRange");
			method.Invoke(instance, new object[] { floatValues });
			return instance;
		}
	}
}
