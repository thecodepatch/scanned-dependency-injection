using System;
using System.Collections.Generic;
using TheCodePatch.ScannedDependencyInjection.Annotations;

namespace TheCodePatch.ScannedDependencyInjection
{
    public interface IFrameworkAdapter
    {
        void RegisterAttribute(
            Type implementationType,
            RegisterDependencyAttribute attribute
        );
    }
}