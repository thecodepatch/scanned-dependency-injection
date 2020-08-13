using System;
using System.Collections.Generic;

namespace TheCodePatch.ScannedDependencyInjection.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RegisterTransientDependency : RegisterDependencyAttribute
    {
        /// <inheritdoc />
        public RegisterTransientDependency(
            Type forInterface = null
        )
            : base(Lifetime.Transient, forInterface)
        {
        }
    }
}