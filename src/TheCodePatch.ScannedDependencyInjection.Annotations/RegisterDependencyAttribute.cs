using System;
using System.Collections.Generic;

namespace TheCodePatch.ScannedDependencyInjection.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public abstract class RegisterDependencyAttribute : Attribute
    {
        public Type ForInterface { get; }

        public Lifetime Lifetime { get; }

        protected RegisterDependencyAttribute(
            Lifetime lifetime,
            Type forInterface = null
        )
        {
            if (null != forInterface && false == forInterface.IsInterface)
                throw new ArgumentException("Argument must be an interface type", nameof(forInterface));

            Lifetime = lifetime;
            ForInterface = forInterface;
        }
    }
}