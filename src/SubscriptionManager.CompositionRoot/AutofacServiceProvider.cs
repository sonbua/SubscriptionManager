using System;
using Autofac;

namespace SubscriptionManager.CompositionRoot
{
    /// <summary>
    /// Autofac implementation of the ASP.NET Core <see cref="T:System.IServiceProvider" />.
    /// </summary>
    /// <seealso cref="T:System.IServiceProvider" />
    public class AutofacServiceProvider : IServiceProvider
    {
        private readonly IComponentContext _componentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Autofac.Extensions.DependencyInjection.AutofacServiceProvider" /> class.
        /// </summary>
        /// <param name="componentContext">
        /// The component context from which services should be resolved.
        /// </param>
        public AutofacServiceProvider(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        /// <summary>
        /// Gets service of type <paramref name="serviceType" /> from the
        /// <see cref="T:Autofac.Extensions.DependencyInjection.AutofacServiceProvider" /> and requires it be present.
        /// </summary>
        /// <param name="serviceType">
        /// An object that specifies the type of service object to get.
        /// </param>
        /// <returns>
        /// A service object of type <paramref name="serviceType" />.
        /// </returns>
        /// <exception cref="T:Autofac.Core.Registration.ComponentNotRegisteredException">
        /// Thrown if the <paramref name="serviceType" /> isn't registered with the container.
        /// </exception>
        /// <exception cref="T:Autofac.Core.DependencyResolutionException">
        /// Thrown if the object can't be resolved from the container.
        /// </exception>
        public object GetService(Type serviceType) => _componentContext.Resolve(serviceType);
    }
}