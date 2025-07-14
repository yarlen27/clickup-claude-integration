using ClickUp.Core.Interfaces;
using ClickUp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure ClickUp services
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.Services.AddHttpClient<IClickUpService, ClickUpService>();

// Add health checks
builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "ClickUp Integration API", 
        Version = "v1",
        Description = "REST API for ClickUp integration with Claude Code via MCP"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClickUp Integration API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Add health check endpoint
app.MapHealthChecks("/health");

// Map controllers
app.MapControllers();

app.Run();
