using System;
using System.Collections.Generic;

namespace TheCodePatch.ScannedDependencyInjection.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterTransientDependencyInheriting : RegisterDependencyAttribute
    {
        /// <inheritdoc />
        public RegisterTransientDependencyInheriting(
            Type forInterface = null
        )
            : base(Lifetime.Transient, forInterface)
        {
        }
    }
}