using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FluentAssertions.Microsoft.Extensions.DependencyInjection
{
    /// <inheritdoc />
    /// <summary>
    ///     Contains a number of methods to assert that an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> has registered expected services.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class ServiceCollectionAssertions : ReferenceTypeAssertions<IServiceCollection, ServiceCollectionAssertions>
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="subject"></param>
        public ServiceCollectionAssertions(IServiceCollection subject) : base(subject)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override string Identifier => "services";

        /// <summary>
        /// Asserts that the number of items in the collection matches the supplied <paramref name="expected" /> amount.
        /// </summary>
        /// <param name="expected">The expected number of items in the collection.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> HaveCount(int expected, string because = "", params object[] becauseArgs)
        {
            //had to take the HaveCount function from fluent asserts library since IServiceCollection is not explicitlly 
            //Ienumerable<TService> see: https://github.com/fluentassertions/fluentassertions/blob/develop/Src/FluentAssertions/Collections/NonGenericCollectionAssertions.cs
            if (Subject is null)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to contain {0} item(s){reason}, but found <null>.", expected);
            }

            int actualCount = Subject.Count();

            Execute.Assertion
                .ForCondition(actualCount == expected)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:services} to contain {0} item(s){reason}, but found {1}.", expected, actualCount);

            return new AndConstraint<ServiceCollectionAssertions>(this);
        }

        #region Singleton
        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a singleton
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainSingleton<TService>(string because = "", params object[] becauseArgs)
            => ContainSingleton<TService>(1, because, becauseArgs);


        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a singleton
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <param name="count">
        /// Number of instences in collection
        /// </param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainSingleton<TService>(int count, string because = "", params object[] becauseArgs)
            => CheckServiceCollection<TService>(ServiceLifetime.Singleton, count, because, becauseArgs);

        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a singleton
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <typeparam name="TImplementation">The implentation to check</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainSingleton<TService, TImplementation>(string because = "", params object[] becauseArgs)
            where TImplementation : TService
            => CheckServiceCollection<TService, TImplementation>(ServiceLifetime.Singleton, because, becauseArgs);

        #endregion

        #region Scoped
        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a scoped
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainScoped<TService>(string because = "", params object[] becauseArgs)
            => ContainScoped<TService>(1, because, becauseArgs);

        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a scoped
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <param name="count">
        /// Number of instences in collection
        /// </param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainScoped<TService>(int count, string because = "", params object[] becauseArgs)
            => CheckServiceCollection<TService>(ServiceLifetime.Scoped, count, because, becauseArgs);

        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a scoped
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <typeparam name="TImplementation">The implentation to check</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainScoped<TService, TImplementation>(string because = "", params object[] becauseArgs)
            where TImplementation : TService
            => CheckServiceCollection<TService, TImplementation>(ServiceLifetime.Scoped, because, becauseArgs);

        #endregion

        #region Transient
        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a transient
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainTransient<TService>(string because = "", params object[] becauseArgs)
            => ContainTransient<TService>(1, because, becauseArgs);

        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a transient
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <param name="count">
        /// Number of instences in collection
        /// </param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainTransient<TService>(int count, string because = "", params object[] becauseArgs)
            => CheckServiceCollection<TService>(ServiceLifetime.Transient, count, because, becauseArgs);

        /// <summary>
        /// Asserts that the service collection contains a single serivce of type T and registered as a transient
        /// </summary>
        /// <typeparam name="TService">The service to check</typeparam>
        /// <typeparam name="TImplementation">The implentation to check</typeparam>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<ServiceCollectionAssertions> ContainTransient<TService, TImplementation>(string because = "", params object[] becauseArgs)
            where TImplementation : TService
            => CheckServiceCollection<TService, TImplementation>(ServiceLifetime.Transient, because, becauseArgs);

        #endregion

        #region Helpers
        private AndConstraint<ServiceCollectionAssertions> CheckServiceCollection<TService, TImplenation>(ServiceLifetime lifetime, string because, params object[] becauseArgs)
        {
            CheckServiceCollection<TService>(lifetime, 1, because, becauseArgs);

            //check implentation
            var services = Subject.Where(descriptor => descriptor.ServiceType == typeof(TService));
            var service = services.First();
            if (service.ImplementationType != typeof(TImplenation))
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to have a implementation of type {0} registered, but found {1}.",
                        typeof(TImplenation),
                        service.ImplementationType);
            }

            return new AndConstraint<ServiceCollectionAssertions>(this);
        }

        private AndConstraint<ServiceCollectionAssertions> CheckServiceCollection<TService>(ServiceLifetime lifetime, int count, string because, params object[] becauseArgs)
        {
            NotBeNull();

            var services = Subject.Where(descriptor => descriptor.ServiceType == typeof(TService));

            //check that there is a service
            if (!services.Any())
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to have a service of type {0} registered, but found none.",
                        typeof(TService));
            }

            //check that there is only one service
            if (services.Count() != count)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to have {0} service(s) of type {1} registered, but found {2}.",
                        count,
                        typeof(TService),
                        services.Count());
            }

            if (services.Any(service => service.Lifetime != lifetime))
            {
                var service = services.First(x => x.Lifetime != lifetime);
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected {context:services} to have a {0} of type {1} registered, but found {2}.",
                        lifetime,
                        typeof(TService),
                        service.Lifetime);
            }

            return new AndConstraint<ServiceCollectionAssertions>(this);
        }

        public void NotBeNull()
        {
            if (Subject is null)
            {
                throw new ArgumentNullException("Services", "cannot not assert on null service collection");
            }
        }

        #endregion
    }
}
