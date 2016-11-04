using System;
using System.Collections.Generic;
using System.Reflection;

namespace JOS.TypedArgs
{
	public class ArgsHelper<T>
	{
		public static T Value => _value;
		private static T _value { get; set; }
		public static T GetTypedArgs(string[] args) {
			var groupedArguments = GroupArguments(args);
			var typedArguments = GetTypedArguments(groupedArguments);
			_value = typedArguments;
			return typedArguments;
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

				var propertyValue = GetTypedProperty(property.PropertyType, groupedArgument.Value);
				property.SetValue(typedArguments, propertyValue, null);
			}
			return typedArguments;
		}

		private static object GetTypedProperty(Type propertyType, object propertyValue) {
			var handler = PropertyTypeHandler.GetPropertyTypeHandler(propertyType);

			if(handler == null) {
				throw new NotImplementedException(string.Format("Couldn't find any IPropertyTypeHandler for type {0}", propertyType.FullName));
			}

			var value = handler.GetTypedValue(propertyValue);
			return value;
		}
	}
}
