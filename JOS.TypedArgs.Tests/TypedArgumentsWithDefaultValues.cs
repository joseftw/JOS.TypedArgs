using System.Collections.Generic;

namespace JOS.TypedArgs.Tests
{
	public class TypedArgumentsWithDefaultValues
	{
		public string FilePath { get; set; } = "any path";
		public int FileCount { get; set; } = 10;
		public float Degrees { get; set; } = 100f;
		public string NumberAsString { get; set; } = "any string";
		public List<string> Humans { get; set; } = new List<string> {"Josef Ottosson", "Silvia"};
		public List<int> Ages { get; set; } = new List<int> {10, 20, 30};
		public List<float> Temperatures { get; set; } = new List<float> {10f, 20f, 30f, 45.5f};
		public bool Verbose { get; set; } = true;
	}
}
