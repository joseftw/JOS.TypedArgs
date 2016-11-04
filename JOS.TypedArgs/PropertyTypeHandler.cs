using System;
using System.Collections.Generic;
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
			string[] ignoreNamespace = {"Microsoft", "System", "mscorlib", "vshost"};
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => !ignoreNamespace.Any(i => x.FullName.StartsWith(i)))
				.SelectMany(t => t.GetTypes().Where(a => a.IsDefined(typeof(PropertyTypeHandlerAttribute)))
				.Select(z => z.IsGenericType ? z.GetGenericTypeDefinition() : z));
			
			var tmpDict = new Dictionary<string, IPropertyTypeHandler>();
			foreach (var type in types) {
				var attribute = Attribute.GetCustomAttribute(type, typeof(PropertyTypeHandlerAttribute)) as PropertyTypeHandlerAttribute;
				if(attribute == null) {
					throw new NotImplementedException(string.Format("Couldn't find any {0} attribute implemented on {1}", nameof(PropertyTypeHandlerAttribute), type.FullName));
				}
				tmpDict.Add(attribute.PropertyType.FullName, Activator.CreateInstance(type) as IPropertyTypeHandler);
			}

			return tmpDict;
		}
	}
}