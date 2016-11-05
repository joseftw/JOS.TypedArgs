using Shouldly;
using Xunit;

namespace JOS.TypedArgs.Tests
{
	public class PropertyTypeHandlerTests
	{
		[Fact]
		public void GivenBoolType_WhenGetPropertyTypeHandler_ShouldReturnOverridenBoolPropertyHandler()
		{
			var result = PropertyTypeHandler.GetPropertyTypeHandler(typeof(bool));

			result.ShouldBeOfType(typeof(OverriddenBoolPropertyTypeHandler));
		}
	}
}
