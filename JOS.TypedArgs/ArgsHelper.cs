using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace JOS.TypedArgs
{
	public class ArgsHelper<T>
	{
		public static T Value => _value;
		private static T _value { get; set; }

		public static Result SetTypedArgs(string[] args)
		{
			var groupedArguments = GroupArguments(args);
			var validationResult = ValidateParameters(groupedArguments);
			if (!validationResult.Success)
			{
				if (TypedArgsSettings.ThrowWhenValidationFails)
				{
					var message = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ToString()));
					throw new ValidationException(message);
				}
				return validationResult;
			}
			var typedArguments = GetTypedArguments(groupedArguments);
			_value = typedArguments;
			return new Result();
		}

		private static Result ValidateParameters(IReadOnlyDictionary<string, object> args)
		{
			var requiredProperties =
				typeof(T).GetProperties().Where(x => Attribute.IsDefined(x, typeof(RequiredAttribute)));

			var errors = new List<Error>();
			foreach (var requiredProperty in requiredProperties)
			{
				if (!args.ContainsKey(requiredProperty.Name))
				{
					errors.Add(new Error
					{
						Title = "Missing required parameter",
						Message = $"The parameter {requiredProperty.Name} is required"
					});
				}
			}
			return new Result {Errors = errors};
		}

		private static Dictionary<string, object> GroupArguments(IReadOnlyList<string> args) {
			var arguments = new Dictionary<string, object>();
			for(var i = 0; i < args.Count; i++) {
				var arg = args[i];
				if(!arg.StartsWith("-")) {
					continue;
				}

				var stripped = arg.TrimStart('-');
				var next = (i + 1) < args.Count ? args[i + 1] : null;

				if(next != null && !next.StartsWith("-")) {
					var value = next;
					arguments.Add(stripped, value);
				} else {
					arguments.Add(stripped, null);
				}
			}
			return arguments;
		}

		private static T GetTypedArguments(Dictionary<string, object> groupedArguments) {
			var typedArguments = Activator.CreateInstance<T>();
			foreach (var groupedArgument in groupedArguments) {
				var property = typedArguments.GetType().GetProperty(groupedArgument.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
				if(property == null) {
					continue;
				}

				var defaultValue = property.GetValue(typedArguments);
				var propertyValue = GetTypedProperty(property.PropertyType, groupedArgument.Value);
				property.SetValue(typedArguments, propertyValue ?? defaultValue, null);
			}
			return typedArguments;
		}

		private static object GetTypedProperty(Type propertyType, object propertyValue) {
			var handler = PropertyTypeHandler.GetPropertyTypeHandler(propertyType);

			if(handler == null) {
				throw new NotImplementedException($"Couldn't find any IPropertyTypeHandler for type {propertyType.FullName}");
			}

			var value = handler.GetTypedValue(propertyValue);
			return value;
		}
	}
}
