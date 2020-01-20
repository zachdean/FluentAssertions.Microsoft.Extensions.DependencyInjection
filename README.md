# FluentAssertions.Microsoft.Extensions.DependencyInjection

This repository contains the [Fluent Assertions](http://fluentassertions.com/) extensions for [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/).

* See [www.fluentassertions.com](http://www.fluentassertions.com/) for more information about the main library.

## Quickstart

Install the NuGet package

```powershell
PM> Install-Package FluentAssertions.Microsoft.Extensions.DependencyInjection
```

and start writing tests for your Microsoft.Extensions.DependencyInjection configuration.

Methods

```csharp
.Should()
    .HaveCount(x) //check that the correct number of services has been registered in the IServiceCollection
    .HaveService<TService>(); //check that a service of TService is registered
    
.HaveService<TService>()
    .WithImplentation<SomeService>() //check if service is registered 

```
Example:
```csharp

//general usage example
services.Should()
        .HaveService<ISomeService>()
        //Optional method to check if the collection contians the expected number of registrations
        //for the found service. .As(Lifetime)() will default to checking that there is only one service
        //registered if this method is ommitted
        .WithCount(2)
        //optional method to see if any of the registered services use the implentation, can be chained if 
        //more then one service is registered
        .WithImplentation<SomeService>()
        //checks that the registered service has the correct lifetime (Singleton, Scoped, Transient)
        //Will also ensure that there is only one service registered if the WithCount() operator is ommitted.
        //Can Chain with And() method
        .AsSingleton();

//And chain and HasCount(x) method
services.Should()
        .HaveService<ISomeService>()
        .AsTransient()
        .And()
        //assert that the correct number of services has been registered
        .HaveCount(4);
```
