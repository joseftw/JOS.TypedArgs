using System;
using JOS.TypedArgs;

namespace JOS.Console
{
	[PropertyTypeHandler(PropertyType = typeof(char))]
	public class CharPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue)
		{
			return Convert.ToChar(propertyValue);
		}
	}
}
