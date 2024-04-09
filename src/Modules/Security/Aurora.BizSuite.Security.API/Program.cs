using Aurora.BizSuite.Security.Application;
using Aurora.BizSuite.Security.Infrastructure;
using Aurora.Framework.Persistence.EFCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthorization()
    .AddAuthentication(builder.Configuration);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddEndpoints();

builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.ConfigureSerilogToElasticsearch();

builder.Services.ConfigureOptions<SwaggerGenOptionsSetup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.MapEndpoints();

app.MigrateDatabase<SecurityContext>();
app.SeedData<SecurityContext>();

app.Run();
