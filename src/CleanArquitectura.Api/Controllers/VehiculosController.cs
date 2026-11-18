using CleanArquitectura.Application.Vehiculos.GetVehiculosByPagination;
using CleanArquitectura.Application.Vehiculos.SearchVehiculos;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Vehiculos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquitectura.Api.Controllers;

[ApiController]
[Route("api/vehiculos")]
public class VehiculosController : ControllerBase
{

    private readonly ISender _sender;

    public VehiculosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchVehiculos(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new SearchVehiculosQuery(startDate, endDate);
        var resultados = await _sender.Send(query, cancellationToken);
        return Ok(resultados.Value);
    }

    [AllowAnonymous]
    [HttpGet("getPagination")]
    public async Task<ActionResult<PaginationResult<Vehiculo, VehiculoId>>> GetVehiculoPagination(
        [FromQuery] GetVehiculoPaginationQuery request
    )
    {
        var resultados = await _sender.Send(request);
        return Ok(resultados);
    }


}
