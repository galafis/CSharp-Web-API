// C# Web API
// Author: Gabriel Demetrios Lafis

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Professional Web API", 
        Version = "v1",
        Description = "Created by Gabriel Demetrios Lafis",
        Contact = new OpenApiContact
        {
            Name = "Gabriel Demetrios Lafis",
            Email = "gabriel@example.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Professional Web API v1");
        c.DocumentTitle = "API by Gabriel Lafis";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add custom middleware
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Author", "Gabriel Demetrios Lafis");
    await next();
});

Console.WriteLine("🚀 C# Web API starting...");
Console.WriteLine("👨‍💻 Created by Gabriel Demetrios Lafis");

app.Run();
