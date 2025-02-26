using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application;
using OrderManagement.Persistence;
using OrderManagement.WebApi.Configurations;
using OrderManagement.WebApi.Filters;
using OrderManagement.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureLogging();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();

builder.Services.AddApplication();
builder.Services.AddValidators();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
