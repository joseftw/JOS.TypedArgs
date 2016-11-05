# JOS.TypedArgs
Binds the provided args string[] in the Main method of a Console Application to a typed class.
###Usage
Create a class with all of your expected paramters as properties like this

```csharp
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
```
You can then use it like this to retrieve the values
```csharp
public class Program
{
	public static void Main(string[] args)
	{
		var typedArgs = ArgsHelper<TypedArguments>.GetTypedArgs(args);
		var humans = typedArgs.Humans;
	}
}
```
OR
```csharp
public class Program
{
	public static void Main(string[] args)
	{
		ArgsHelper<TypedArguments>.SetTypedArgs(args);
		var humans = ArgsHelper<TypedArguments>.Value.Humans;
	}
}
```

###Supported properties out of the box
* `bool`
* `float`
* `int`
* `string`
* `List<int>`
* `List<string>`
* `List<float>`

###Add support for your own types
Create a new class and implement the `IPropertyTypeHandler` interface. You will also need to mark the class with the `PropertyTypeHandler` attribute
```csharp
[PropertyTypeHandler(PropertyType = typeof(bool))]
public class BoolPropertyTypeHandler : IPropertyTypeHandler
{
	public object GetTypedValue(object propertyValue)
	{
		if (propertyValue == null)
		{
			return true;
		}

		bool result;
		bool.TryParse(propertyValue.ToString(), out result);
		return result;
	}
}
```
###Examples

Input
```
myConsoleApplication.exe -humans Josef Ottosson|Carl|Silvia -verbose -filePath c:\\temp
```
Outcome
```csharp
var typedArgs = ArgsHelper<TypedArguments>.GetTypedArgs(args);
var humans = typedArgs.Humans; // List containing Josef Ottosson, Carl and Silvia
var verbose = typedArgs.Verbose; // true
var filePath = typedArgs.FilePath; // c:\\temp
```
###Default values
You can set default values like this
```csharp
public class TypedArguments
{
	public string FilePath { get; set; } = "c:\\temp"
	public int FileCount { get; set; }
	public float Degrees { get; set; }
	public string NumberAsString { get; set; }
	public List<string> Humans { get; set; }
	public List<int> Ages { get; set; }
	public List<float> Temperatures { get; set; }
	public bool Verbose { get; set; } = true
}
```
Input
```
myConsoleApplication.exe -humans Josef Ottosson|Carl|Silvia
```
Outcome
```csharp
var typedArgs = ArgsHelper<TypedArguments>.GetTypedArgs(args);
var verbose = typedArgs.Verbose; // true
var filePath = typedArgs.FilePath; // c:\\temp
```
