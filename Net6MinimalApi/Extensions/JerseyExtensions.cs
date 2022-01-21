using Microsoft.AspNetCore.Mvc;
using Net6MinimalApi.Models;
using Net6MinimalApi.Services;

namespace Net6MinimalApi.Extensions
{
    public static class JerseyExtensions
    {
        public static void MapJerseyGetEndpoints(this WebApplication app)
        {
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
        }
    }
}
