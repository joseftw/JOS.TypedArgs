# JOS.TypedArgs
Binds the provided args string[] in the Main method of a Console Application to a typed class.
###Installation
`Install-Package JOS.TypedArgs`

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
###Settings
You can set the following settings
```csharp
//List of strings containing the starting section of a namespace you want to ignore when using reflection to find classes implementing the IPropertyTypeHandler
TypedArgsSettings.IgnoreNamespaces = new List<string> {"YourNameHere"}; //Default value: new List<string> {"Microsoft", "System", "mscorlib", "vshost"}

//Separator used when splitting values for List<> properties
TypedArgsSettings.Separator = '&'; //Default value: '|'

//Determs if a ValidationException should be thrown. If false, a Result object with an Error list will be returned.
TypedArgsSettings.ThrowWhenValidationFails = false; //Default value: true
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
[PropertyTypeHandler(PropertyType = typeof(char))]
public class CharPropertyTypeHandler : IPropertyTypeHandler
{
	public object GetTypedValue(object propertyValue)
	{
		return Convert.ToChar(propertyValue);
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
