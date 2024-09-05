using Aurora.BizSuite.Api.Extensions;
using Aurora.BizSuite.Companies.Infrastructure;
using Aurora.BizSuite.Items.Infrastructure;
using Aurora.Framework.Presentation.Endpoints;
using Aurora.Framework.Presentation.Middlewares;
using Aurora.Framework.Presentation.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStringEnumConverter();

builder.Services
    .AddAuthorization()
    .AddAuthentication();

builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// builder.Host.ConfigureSerilogToElasticsearch();

builder.Services.ConfigureOptions<SwaggerGenOptionsSetup>();
builder.Configuration.AddModuleConfiguration(["items"]);

// Add module services
builder.Services
    .AddCompaniesModuleServices(builder.Configuration)
    .AddItemsModuleServices(builder.Configuration);

var app = builder.Build();

RouteGroupBuilder routeGroup = app
    .MapGroup("aurora/bizsuite/")
    .WithOpenApi();
//.RequireAuthorization();

app.MapEndpoints(routeGroup);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.UseExceptionHandler();
//app.UseSerilogRequestLogging();

app.Run();
