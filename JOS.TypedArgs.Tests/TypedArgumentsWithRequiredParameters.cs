using System.ComponentModel.DataAnnotations;

namespace JOS.TypedArgs.Tests
{
	public class TypedArgumentsWithRequiredParameters
	{
		[Required]
		public string FilePath { get; set; }
	}
}
