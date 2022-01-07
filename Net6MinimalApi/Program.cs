using Microsoft.AspNetCore.Mvc;
using Net6MinimalApi.Models;
using Net6MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IJerseyService, JerseyService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

#region Api Operaitons

#region Get

app.MapGet("/jerseys", ([FromServices] IJerseyService jerseyService) => Results.Ok(jerseyService.GetAll()))
    .Produces<List<Jersey>>(StatusCodes.Status200OK)
    .WithName("Get All Jersey").WithTags("Jersey");

app.MapGet("/jerseys/{id}", ([FromServices] IJerseyService jerseyService, int id) =>
{
    var jersey = jerseyService.Get(id);
    return jersey != null ? Results.Ok(jersey) : Results.NotFound();
})
.Produces<Jersey>(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound)
.WithName("Get Spesific Jersey").WithTags("Jersey");

#endregion

#region Post

app.MapPost("/jerseys", ([FromBody] Jersey jersey, [FromServices] IJerseyService jerseyService) =>
{
    var insertedId = jerseyService.Insert(jersey);
    return insertedId > 0 ? Results.Created("jerseys", insertedId) : Results.BadRequest();
})
.Accepts<Jersey>("application/json")
.Produces<int>(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest)
.WithName("Add New Jersey").WithTags("Jersey");

#endregion

#region Put

app.MapPut("/jerseys/{id}", (int id, string playerName, [FromServices] IJerseyService jerseyService) =>
{
    var updatedJersey = jerseyService.Update(id, playerName);
    return updatedJersey != null ? Results.Ok(updatedJersey) : Results.BadRequest();
})
.Produces<Jersey>(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest)
.WithName("Update Jersey Player Name").WithTags("Jersey");

#endregion

#region Delete

app.MapDelete("/jerseys/{id}", (int id, [FromServices] IJerseyService jerseyService) =>
{
    var isDeleted = jerseyService.Delete(id);
    return isDeleted ? Results.Ok() : Results.NotFound();
})
.Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound)
.WithName("Delete Jersey ").WithTags("Jersey");

#endregion

#endregion

app.Run("http://localhost:1000");

//Authorize yada AllowAnonymous attributeler ve async gibi keywordler burada kullanýlabilir. Örnek kullaným;
//app.MapPost("/jerseys", [AllowAnonymous] async ([FromBody] Jersey jersey, [FromServices] IJerseyService jerseyService) => Results.Ok());
//app.MapPost("/jerseys", [Authorize] async ([FromBody] Jersey jersey, [FromServices] IJerseyService jerseyService) => Results.Ok());

//app.MapPost("/jerseys", async ([FromBody] Jersey jersey, [FromServices] IJerseyService jerseyService) => Results.Ok()).RequireAuthorization();
//app.MapPost("/jerseys", async ([FromBody] Jersey jersey, [FromServices] IJerseyService jerseyService) => Results.Ok()).AllowAnonymous();

//app.Run("http://localhost:1000"); // Custom Port 

//app.Urls.Add("http://localhost:1000");
//app.Urls.Add("http://localhost:1001");
//app.Run() Custom Multiple Port
