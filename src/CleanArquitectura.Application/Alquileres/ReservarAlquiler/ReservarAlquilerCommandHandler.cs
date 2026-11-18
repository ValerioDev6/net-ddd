using CleanArquitectura.Application.Abstractions;
using CleanArquitectura.Application.Abstractions.Clock;
using CleanArquitectura.Application.Exceptions;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Alquileres;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Domain.Vehiculos;

namespace CleanArquitectura.Application.Alquileres.ReservarAlquiler;

internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
{

    private readonly IUserRepository _userRepository;
    private readonly IAlquilerRepository _alquilerRepository;
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly PrecioService _precioService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReservarAlquilerCommandHandler(
        IUserRepository userRepository,
        IAlquilerRepository alquilerRepository,
        IVehiculoRepository vehiculoRepository,
        PrecioService precioService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _alquilerRepository = alquilerRepository;
        _vehiculoRepository = vehiculoRepository;
        _precioService = precioService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReservarAlquilerCommand request, CancellationToken cancellationToken)
    {

        var userId = new UserId(request.UserId);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehiculoId = new VehiculoId(request.VehiculoId);
        var vehiculo = await _vehiculoRepository.GetByIdAsync(vehiculoId, cancellationToken);
        if (vehiculo is null)
        {
            return Result.Failure<Guid>(VehiculoErrors.NotFound);
        }

        var duracion = DateRange.Create(request.FechaInicio, request.FechaFin);

        if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }

        try
        {
            var alquiler = Alquiler.Reservar(
                vehiculo,
                user.Id!,
                duracion,
                DateTime.UtcNow,
                _precioService
            );


            _alquilerRepository.Add(alquiler);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return alquiler.Id.Value;
        }
        catch (ConcurrencyExceptions)
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }
    }
}
