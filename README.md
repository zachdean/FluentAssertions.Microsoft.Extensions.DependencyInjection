# FluentAssertions.Microsoft.Extensions.DependencyInjection

This repository contains the [Fluent Assertions](http://fluentassertions.com/) extensions for [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/).

* See [www.fluentassertions.com](http://www.fluentassertions.com/) for more information about the main library.

## Quickstart

Install the NuGet package

```powershell
PM> Install-Package FluentAssertions.Microsoft.Extensions.DependencyInjection
```

and start writing tests for your Microsoft.Extensions.DependencyInjection configuration.
All extensions are borken into three types, Singleton, Scoped, and Transient

```csharp
services.Should().ContianSingleton<ISomeService>();

services.Should().ContianScoped<ISomeService>();

services.Should().ContianTransient<ISomeService>();
```

each type has the same overloads to check different things

```csharp
//check that there is only one singleton, ignoring the implemation
services.Should().ContianSingleton<ISomeService>();

//check if there are multiple services registered of the same type and lifespan, ignoring implemations
services.Should().ContianTransient<ISomeService>(2);

//check that services contains both service and implentation type
//note: this will only pass if there is one registration of TService,
//checking multiple service implemations is not supported 
services.Should().ContianScoped<ISomeService, SomeService>();

//ensure the proper number of services has been registered
services.Should().HaveCount(4);
```
