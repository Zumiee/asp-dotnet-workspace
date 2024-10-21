using WebApplication1.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

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


// GET /games/
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

app.Run();
