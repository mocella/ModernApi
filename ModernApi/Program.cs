using Api.Core.Middleware;
using FluentValidation;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ModernApi.Services;
using ModernApi.Validation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// #### begin-custom wiring for the builder pipeline:

var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
        true)
    .AddEnvironmentVariables()
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(GetMessageDetailsValidator));
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddHttpClient();
// TODO: add custom client mapped for maybe GitHub?

builder.Services
    .AddHealthChecksUI()
    .AddInMemoryStorage()
    .Services
    .AddHealthChecks()
    .AddCheck<ApiHealthCheck>("ModernApi")
    .AddSqlServer(
        builder.Configuration.GetConnectionString("MessageDatabase"))
    // TODO: add EF Context health-check
    //     .AddDbContextCheck<SampleDbContext>();
    .Services
    .AddControllers();

// #### end-custom wiring


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    // swagger enabled for development and staging environments 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// #### begin-custom wiring for the app pipeline:
app.UseRouting()
    .UseMiddleware<ExceptionHandlingMiddleware>()
    .UseEndpoints(config =>
    {
        config.MapHealthChecks("healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        config.MapHealthChecksUI(); // creates route "/healthchecks-ui"
        config.MapDefaultControllerRoute();
    });

// #### end-custom wiring

app.Run();