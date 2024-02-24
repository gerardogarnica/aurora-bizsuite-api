using Aurora.BizSuite.Settings.Application;
using Aurora.BizSuite.Settings.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddEndpoints();
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

builder.Host.ConfigureSerilogToElasticsearch();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseSerilogRequestLogging();
app.MapEndpoints();

app.Run();
