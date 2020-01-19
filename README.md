# FluentAssertions.Microsoft.Extensions.DependencyInjection

This repository contains the [Fluent Assertions](http://fluentassertions.com/) extensions for [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/).

* See [www.fluentassertions.com](http://www.fluentassertions.com/) for more information about the main library.

## Quickstart

Install the NuGet package

```powershell
PM> Install-Package FluentAssertions.Microsoft.Extensions.DependencyInjection
```

and start writing tests for your Microsoft.Extensions.DependencyInjection configuration.

```csharp
//check that there is singleton
services.Should().ContianSingleton<ISomeService>();

//check that there is transient
services.Should().ContianTransient<ISomeService>();

//check that there is scoped
services.Should().ContianScoped<ISomeService>();

//overloads to check if there are multiple services registered of the same type
services.Should().ContianScoped<ISomeService>(2);

//uensure the proper number of services has been registered
services.Should().HaveCount(4);
```
