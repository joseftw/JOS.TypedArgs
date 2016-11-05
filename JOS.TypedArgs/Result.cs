using System.Collections.Generic;

namespace JOS.TypedArgs
{
	public class Result
	{
		public Result()
		{
			Errors = new List<Error>();
		}
		public bool Success => Errors.Count == 0;
		public List<Error> Errors { get; set; }
	}
}
