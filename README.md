# 🚆 TrainCloud.HttpClient

The `TrainCloud.HttpClient` library provides simple extensions for `System.Net.HttpClient` to wrap the de-/serialization and Status/Error handling.
Theese extensions get used in the `TrainCloud(.Tests).Application.*` and `TrainCloud(.Tests).Microservices.*` projects to make the application run like the tests and the test run like the application.

## Status

### GitHub Actions
[![SonarQube](https://github.com/traincloud-net/TrainCloud.HttpClient/actions/workflows/sonarqube.yml/badge.svg)](https://github.com/traincloud-net/TrainCloud.HttpClient/actions/workflows/sonarqube.yml) 
[![NuGet](https://github.com/traincloud-net/TrainCloud.HttpClient/actions/workflows/nuget.yml/badge.svg)](https://github.com/traincloud-net/TrainCloud.HttpClient/actions/workflows/nuget.yml) 

### SonarQube
[![Bugs](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=bugs&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Code Smells](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=code_smells&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Duplicated Lines (%)](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=duplicated_lines_density&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Lines of Code](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=ncloc&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Maintainability Rating](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=sqale_rating&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Reliability Rating](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=reliability_rating&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Security Hotspots](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=security_hotspots&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Security Rating](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=security_rating&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Technical Debt](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=sqale_index&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient) 
[![Vulnerabilities](https://sonarqube.traincloud.net/api/project_badges/measure?project=TrainCloud.HttpClient&metric=vulnerabilities&token=sqb_2a3368bc906f3ad06a68c26889bb616d1fa59a97)](https://sonarqube.traincloud.net/dashboard?id=TrainCloud.HttpClient)

## How to use

Add the TrainCloud NuGet Feed to you `nuget.config` file.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
		<packageSources>
				<add key="TrainCloud" value="https://nuget.pkg.github.com/traincloud-net/index.json" />
		</packageSources>
</configuration>
```

Add `TrainCloud.HttpClient` package to the project

```bash
dotnet add package TrainCloud.HttpClient
```

Add the namespace `TrainCloud.HttpClient` to global usings

**GlobalUsings.cs**
```csharp
using TrainCloud.HttpClient;
```

**_Imports.razor**
```razor
@using TrainCloud.HttpClient
```

### Example
```csharp
HttpClient client = ClientFactory.CreateClient("TrainCloudClient");

ResponseModel? result = await client.PostRequestAsync<PostThingModel, ResponseModel>("/Route", validModel, httpStatus =>
{
    // Http-Status handling
    if (httpStatus != System.Net.HttpStatusCode.OK)
    {
        ToastService.ShowWarning("Error message here");
    }
}, exception =>
{
    // Error
    // ...
});

if (result is not null && ...)
{
    // ...
}
```