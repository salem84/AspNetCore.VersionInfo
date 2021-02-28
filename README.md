[![NuGet version](https://img.shields.io/nuget/v/AspNetCore.VersionInfo?color=yellowgreen)](http://www.nuget.org/packages/AspNetCore.VersionInfo) ![.NET](https://github.com/salem84/AspNetCore.VersionInfo/workflows/.NET/badge.svg) [![License](https://img.shields.io/badge/License-Apache%202.0-red.svg)](https://github.com/salem84/AspNetCore.VersionInfo/blob/master/LICENSE) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=coverage)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=security_rating)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)

# AspNetCore.VersionInfo

AspNetCore.VersionInfo is a library to expose information about assembly versions used in your web application. 
In particular there are two endpoints, which returns:
* a JSON-formatted data (_/version/json_)
* an HTML user-friendly page (_/version/html_)

Library offers some in-bundle providers to capture versions information, such as the version of entry assembly or the version of the common language runtime. A typical JSON output is: 

```js
{
    "RuntimeInformation.FrameworkDescription":".NET 5.0.3",
    "EntryAssemblyVersion":"1.0.0.0",

    ...
}
```

Moreover it is possible create a specific class to collect additional data as described in [Providers](#providers) section.

## Prerequisites
This library currently targets `net5.0`

## Download

Prerelease packages are on [GH Packages](https://github.com/salem84?tab=packages&repo_name=AspNetCore.VersionInfo)

Release packages are on [Nuget](http://www.nuget.org/packages/AspNetCore.VersionInfo)

## Demo

**URL:** https://aspnetcoreversioninfo-demo.azurewebsites.net

|   |  Endpoint |
| - | - |
| *HTML*   |   [/version/html](https://aspnetcoreversioninfo-demo.azurewebsites.net/version/html)               |
| *JSON*  |   [/version/json](https://aspnetcoreversioninfo-demo.azurewebsites.net/version/json)    |
| *Badge* |   [/version/badge](https://aspnetcoreversioninfo-demo.azurewebsites.net/version/badge/EntryAssemblyVersion?color=Blue&displayName=Version)

## Getting Started

### Startup.cs
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddVersionInfo()
            .With<ClrVersionProvider>()
            .With<AssemblyVersionProvider>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapVersionInfo();
        });
    }
}
```

## Providers
Library is based on following types:
* Providers, that read information and return data in a dictionary
* Collector, that aggregates all data from providers and exposes to endpoints.

A Collector implementation is already included in _AspNetCore.VersionInfo_ package, and usually its default implementation is valid for all scenarios. 

Instead, in order to enrich data gathered from library, multiple custom providers could be developed, implementing custom classes inherited from `IInfoProvider` interface.

To show providers' information, they have to be configured in `ConfigureServices` declaration, using `.With<IInfoProvider>` extension method.

### In-bundle providers
_AspNetCore.VersionInfo_ package includes following providers:

| Provider | Keys | Description |
| - | - | - |
| AssemblyVersionProvider  | `EntryAssembly` | Version of entry assembly |
| ClrVersionProvider  | `RuntimeInformation.FrameworkDescription` <br/> `RuntimeInformation.OsDescription` <br/> `RuntimeInformation.OsArchitecture` <br/> `RuntimeInformation.ProcessArchitecture` <br/> `RuntimeInformation.RuntimeIdentifier` | Version of the common language runtime and .NET installation on which the app is running |
| AppDomainAssembliesVersionProvider  | `<AssemblyName>` | version of assemblies loaded in App Domain |


### Options

WIP - Configure Endpoint


### Badge

Using badge endpoint, 