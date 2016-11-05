using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JOS.Console
{
	public class TypedArguments
	{
		public string FilePath { get; set; } = "c\\temp";
		public int FileCount { get; set; }
		[Required]
		public float Degrees { get; set; }
		public string NumberAsString { get; set; }
		public List<string> Humans { get; set; }
		public List<int> Ages { get; set; }
		public List<float> Temperatures { get; set; }
		public char Char { get; set; }
		public bool Josef { get; set; }
	}
}
