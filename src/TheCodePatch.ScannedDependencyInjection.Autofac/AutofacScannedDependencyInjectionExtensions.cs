using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using TheCodePatch.ScannedDependencyInjection;
using TheCodePatch.ScannedDependencyInjection.Annotations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
    public static class AutofacScannedDependencyInjectionExtensions
    {
        public static ContainerBuilder ScanAssemblyForDependencyInjectionAttributes(
            this ContainerBuilder serviceCollection,
            Assembly assembly
        )
        {
            var registrar = new AutofacDependencyAdapter(serviceCollection);
            var annotatedTypes = assembly.FindTypesAndAnnotations();
            annotatedTypes.RegisterTypes(registrar);
            return serviceCollection;
        }

        private class AutofacDependencyAdapter : IFrameworkAdapter
        {
            private readonly ContainerBuilder _services;

            public AutofacDependencyAdapter(
                ContainerBuilder services
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
                var registrationBuilder = _services.RegisterType(implementationType);

                if (null != interfaceType) registrationBuilder = registrationBuilder.As(interfaceType);

                switch (attribute.Lifetime)
                {
                    case Lifetime.Singleton:
                        registrationBuilder.SingleInstance();
                        break;
                    case Lifetime.Scoped:
                        registrationBuilder.InstancePerLifetimeScope();
                        break;

                    // TODO Are we handling transient the correct way?
                }
            }
        }
    }
}