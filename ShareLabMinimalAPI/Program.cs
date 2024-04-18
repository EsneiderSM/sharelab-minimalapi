var builder = WebApplication.CreateBuilder(args);
var appName = builder.Configuration.GetValue<string>("AppName");

// Start services area


// End services area


var app = builder.Build();

// Start middlewarte area

app.MapGet("/", () => "Hello World!");
app.MapGet("/appName", () => appName);

// End middleware area

app.Run();
