using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using SharedLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});
var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<TokenCheckerMiddleware>();
app.UseMiddleware<InterceptionMiddleware>();
app.UseAuthentication();

app.UseOcelot().Wait();
app.Run();
