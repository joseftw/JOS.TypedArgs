using System.Collections.Generic;

namespace JOS.TypedArgs
{
	public static class TypedArgsSettings {
		public static char Separator { get; set; } = '|';

		public static List<string> IgnoreNamespaces { get; set; } = new List<string>
		{
			"Microsoft",
			"System",
			"mscorlib",
			"vshost"
		};

		public static bool ThrowWhenValidationFails { get; set; } = true;
	}
}
