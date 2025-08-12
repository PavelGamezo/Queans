using Queans.Api;
using Queans.Api.Common.GlobalExceptions;
using Queans.Application;
using Queans.Infrastructure;
using Queans.Infrastructure.Persistence;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Adding serilog logger
builder.AddSerilog();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.UseDatabaseSeeding();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.HideClientButton = true;
        options.HideDownloadButton = true;
        
    });
}

app.UseCors();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
