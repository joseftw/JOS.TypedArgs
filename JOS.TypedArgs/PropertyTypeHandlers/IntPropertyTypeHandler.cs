namespace JOS.TypedArgs.PropertyTypeHandlers
{
	[PropertyTypeHandler(PropertyType = typeof(int))]
	public class IntPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue) {
			return int.Parse(propertyValue.ToString());
		}
	}
}
