using System;
using WebApplication1.Dtos;

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

    ];

    public static WebApplication MapGamesEndpoints(this WebApplication app) {
        // GET /games/
        app.MapGet("games", () => games);

        // GET /games/1
        app.MapGet("games/{id}", (int id) => 
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

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


        // PUT /games
        app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => {
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

        app.MapDelete("games/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return app;
    }
}
