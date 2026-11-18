using CleanArquitectura.Application.Alquileres.GetAlquiler;
using CleanArquitectura.Application.Alquileres.ReservarAlquiler;
using CleanArquitectura.Domain.Permissions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitectura.Api.Controllers.Alquileres;


[ApiController]
[Route("api/alquileres")]
public static class AlquileresController
{
    // private readonly ISender _sender;


    public static IEndpointRouteBuilder MapAlquilerEndpoints(
        this IEndpointRouteBuilder builder
    )
    {

        builder
            .MapGet("api/alquileres/{id}", GetAlquiler)
            .RequireAuthorization(PermissionEnum.ReadUser.ToString())
            .WithName(nameof(GetAlquiler));

        return builder;
    }

    public static async Task<IResult> GetAlquiler(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken
    )
    {
        var query = new GetAlquilerQuery(id);
        var resultado = await sender.Send(query, cancellationToken);
        return resultado.IsSuccess ? Results.Ok(resultado.Value) : Results.NotFound();

    }


    // public AlquileresController(ISender sender)
    // {
    //     _sender = sender;
    // }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetAlquiler(
    //     Guid id,
    //     CancellationToken cancellationToken
    // )
    // {
    //     var query = new GetAlquilerQuery(id);
    //     var resultado = await _sender.Send(query, cancellationToken);

    //     return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
    // }

    // [HttpPost]
    // public async Task<IActionResult> ReservaAlquiler(
    //     AlquilerReservaRequest request,
    //     CancellationToken cancellationToken
    // )
    // {
    //     var command = new ReservarAlquilerCommand
    //     (
    //         request.VehiculoId,
    //         request.UserId,
    //         request.StartDate,
    //         request.EndDate
    //     );

    //     var resultado = await _sender.Send(command, cancellationToken);

    //     if (resultado.IsFailure)
    //     {
    //         return BadRequest(resultado.Error);
    //     }

    //     return CreatedAtAction(nameof(GetAlquiler), new { id = resultado.Value }, resultado.Value);
    // }
}
