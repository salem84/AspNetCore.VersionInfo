[![NuGet version](https://img.shields.io/nuget/v/AspNetCore.VersionInfo?color=yellowgreen)](http://www.nuget.org/packages/AspNetCore.VersionInfo) ![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet) [![License](https://img.shields.io/badge/License-Apache%202.0-red.svg)](https://github.com/salem84/AspNetCore.VersionInfo/blob/master/LICENSE) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=coverage)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=security_rating)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=salem84_AspNetCore.VersionInfo&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=salem84_AspNetCore.VersionInfo)

# AspNetCore.VersionInfo

AspNetCore.VersionInfo is a library to expose information about assembly versions used in your web application. 
In particular there are three endpoints, which returns:
* a JSON-formatted data (_/version/json_)
* an HTML user-friendly page (_/version/html_)
* a nice badge image (_/version/badge_)

Library offers some in-bundle providers to capture versions information, such as the version of entry assembly or the version of the common language runtime. A typical JSON output is: 

```js
{
    "RuntimeInformation.FrameworkDescription":".NET 6.0.8",
    "EntryAssemblyVersion":"2.5.0.0",

    ...
}
```

Moreover it is possible create a specific class to collect additional data as described in [Providers](#providers) section.

## 🧰 Supported Platforms

This library currently targets `net8.0`

## 📦 Download

Prerelease packages are on [GH Packages](https://github.com/salem84?tab=packages&repo_name=AspNetCore.VersionInfo)

Release packages are on [Nuget](http://www.nuget.org/packages/AspNetCore.VersionInfo)

## 🚀 Online Demo

|   | URL |
|---|:-----:|
|![Win](docs/images/win_med.png) **Windows Web App**| https://aspnetcoreversioninfo-demo.azurewebsites.net | 
|![Win](docs/images/win_med.png) **Windows HTML Endpoint** | [/version/html](https://aspnetcoreversioninfo-demo.azurewebsites.net/version/html) | 
|![Win](docs/images/win_med.png) **Windows JSON Endpoint** | [/version/json](https://aspnetcoreversioninfo-demo.azurewebsites.net/version/json) |
|![Win](docs/images/win_med.png) **Windows Badge Endpoint** | [![/version/badge](https://aspnetcoreversioninfo-demo.azurewebsites.net/version/badge/RuntimeInformation.RuntimeIdentifier?color=BrightGreen&label=os)](https://aspnetcoreversioninfo-demo.azurewebsites.net/version/badge/RuntimeInformation.RuntimeIdentifier?color=BrightGreen&label=os) | 
| | |
|![Linux](docs/images/linux_med.png) **Linux Web App**| https://aspnetcoreversioninfo-linux-demo.azurewebsites.net | 
|![Linux](docs/images/linux_med.png) **Linux HTML Endpoint**| [/version/html](https://aspnetcoreversioninfo-linux-demo.azurewebsites.net/version/html) | 
|![Linux](docs/images/linux_med.png) **Linux JSON Endpoint**| [/version/json](https://aspnetcoreversioninfo-linux-demo.azurewebsites.net/version/json) | 
|![Linux](docs/images/linux_med.png) **Linux Badge Endpoint** | [![/version/badge](https://aspnetcoreversioninfo-linux-demo.azurewebsites.net/version/badge/RuntimeInformation.RuntimeIdentifier?color=Red&label=os&icon=simpleicons__debian)](https://aspnetcoreversioninfo-linux-demo.azurewebsites.net/version/badge/RuntimeInformation.RuntimeIdentifier?color=Red&label=os&icon=simpleicons__debian) | 

## ⭐ Getting Started

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

## 🔌 Providers
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
| AssemblyVersionProvider  | `EntryAssembly` <br/> `EntryAssemblyFullName` <br/> `EntryAssemblyLocation` <br/> `EntryAssemblyDirectoryPath` <br/> `EntryAssemblyFileVersion` <br/> `EntryAssemblyClrVersion` <br/> `EntryAssemblyCreationDate` <br/> `EntryAssemblyLastModifiedDate` | Version and main properties of entry assembly |
| ClrVersionProvider  | `RuntimeInformation.FrameworkDescription` <br/> `RuntimeInformation.OsDescription` <br/> `RuntimeInformation.OsArchitecture` <br/> `RuntimeInformation.ProcessArchitecture` <br/> `RuntimeInformation.RuntimeIdentifier` | Version of the common language runtime and .NET installation on which the app is running |
| AppDomainAssembliesVersionProvider  | `<AssemblyName>` | Version of assemblies loaded in App Domain |
| EnvironmentProvider  | `Environment.Uptime` <br/> `Environment.OSVersion` <br/> `Environment.IsOsWindows` <br/> `Environment.Is64BitOperatingSystem` <br/> `Environment.Is64BitProcess` <br/> `Environment.ProcessorCount` <br/> `Environment.MachineName` <br/> `Environment.SystemDirectory` <br/> `Environment.WorkingDirectory` <br/> `Environment.CommandLine` <br/> `Environment.DotNetVersion` | Environment properties |
| EnvironmentVariablesProvider | `<EnvironmentVariableName>`-`<EnvironmentVariableValue>` | Environment variables |

## 🔧 Options

`MapVersionInfo` extension method accepts an optional `VersionInfoOptions` argument to change default URLs:

```csharp

public void Configure(IApplicationBuilder app)
{
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapVersionInfo(o =>
        {
            o.HtmlPath = CUSTOM_HTML_URL;
            o.ApiPath = CUSTOM_JSON_URL;
        });
    });
}

```


## 🖼 Badges

Badge image can be obtained with url

`/version/badge/{versionInfoId}`

where `{versionInfoId}` is a key returned by providers.

Moreover endpoint accepts following parameters in querystring:
* `label`: it's the name to show in the image
* `icon`: the source type and slug for the icon separated by two underscore characters (`__`) such as `simpleicons__linux`. In this version only [Simple Icons](https://simpleicons.org/) type is supported; you can find a list of slugs [here](https://github.com/simple-icons/simple-icons/blob/develop/slugs.md). Icon color is always white.
* `color`: a string as defined in the colors table or a custom colors in hexadecimal, RGB, HSL.

| Color | String | 
| -     | -      |
| ![#4c1](https://via.placeholder.com/15/4c1/000000.png?text=+)| BrightGreen |
| ![#97CA00](https://via.placeholder.com/15/97CA00/000000.png?text=+) | Green |
| ![#dfb317](https://via.placeholder.com/15/dfb317/000000.png?text=+) | Yellow |
| ![#a4a61d](https://via.placeholder.com/15/a4a61d/000000.png?text=+) | YellowGreen |
| ![#fe7d37](https://via.placeholder.com/15/fe7d37/000000.png?text=+) | Orange |
| ![#e05d44](https://via.placeholder.com/15/e05d44/000000.png?text=+) | Red |
| ![#007ec6](https://via.placeholder.com/15/007ec6/000000.png?text=+) | Blue |
| ![#555](https://via.placeholder.com/15/555/000000.png?text=+) | Gray |
| ![#9f9f9f](https://via.placeholder.com/15/9f9f9f/000000.png?text=+) | LightGray |

## 💿 Examples

| Name | Notes | Repository |
| -     | -      | - |
| Basic | Simple .NET 6 WebApplication with built-in endpoints (published in [demo site](https://aspnetcoreversioninfo-demo.azurewebsites.net)) | [💾](./samples/Basic) |
| CustomOptions | WebApplication with custom configuration for endpoint URLs | [💾](./samples/CustomOptions) | 
| Minimal | WebApplication using Minimal API | [💾](./samples/Minimal) | 
| Authentication | WebApplication leverages the ASP.NET Core Authentication/Authorization features to easily restrict access | [💾](./samples/Authentication) | 


## 🔑 Security

### Enable Endpoint only in Development mode
In order to enable _AspNetCore.VersionInfo_ only in the development environment, change `Configure` method

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    
    app.UseEndpoints(endpoints =>
    {
        if (env.IsDevelopment())
        {
            endpoints.MapVersionInfo();
        }
    });
    
    ...
}
```

### Authorization Policy on endpoint

```cs
public void ConfigureServices(IServiceCollection services)
{
    ...
    
    services.AddAuthorization(cfg =>
    {
        cfg.AddPolicy(name: Constants.VERSIONINFO_USER_POLICY, cfgPolicy =>
        {
            cfgPolicy.AddRequirements().RequireAuthenticatedUser();
            cfgPolicy.AddAuthenticationSchemes(Constants.COOKIE_SCHEME);
        });
    });
    
    ...
}


public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapVersionInfo().RequireAuthorization(Constants.VERSIONINFO_USER_POLICY); ;
    });
    
    ...
}
```

For more information, you can inspect [Authentication example](./samples/Authentication). 

## 📄 License

_AspNetCore.VersionInfo_ is [Apache-2.0 licensed](./LICENSE.md)
