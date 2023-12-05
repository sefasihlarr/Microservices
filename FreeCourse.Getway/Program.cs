using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();

//Ýstenilen iþlemle göre yönlendirme istegi

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config
        .AddJsonFile($"configuration.{env.EnvironmentName.ToLower()}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
});


builder.Services.AddAuthentication().AddJwtBearer("GetwayAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_getway";
    options.RequireHttpsMetadata = false;
});


var app = builder.Build();

app.UseRouting();

// Ocelot pipeline'ýný ekleyin
app.UseOcelot().Wait();

app.Run();
