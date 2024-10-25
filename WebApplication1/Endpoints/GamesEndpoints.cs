using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Entities;

namespace WebApplication1.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
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
        new (
            3,
            "Tekken 7",
            "Fighting",
            20.99M,
            new DateOnly(2021, 7, 15)
        ),

    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {
        
        var group = app.MapGroup("games")
                .WithParameterValidation();
        
        // GET /games/.
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) => 
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => {
            
            Game game = new(){
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreID = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
        });


        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
            var index = games.FindIndex(game => game.Id == id); 

            if (index == -1) {
                return Results.NotFound();
            }
            
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
