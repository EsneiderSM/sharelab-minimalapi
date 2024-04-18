using ShareLabMinimalAPI.Entities;

var builder = WebApplication.CreateBuilder(args);
var appName = builder.Configuration.GetValue<string>("AppName");

// Start services area


// End services area


var app = builder.Build();

// Start middlewarte area

app.MapGet("/", () => "Hello World!");
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
});

// End middleware area

app.Run();
