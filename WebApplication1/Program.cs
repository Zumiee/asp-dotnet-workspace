using WebApplication1.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (
        1,
        "Street Fighter VI",
        "Fighting",
        20.99M,
        new DateOnly(2020, 7, 15)
    ),
    new (
        2,
        "Valorant",
        "FPS",
        20.99M,
        new DateOnly(2021, 7, 15)
    ),

];


// Get games/
app.MapGet("games", () => games);

// Get games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));


app.Run();
