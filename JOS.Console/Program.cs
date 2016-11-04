using JOS.TypedArgs;

namespace JOS.Console
{
	class Program
	{
		static void Main(string[] args) {
			var typeArgs = new ArgsHelper().GetTypedArgs<TypedArguments>(args);
		}
	}
}
