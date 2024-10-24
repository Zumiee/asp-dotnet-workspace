using WebApplication1.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connString = "Data Source=GameStore.db";

app.MapGamesEndpoints();

app.Run();
