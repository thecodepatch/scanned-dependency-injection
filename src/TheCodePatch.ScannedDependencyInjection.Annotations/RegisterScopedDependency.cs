using System;
using System.Collections.Generic;

namespace TheCodePatch.ScannedDependencyInjection.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RegisterScopedDependency : RegisterDependencyAttribute
    {
        /// <inheritdoc />
        public RegisterScopedDependency(
            Type forInterface = null
        )
            : base(Lifetime.Scoped, forInterface)
        {
        }
    }
}