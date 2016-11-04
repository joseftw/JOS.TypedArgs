using System.Globalization;

namespace JOS.TypedArgs.PropertyTypeHandlers
{
	[PropertyTypeHandler(PropertyType = typeof(float))]
	public class FloatPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue)
		{
			return float.Parse(propertyValue.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
		}
	}
}
