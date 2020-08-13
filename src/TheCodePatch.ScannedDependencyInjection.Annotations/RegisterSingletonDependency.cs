using System;
using System.Collections.Generic;

namespace TheCodePatch.ScannedDependencyInjection.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RegisterSingletonDependency : RegisterDependencyAttribute
    {
        /// <inheritdoc />
        public RegisterSingletonDependency(
            Type forInterface = null
        )
            : base(Lifetime.Singleton, forInterface)
        {
        }
    }
}