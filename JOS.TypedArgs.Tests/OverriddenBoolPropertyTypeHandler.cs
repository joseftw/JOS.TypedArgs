namespace JOS.TypedArgs.Tests
{
	[PropertyTypeHandler(PropertyType = typeof(bool))]
	public class OverriddenBoolPropertyTypeHandler : IPropertyTypeHandler
	{
		public object GetTypedValue(object propertyValue)
		{
			if (propertyValue == null)
			{
				return true;
			}

			bool result;
			bool.TryParse(propertyValue.ToString(), out result);
			return result;
		}
	}
}
