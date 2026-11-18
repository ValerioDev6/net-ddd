namespace CleanArquitectura.Application.Vehiculos.SearchVehiculos;

public class SearchVehiculoResponse
{
    public Guid Id { get; init; }
    public string? Modelo { get; init; }

    public string? Vin { get; init; }

    public decimal Precio { get; init; }
    public string? TipoMoneda { get; init; }
    public DireccionResponse? Direccion { get; set; }

}