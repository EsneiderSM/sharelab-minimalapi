using Microsoft.AspNetCore.Cors;
using ShareLabMinimalAPI.Entities;

var builder = WebApplication.CreateBuilder(args);
var appName = builder.Configuration.GetValue<string>("AppName");
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!;

// Start services area

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });

    options.AddPolicy("all", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// End services area

var app = builder.Build();

// Start middlewarte area

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseOutputCache();

app.MapGet("/", [EnableCors(policyName: "all")]() => "Hello World!");
app.MapGet("/appName", () => appName);

app.MapGet("/movies", () =>
{
    var movies = new List<Movie>
    {
        new Movie { id = 1, Name = "The Shawshank Redemption" },
        new Movie { id = 2, Name = "The Godfather" },
        new Movie { id = 3, Name = "The Dark Knight" }
    };
    return movies;
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15)));

// End middleware area

app.Run();
