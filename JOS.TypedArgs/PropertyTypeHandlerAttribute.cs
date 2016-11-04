using System;

namespace JOS.TypedArgs
{
	public class PropertyTypeHandlerAttribute : Attribute
	{
		public Type PropertyType { get; set; }
	}
}
