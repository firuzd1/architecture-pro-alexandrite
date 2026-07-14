using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("service-a"))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(opts =>
        {
            opts.Endpoint = new Uri("http://simplest-collector:4317");
        }));

var app = builder.Build();

app.MapGet("/", async (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient();
    var response = await client.GetStringAsync("http://service-b:8080");
    return $"service-a got from service-b: {response}";
});

app.Run();