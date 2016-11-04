using JOS.TypedArgs;

namespace JOS.Console
{
	class Program
	{
		static void Main(string[] args) {
			var typeArgs = ArgsHelper<TypedArguments>.GetTypedArgs(args);
		}
	}
}
