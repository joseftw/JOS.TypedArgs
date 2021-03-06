﻿using System.Collections.Generic;

namespace JOS.TypedArgs.Tests
{
	public class TypedArguments
	{
		public string FilePath { get; set; }
		public int FileCount { get; set; }
		public float Degrees { get; set; }
		public string NumberAsString { get; set; }
		public List<string> Humans { get; set; }
		public List<int> Ages { get; set; }
		public List<float> Temperatures { get; set; }
		public bool Verbose { get; set; }
	}
}
