using System.Diagnostics.Metrics;
using System.Reflection;
using Api.Core;
using Api.Core.Middleware;
using Api.Core.Services;
using FluentValidation;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using ModernApi.Api.MessageDetails;
using ModernApi.Data;
using ModernApi.Jobs.FileCleanup;
using ModernApi.Services;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Polly;
using Serilog;


var assemblyName = Assembly.GetEntryAssembly()!.GetName();
var appName = assemblyName.Name!;
var appVersion = assemblyName.Version!.ToString();

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

// builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var appResourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: appName, serviceVersion: appVersion);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
#if DEBUG
            .AddConsoleExporter()
#endif
            .AddSource(assemblyName.Name)
            .SetResourceBuilder(appResourceBuilder)
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddSqlClientInstrumentation();
        // would probably want something like the following to actually send telemetry somewhere: 
        /*
         .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri("http://otel-collector:4317");
            }))
         */
    })
    .StartWithHost();

var meter = new Meter(appName);
builder.Services.AddSingleton(meter);

builder.Services.AddOpenTelemetry()
    .WithMetrics(metricProviderBuilder =>
    {
        metricProviderBuilder
#if DEBUG
            .AddConsoleExporter()
#endif
            .AddMeter(meter.Name)
            .SetResourceBuilder(appResourceBuilder)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
        // would probably want something like the following to actually send telemetry somewhere: 
        /*
         .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri("http://otel-collector:4317");
            }))
         */
    })
    .StartWithHost();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(GetMessageDetailsValidator));
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IOperationScoped, OperationScoped>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<OperationHandler>();

builder.Services.AddOptions();

builder.Services.AddTransient<DateTimeProvider>();
builder.Services.Configure<FileCleanupConfig>(configuration.GetSection("FileCleanup"));
builder.Services.AddHostedService<FileCleanupJob>();

builder.Services.AddDbContext<MessageContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MessageDatabase")));

builder.Services.AddHttpClient("PollyMultiple")
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(150 * retryNumber)))
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseDefaultCredentials = true
        });

// TODO: add custom client mapped for maybe GitHub?

builder.Services
    .AddHealthChecksUI()
    .AddInMemoryStorage()
    .Services
    .AddHealthChecks()
    .AddCheck<ApiHealthCheck>("ModernApi")
    .AddSqlServer(
        builder.Configuration.GetConnectionString("MessageDatabase"),
        timeout: TimeSpan.FromSeconds(5))
    .AddDbContextCheck<MessageContext>()
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