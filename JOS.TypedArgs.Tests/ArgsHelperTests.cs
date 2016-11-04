﻿using Shouldly;
using Xunit;

namespace JOS.TypedArgs.Tests
{
	public class ArgsHelperTests
	{
		[Fact]
		public void GivenMultipleArgs_WhenGetTypedArgs_ThenShouldReturnCorrectlyTypedArguments()
		{
			string[] args = { "-filePath", "c:\temp",
				"--fileCount", "2",
				"-verbose",
				"-degrees", "37",
				"-numberAsString", "22",
				"-humans", "Josef Ottosson|Carl|Silvia",
				"-ages", "10|1337|2019",
				"-temperatures", "30|20|22|12,3"};
			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);
			result.FileCount.ShouldBe(2);
			result.FilePath.ShouldBe("c:\temp");
			result.Degrees.ShouldBe(37);
			result.NumberAsString.ShouldBe("22");
			result.Temperatures.Count.ShouldBe(4);
			result.Ages.Count.ShouldBe(3);
			result.Humans.Count.ShouldBe(3);
			result.Humans.ShouldContain("Josef Ottosson");
			result.Humans.ShouldContain("Carl");
			result.Humans.ShouldContain("Silvia");
			result.Verbose.ShouldBe(true);
		}

		[Fact]
		public void GivenBoolArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args =  {"-verbose"};

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.Verbose.ShouldBe(true);
		}

		[Fact]
		public void GivenFloatArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-degrees", "37.8"};

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.Degrees.ShouldBe(37.8f);
		}

		[Fact]
		public void GivenFloatArgumentWithComma_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = { "-degrees", "37,8" };

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.Degrees.ShouldBe(37.8f);
		}

		[Fact]
		public void GivenStringArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-filePath", "c:\\temp"};

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.FilePath.ShouldBe("c:\\temp");
		}

		[Fact]
		public void GivenIntArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-fileCount", "2"};

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.FileCount.ShouldBe(2);
		}

		[Fact]
		public void GivenIntListArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-ages", "10|1337|2019"};

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.Ages.Count.ShouldBe(3);
			result.Ages.ShouldContain(10);
			result.Ages.ShouldContain(1337);
			result.Ages.ShouldContain(2019);
		}

		[Fact]
		public void GivenStringListArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-humans", "Josef Ottosson|Carl|Silvia"};

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.Humans.Count.ShouldBe(3);
			result.Humans.ShouldContain("Josef Ottosson");
			result.Humans.ShouldContain("Carl");
			result.Humans.ShouldContain("Silvia");
		}

		[Fact]
		public void GivenFloatListArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-temperatures", "12|20|22.2|19,2"};

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);

			result.Temperatures.Count.ShouldBe(4);
			result.Temperatures.ShouldContain(12);
			result.Temperatures.ShouldContain(20);
			result.Temperatures.ShouldContain(22.2f);
			result.Temperatures.ShouldContain(19.2f);
		}

		[Fact]
		public void GivenCustomListSeparator_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			TypedArgsSettings.Separator = '&';
			string[] args = { "-temperatures", "12&20&22.2&19,2" };

			var result = ArgsHelper<TypedArguments>.GetTypedArgs(args);
			TypedArgsSettings.Separator = '|';

			result.Temperatures.Count.ShouldBe(4);
			result.Temperatures.ShouldContain(12);
			result.Temperatures.ShouldContain(20);
			result.Temperatures.ShouldContain(22.2f);
			result.Temperatures.ShouldContain(19.2f);
		}

		[Fact]
		public void WhenGetTypedArgs_ThenValueShouldBePopulated()
		{
			string[] args = { "-temperatures", "12|20|22.2|19,2" };

			ArgsHelper<TypedArguments>.GetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Temperatures.Count.ShouldBe(4);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(12);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(20);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(22.2f);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(19.2f);
		}
	}
}
