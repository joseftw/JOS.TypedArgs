using Shouldly;
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
			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.FileCount.ShouldBe(2);
			ArgsHelper<TypedArguments>.Value.FilePath.ShouldBe("c:\temp");
			ArgsHelper<TypedArguments>.Value.Degrees.ShouldBe(37);
			ArgsHelper<TypedArguments>.Value.NumberAsString.ShouldBe("22");
			ArgsHelper<TypedArguments>.Value.Temperatures.Count.ShouldBe(4);
			ArgsHelper<TypedArguments>.Value.Ages.Count.ShouldBe(3);
			ArgsHelper<TypedArguments>.Value.Humans.Count.ShouldBe(3);
			ArgsHelper<TypedArguments>.Value.Humans.ShouldContain("Josef Ottosson");
			ArgsHelper<TypedArguments>.Value.Humans.ShouldContain("Carl");
			ArgsHelper<TypedArguments>.Value.Humans.ShouldContain("Silvia");
			ArgsHelper<TypedArguments>.Value.Verbose.ShouldBe(true);
		}

		[Fact]
		public void GivenBoolArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args =  {"-verbose"};

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Verbose.ShouldBe(true);
		}

		[Fact]
		public void GivenFloatArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-degrees", "37.8"};

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Degrees.ShouldBe(37.8f);
		}

		[Fact]
		public void GivenFloatArgumentWithComma_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = { "-degrees", "37,8" };

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Degrees.ShouldBe(37.8f);
		}

		[Fact]
		public void GivenStringArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-filePath", "c:\\temp"};

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.FilePath.ShouldBe("c:\\temp");
		}

		[Fact]
		public void GivenIntArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-fileCount", "2"};

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.FileCount.ShouldBe(2);
		}

		[Fact]
		public void GivenIntListArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-ages", "10|1337|2019"};

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Ages.Count.ShouldBe(3);
			ArgsHelper<TypedArguments>.Value.Ages.ShouldContain(10);
			ArgsHelper<TypedArguments>.Value.Ages.ShouldContain(1337);
			ArgsHelper<TypedArguments>.Value.Ages.ShouldContain(2019);
		}

		[Fact]
		public void GivenStringListArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-humans", "Josef Ottosson|Carl|Silvia"};

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Humans.Count.ShouldBe(3);
			ArgsHelper<TypedArguments>.Value.Humans.ShouldContain("Josef Ottosson");
			ArgsHelper<TypedArguments>.Value.Humans.ShouldContain("Carl");
			ArgsHelper<TypedArguments>.Value.Humans.ShouldContain("Silvia");
		}

		[Fact]
		public void GivenFloatListArgument_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			string[] args = {"-temperatures", "12|20|22.2|19,2"};

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Temperatures.Count.ShouldBe(4);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(12);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(20);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(22.2f);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(19.2f);
		}

		[Fact]
		public void GivenCustomListSeparator_WhenGetTypedArgs_ThenShouldBindCorrectly()
		{
			TypedArgsSettings.Separator = '&';
			string[] args = { "-temperatures", "12&20&22.2&19,2" };

			ArgsHelper<TypedArguments>.SetTypedArgs(args);
			TypedArgsSettings.Separator = '|';

			ArgsHelper<TypedArguments>.Value.Temperatures.Count.ShouldBe(4);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(12);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(20);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(22.2f);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(19.2f);
		}

		[Fact]
		public void WhenGetTypedArgs_ThenValueShouldBePopulated()
		{
			string[] args = { "-temperatures", "12|20|22.2|19,2" };

			ArgsHelper<TypedArguments>.SetTypedArgs(args);

			ArgsHelper<TypedArguments>.Value.Temperatures.Count.ShouldBe(4);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(12);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(20);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(22.2f);
			ArgsHelper<TypedArguments>.Value.Temperatures.ShouldContain(19.2f);
		}

		[Fact]
		public void GivenTypedArgumentsWithDefaultValues_WhenGetTypedArgs_ThenShouldReturnDefaultValues()
		{
			string[] args = {};

			ArgsHelper<TypedArgumentsWithDefaultValues>.SetTypedArgs(args);

			ArgsHelper<TypedArgumentsWithDefaultValues>.Value.ShouldSatisfyAllConditions(
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.Count.ShouldBe(3),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.ShouldContain(10),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.ShouldContain(20),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.ShouldContain(30),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.Count.ShouldBe(4),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(10f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(20f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(30f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(45.5f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.FileCount.ShouldBe(10),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.FilePath.ShouldBe("any path"),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Degrees.ShouldBe(100f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Humans.Count.ShouldBe(2),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Humans.ShouldContain("Josef Ottosson"),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Humans.ShouldContain("Silvia"),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Verbose.ShouldBeTrue()
			);
		}

		[Fact]
		public void GivenTypedArgumentsWithDefaultValues_WhenGetTypedArgsGetsCalledWithSuppliedParameters_ThenShouldReturnSuppliedValues()
		{
			string[] args = { "-filePath", "c:\\temp",
				"--fileCount", "20",
				"-verbose", "false",
				"-degrees", "20",
				"-numberAsString", "22",
				"-humans", "Eminem|Carl|Silvia Drottningsson",
				"-ages", "40|50|60",
				"-temperatures", "100|200|300|400.4"};

			ArgsHelper<TypedArgumentsWithDefaultValues>.SetTypedArgs(args);

			ArgsHelper<TypedArgumentsWithDefaultValues>.Value.ShouldSatisfyAllConditions(
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.Count.ShouldBe(3),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.ShouldContain(40),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.ShouldContain(50),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Ages.ShouldContain(60),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.Count.ShouldBe(4),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(100f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(200f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(300f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Temperatures.ShouldContain(400.4f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.FileCount.ShouldBe(20),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.FilePath.ShouldBe("c:\\temp"),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Degrees.ShouldBe(20f),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Humans.Count.ShouldBe(3),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Humans.ShouldContain("Eminem"),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Humans.ShouldContain("Silvia Drottningsson"),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Humans.ShouldContain("Carl"),
				() => ArgsHelper<TypedArgumentsWithDefaultValues>.Value.Verbose.ShouldBeFalse()
			);
		}
	}
}
