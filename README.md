[![NuGet version](https://img.shields.io/nuget/v/AspNetCore.VersionInfo?color=yellowgreen)](http://www.nuget.org/packages/AspNetCore.VersionInfo) ![.NET](https://github.com/salem84/AspNetCore.VersionInfo/workflows/.NET/badge.svg) [![License](https://img.shields.io/badge/License-Apache%202.0-red.svg)](https://github.com/salem84/AspNetCore.VersionInfo/blob/master/LICENSE) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=coverage)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=security_rating)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)

# AspNetCore.VersionInfo

AspNetCore.VersionInfo is a library to expose information about assembly versions used in your web application. 
In particular there are two endpoints, which returns:
* a JSON-formatted data (_/version/json_)
* an HTML user-friendly page (_/version/html_)

For example, library supports following data versions:
* The name of .NET installation on which the app is running
* The version of the common language runtime
* The version of entry assembly

```js
{
    "RuntimeInformation.FrameworkDescription":".NET 5.0.3",
    "DotNetVersion":"5.0.3",
    "EntryAssemblyVersion":"1.0.0.0",

    ...
}
```

## Prerequisites
This library currently targets `net5.0`

## Download

Prerelease packages are on [GH Packages](https://github.com/salem84?tab=packages&repo_name=AspNetCore.VersionInfo)

Release packages are on [Nuget](http://www.nuget.org/packages/AspNetCore.VersionInfo)

## Getting Started

### Startup.cs
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddVersionInfo();
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

