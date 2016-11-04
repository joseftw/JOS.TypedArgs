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
		private static Dictionary<string, IPropertyTypeHandler> GetRegisteredPropertyTypeHandlers() {
			var types = Assembly.GetExecutingAssembly().GetTypes().Where( x => x.IsDefined(typeof(PropertyTypeHandlerAttribute))).Select(t => t.IsGenericType ? t.GetGenericTypeDefinition() : t);
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