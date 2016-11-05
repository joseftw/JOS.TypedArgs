using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace JOS.TypedArgs
{
	public static class PropertyTypeHandler {
		private static readonly Dictionary<string, IPropertyTypeHandler> RegisteredPropertyTypeHandlers = GetRegisteredPropertyTypeHandlers();

		public static IPropertyTypeHandler GetPropertyTypeHandler(Type type) {
			var fullName = type.FullName;
			IPropertyTypeHandler propertyTypeHandler;
			RegisteredPropertyTypeHandlers.TryGetValue(fullName, out propertyTypeHandler);
			return propertyTypeHandler;
		} 
		private static Dictionary<string, IPropertyTypeHandler> GetRegisteredPropertyTypeHandlers()
		{
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => !TypedArgsSettings.IgnoreNamespaces.Any(i => x.FullName.StartsWith(i)))
				.SelectMany(t => t.GetTypes().Where(a => a.IsDefined(typeof(PropertyTypeHandlerAttribute)))
					.Select(z => z.IsGenericType ? z.GetGenericTypeDefinition() : z))
				.OrderByDescending(x => !x.FullName.StartsWith("JOS.TypedArgs"));
						
			var tmpDict = new Dictionary<string, IPropertyTypeHandler>();
			foreach (var type in types) {
				var attribute = Attribute.GetCustomAttribute(type, typeof(PropertyTypeHandlerAttribute)) as PropertyTypeHandlerAttribute;
				if(attribute == null) {
					throw new NotImplementedException(
						$"Couldn't find any {nameof(PropertyTypeHandlerAttribute)} attribute implemented on {type.FullName}");
				}

				if (tmpDict.ContainsKey(attribute.PropertyType.FullName))
				{
					Debug.WriteLine(
						$"A PropertyTypeHandler for {attribute.PropertyType.FullName} has already been registered, skipping {type.FullName}");
				}
				else
				{
					tmpDict.Add(attribute.PropertyType.FullName, Activator.CreateInstance(type) as IPropertyTypeHandler);
				}
			}

			return tmpDict;
		}
	}
}