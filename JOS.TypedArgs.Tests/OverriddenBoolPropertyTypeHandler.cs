namespace JOS.TypedArgs.Tests
{
	[PropertyTypeHandler(PropertyType = typeof(bool))]
	public class OverriddenBoolPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue)
		{
			return true;
		}
	}
}
