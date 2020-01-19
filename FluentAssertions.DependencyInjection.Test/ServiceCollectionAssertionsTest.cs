using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Microsoft.Extensions.DependencyInjection.Test
{
    public class ServiceCollectionAssertionsTest
    {
        private readonly IServiceCollection services;

        public ServiceCollectionAssertionsTest()
        {
            services = new ServiceCollection();
            services.AddSingleton<ISingleton, Singleton>();
            services.AddTransient<ITransient, Transient>();
            services.AddScoped<IScoped, Scoped>();
        }

        [Fact]
        public void ServiceCollection_Should_Not_Be_Null()
        {
            IServiceCollection services = null;
            Action act = () => services.Should()
                                        .HaveService<ISingleton>()
                                        .AsSingleton();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ServiceCollection_Should_Not_Contain_Different_Lifetimes()
        {
            services.AddTransient<ISingleton, Singleton>();
            Action act = () => services.Should()
                                     .HaveService<ISingleton>()
                                     .WithCount(2)
                                     .AsSingleton();

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void ServiceCollection_Should_Not_Have_Service()
        {
            Action act = () => services.Should()
                                     .HaveService<IServiceCollection>();

            // Assert
            act.Should().Throw<XunitException>();
        }

        #region Singleton
        [Fact]
        public void ServiceCollection_Should_Contain_Singleton()
        {
            services.Should()
                .HaveService<ISingleton>()
                .WithImplementation<Singleton>()
                .AsSingleton();
        }

        [Fact]
        public void ServiceCollection_Should_Contain_Singleton_With_Implementation()
        {
            services.Should().HaveService<ISingleton>()
                            .WithImplementation<Singleton>()
                            .AsSingleton();
        }

        [Fact]
        public void ServiceCollection_Should_Not_Contain_Singleton_With_Implementation()
        {
            Action act = () => services.Should()
                                     .HaveService<ISingleton>()
                                     .WithImplementation<SingletonOther>();

            // Assert
            act.Should().Throw<XunitException>("ISingleton is not registered as a singleton");
        }

        [Fact]
        public void ServiceCollection_Should_Contain_Two_Singleton()
        {
            services.AddSingleton<ISingleton, Singleton>();
            services.Should().HaveService<ISingleton>()
                            .WithCount(2)
                            .AsSingleton();
        }

        [Fact]
        public void SerivceCollection_Should_Not_Contain_Singleton()
        {
            Action act = () => services.Should()
                                     .HaveService<ITransient>()
                                     .AsSingleton();

            // Assert
            act.Should().Throw<XunitException>("ITransient is not registered as a singleton");
        }

        [Fact]
        public void SerivceCollection_HasManyShouldSingleton_ExpectExceptionBecuaseIsNotOne()
        {
            services.AddSingleton<ISingleton, Singleton>();
            Action act = () => services.Should()
                                        .HaveService<ISingleton>()
                                        .AsSingleton();

            // Assert
            act.Should().Throw<XunitException>();
        }

        #endregion

        #region Scoped

        [Fact]
        public void ServiceCollection_Should_Contain_Two_Scoped()
        {
            services.AddScoped<IScoped, Scoped>();
            services.Should()
                .HaveService<IScoped>()
                .WithCount(2)
                .AsScoped();
        }

        [Fact]
        public void ServiceCollection_Should_Contain_Scoped()
        {
            services.Should()
                .HaveService<IScoped>()
                .AsScoped();
        }

        [Fact]
        public void SerivceCollection_Should_Not_Contain_Scoped()
        {
            Action act = () => services.Should()
                                    .HaveService<ISingleton>()
                                    .AsScoped();

            // Assert
            act.Should().Throw<XunitException>("ISingleton is not registered as a Scoped");
        }

        [Fact]
        public void SerivceCollection_HasManyShouldScoped_ExpectExceptionBecuaseMoreThenOne()
        {
            services.AddScoped<IScoped, Scoped>();
            Action act = () => services.Should()
                                        .HaveService<IScoped>()
                                        .AsScoped();

            // Assert
            act.Should().Throw<XunitException>("Can only have one service registered");
        }

        [Fact]
        public void ServiceCollection_Should_Contain_Scoped_With_Implementation()
        {
            services.Should()
                .HaveService<IScoped>()
                .WithImplementation<Scoped>()
                .AsScoped();
        }

        [Fact]
        public void ServiceCollection_Should_Not_Contain_Scoped_With_Implementation()
        {
            Action act = () => services.Should()
                                    .HaveService<IScoped>()
                                    .WithImplementation<ScopedOther>();

            // Assert
            act.Should().Throw<XunitException>();
        }

        #endregion

        #region Transient

        [Fact]
        public void ServiceCollection_Should_Contain_Two_Transient()
        {
            services.AddTransient<ITransient, Transient>();
            services.Should()
                .HaveService<ITransient>()
                .WithCount(2)
                .AsTransient();
        }

        [Fact]
        public void ServiceCollection_Should_Contain_Transient()
        {
            services.Should()
                .HaveService<ITransient>()
                .AsTransient();
        }

        [Fact]
        public void SerivceCollection_Should_Not_Contain_Transient()
        {
            Action act = () => services.Should()
                                      .HaveService<ISingleton>()
                                      .AsTransient();

            // Assert
            act.Should().Throw<XunitException>("ISingleton is not registered as a transient");
        }

        [Fact]
        public void SerivceCollection_HasManyShouldTransient_ExpectExceptionBecuaseMoreThenOne()
        {
            services.AddTransient<ITransient, Transient>();
            Action act = () => services.Should()
                                      .HaveService<ITransient>()
                                      .AsTransient();

            // Assert
            act.Should().Throw<XunitException>("Can only have one service registered");
        }

        [Fact]
        public void ServiceCollection_Should_Contain_Transient_With_Implementation()
        {
            services.Should()
                .HaveService<ITransient>()
                .WithImplementation<Transient>()
                .AsTransient();
        }

        [Fact]
        public void ServiceCollection_Should_Not_Contain_Transient_With_Implementation()
        {
            Action act = () => services.Should()
                                    .HaveService<ITransient>()
                                    .WithImplementation< TransientOther>()
                                    .AsTransient();

            // Assert
            act.Should().Throw<XunitException>();
        }

        #endregion

        public interface ISingleton { }
        public interface ITransient { }
        public interface IScoped { }

        public class Singleton : ISingleton { }
        public class SingletonOther : ISingleton { }
        public class Transient : ITransient { }
        public class TransientOther : ITransient { }
        public class Scoped : IScoped { }
        public class ScopedOther : IScoped { }
    }
}
