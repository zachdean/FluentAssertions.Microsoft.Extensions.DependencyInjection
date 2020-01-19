using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace FluentAssertions.Microsoft.Extensions.DependencyInjection
{
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    /// <summary>
    /// Colleciton of assertions to check a the registered services
    /// </summary>
    /// <typeparam name="TService">The registered service to check</typeparam>
    public class ServiceAssertions<TService>
    {
        private readonly IServiceCollection _services;
        private readonly IEnumerable<ServiceDescriptor> _filteredServices;
        private int _count;

        internal ServiceAssertions(IServiceCollection services, IEnumerable<ServiceDescriptor> filteredServices, int count)
        {
            _services = services;
            _filteredServices = filteredServices;
            _count = count;
        }

        /// <summary>
        /// Asserts that the service collection has the expected number of services
        /// </summary>
        /// <param name="expected">
        /// the expected number of services
        /// </param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public ServiceAssertions<TService> WithCount(int expected, string because = "", params object[] becauseArgs)
        {
            _count = expected;
            CheckCount(because, becauseArgs);
            return this;
        }

        /// <summary>
        /// Asserts that the service collection has a service registered with a implentation. to check if multiple implentations
        /// are registered, simply chain method
        /// </summary>
        /// <typeparam name="TImplementation">The Implementation to check</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public ServiceAssertions<TService> WithImplementation<TImplementation>(string because = "", params object[] becauseArgs)
            where TImplementation : TService
        {
            if (!_filteredServices.Any(service => service.ImplementationType == typeof(TImplementation)))
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to have a implementation of type {0} registered, but found {1}.",
                        typeof(TImplementation),
                        _services.First(service => service.ImplementationType != typeof(TImplementation)).ImplementationType);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the service collection <see cref="TService"/> are of lifespan Singleton
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> AsSingleton(string because = "", params object[] becauseArgs)
        {
            //check count for one service if count has not been specified
            CheckCount("Should only have one service");
            CheckLifetime(ServiceLifetime.Singleton, because, becauseArgs);
            return new AndConstraint<ServiceCollectionAssertions>(new ServiceCollectionAssertions(_services));
        }

        /// <summary>
        /// Asserts that the service collection <see cref="TService"/> are of lifespan Scoped
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> AsScoped(string because = "", params object[] becauseArgs)
        {
            //check count for one service if count has not been specified
            CheckCount("Should only have one service");
            CheckLifetime(ServiceLifetime.Scoped, because, becauseArgs);
            return new AndConstraint<ServiceCollectionAssertions>(new ServiceCollectionAssertions(_services));
        }

        /// <summary>
        /// Asserts that the service collection <see cref="TService"/> are of lifespan Transient
        /// </summary>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> AsTransient(string because = "", params object[] becauseArgs)
        {
            //check count for one service if count has not been specified
            CheckCount("Should only have one service");
            CheckLifetime(ServiceLifetime.Transient, because, becauseArgs);
            return new AndConstraint<ServiceCollectionAssertions>(new ServiceCollectionAssertions(_services));
        }

        private void CheckLifetime(ServiceLifetime lifetime, string because, params object[] becauseArgs)
        {
            if (_filteredServices.Any(service => service.Lifetime != lifetime))
            {
                var service = _filteredServices.First(x => x.Lifetime != lifetime);
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to have a {0} of type {1} registered, but found {2}.",
                        lifetime,
                        service.ServiceType,
                        service.Lifetime);
            }
        }

        private void CheckCount(string because, params object[] becauseArgs)
        {
            //check service count
            if (_filteredServices.Count() != _count)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to have {0} service(s) of type {1} registered, but found {2}.",
                        _count,
                        typeof(TService),
                        _filteredServices.Count());
            }
        }
    }
}
