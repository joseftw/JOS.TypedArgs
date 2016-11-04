using JOS.TypedArgs;

namespace JOS.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var typedArgs = ArgsHelper<TypedArguments>.GetTypedArgs(args);
		}
	}
}
