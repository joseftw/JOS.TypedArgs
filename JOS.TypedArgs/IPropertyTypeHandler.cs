namespace JOS.TypedArgs
{
	public interface IPropertyTypeHandler {
		object GetTypedValue(object propertyValue);
	}
}
