using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using TheCodePatch.ScannedDependencyInjection.Annotations;

// ReSharper disable once CheckNamespace
namespace TheCodePatch.ScannedDependencyInjection.NetCore
{
    public static class NetCoreScannedDependencyInjectionExtensions
    {
        public static IServiceCollection ScanAssemblyForDependencyInjectionAttributes(
            this IServiceCollection serviceCollection,
            Assembly assembly
        )
        {
            var registrar = new MicrosoftDependencyAdapter(serviceCollection);
            var annotatedTypes = assembly.FindTypesAndAnnotations();
            annotatedTypes.RegisterTypes(registrar);
            return serviceCollection;
        }

        private class MicrosoftDependencyAdapter : IFrameworkAdapter
        {
            private readonly IServiceCollection _services;

            public MicrosoftDependencyAdapter(
                IServiceCollection services
            )
            {
                _services = services;
            }

            /// <inheritdoc />
            public void RegisterAttribute(
                Type implementationType,
                RegisterDependencyAttribute attribute
            )
            {
                var interfaceType = attribute.ForInterface ?? implementationType;
                var serviceDescriptor =
                    ServiceDescriptor.Describe(interfaceType, implementationType, MapLifetime(attribute.Lifetime));
                _services.Add(serviceDescriptor);
            }

            private static ServiceLifetime MapLifetime(
                Lifetime lifetime
            )
            {
                return lifetime switch
                {
                    Lifetime.Singleton => ServiceLifetime.Singleton,
                    Lifetime.Scoped => ServiceLifetime.Scoped,
                    Lifetime.Transient => ServiceLifetime.Transient,
                    _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Lifetime not supported.")
                };
            }
        }
    }
}