namespace JOS.TypedArgs.PropertyTypeHandlers
{
	[PropertyTypeHandler(PropertyType = typeof(string))]
	public class StringPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue) {
			return propertyValue.ToString();
		}
	}
}
