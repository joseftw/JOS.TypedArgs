using System;

namespace JOS.TypedArgs
{
	public class Error
	{
		public string Title { get; set; }
		public string Message { get; set; }

		public override string ToString()
		{
			return $"Title: {Title}{Environment.NewLine}Message: {Message}{Environment.NewLine}";
		}
	}
}
