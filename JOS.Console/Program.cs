using JOS.TypedArgs;

namespace JOS.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			ArgsHelper<TypedArguments>.SetTypedArgs(args);
			var hej = ArgsHelper<TypedArguments>.Value;
		}
	}
}
