using Microsoft.AspNetCore.Builder;
using TrainCloud.Microservices.Core.Extensions.Authentication;
using TrainCloud.Microservices.Core.Extensions.Authorization;
using TrainCloud.Microservices.Core.Extensions.Swagger;
using TrainCloud.Uic;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.


webApplicationBuilder.Services.AddTrainCloudAuthorization();
AuthenticationOptions authenticationOptions = webApplicationBuilder.Configuration.GetSection(AuthenticationOptions.Position).Get<AuthenticationOptions>()!;
webApplicationBuilder.Services.AddTrainCloudAuthentication(authenticationOptions);

webApplicationBuilder.Services.AddControllers();

SwaggerOptions swaggerOptions = webApplicationBuilder.Configuration.GetSection(SwaggerOptions.Position).Get<SwaggerOptions>()!;
webApplicationBuilder.Services.AddTrainCloudSwagger(swaggerOptions);

webApplicationBuilder.Services.AddTransient<IUicNumberService, UicNumberService>();

var webApplication = webApplicationBuilder.Build();

// Configure the HTTP request pipeline.

webApplication.UseTrainCloudSwagger();
webApplication.UseAuthorization();

webApplication.MapControllers();

webApplication.Run();

/// <summary>
/// The class definition is required to make this service testable
/// TrainCloud.Tests.Microservices.Identity requires a visible Program class for the WebApplicationFactory
/// </summary>
public partial class Program { }
