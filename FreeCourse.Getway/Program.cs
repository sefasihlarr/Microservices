using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddOcelot();


Task<IApplicationBuilder> task = app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
