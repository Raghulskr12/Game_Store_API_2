using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;


    public static class GameApiEndPoints
{
    private static readonly List<GameDto> games = new List<GameDto>
{
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new GameDto(2, "God of War", "Action-Adventure", 49.99m, new DateOnly(2018, 4, 20)),
    new GameDto(3, "Red Dead Redemption 2", "Action-Adventure", 39.99m, new DateOnly(2018, 10, 26)),
    new GameDto(4, "The Witcher 3: Wild Hunt", "RPG", 29.99m, new DateOnly(2015, 5, 19)),
    new GameDto(5, "Cyberpunk 2077", "RPG", 59.99m, new DateOnly(2020, 12, 10))
};

    public static void MapGameApiEndpoints(this WebApplication app)
    {

app.MapGet("/", () => "GameStoreAPI");

app.MapGet("/games", () => games);


app.MapGet("/games/{id}",(int id) => games.Find((game) => game.Id == id)).WithName("GetGameById");

app.MapPost("/games", (GameDto newGame) =>
{
    GameDto game = new GameDto(
        games.Count + 1,
        newGame.Title,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute("GetGameById", new { id = game.Id }, game);
});



app.MapPut("/games/{id}",(int id, UpdateGameDto updatedGame) =>
{
    var game = games.Find((g) => g.Id == id);
    if (game == null)
    {
        return Results.NotFound();
    }

    games.Remove(game);
    games.Add(new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    ));

    return Results.Ok();
});


app.MapDelete("/games/{id}", (int id) =>
{
    var game = games.Find((g) => g.Id == id);
    if (game == null)
    {
        return Results.NotFound();
    }

    games.Remove(game);
    return Results.Ok();
});
} 
}   
    
