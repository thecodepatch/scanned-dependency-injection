using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheCodePatch.ScannedDependencyInjection.Annotations;

namespace TheCodePatch.ScannedDependencyInjection
{
    public static class ScannedDependencyInjectionExtensions
    {
        public static IReadOnlyDictionary<Type, List<RegisterDependencyAttribute>> FindTypesAndAnnotations(
            this Assembly assembly
        )
        {
            var result = new Dictionary<Type, List<RegisterDependencyAttribute>>();

            var types = assembly.GetTypes()
                .Where(t => false == t.IsAbstract)
                .ToDictionary(type => type, type => type.GetCustomAttributes<RegisterDependencyAttribute>().ToList())
                .Where(typeAndAttributes => typeAndAttributes.Value.Any());

            foreach (var (type, attributes) in types) result.Add(type, attributes);

            return result;
        }

        public static void RegisterTypes(
            this IReadOnlyDictionary<Type, List<RegisterDependencyAttribute>> types,
            IFrameworkAdapter services
        )
        {
            foreach (var (type, attributes) in types) type.RegisterAttributes(attributes, services);
        }

        private static void RegisterAttributes(
            this Type implementationType,
            IEnumerable<RegisterDependencyAttribute> attributes,
            IFrameworkAdapter services
        )
        {
            foreach (var attribute in attributes) services.RegisterAttribute(implementationType, attribute);
        }
    }
}