using CleanArquitectura.Application.Abstractions.Clock;
using CleanArquitectura.Application.Alquileres.ReservarAlquiler;
using CleanArquitectura.Application.UnitTests.Users;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Alquileres;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Domain.Vehiculos;
using FluentAssertions;
using NSubstitute;
using Xunit;


namespace CleanArquitectura.Application.UnitTests.Alquileres;

public class ReservaAlquilerTests
{
    private readonly ReservarAlquilerCommandHandler _handlerMock;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IVehiculoRepository _vehiculoRepositoryMock;
    private readonly IAlquilerRepository _alquilerRepositoryMock;
    private readonly IUnitOfWork _unitOfWork;

    private readonly DateTime UtcNow = DateTime.UtcNow;

    private readonly ReservarAlquilerCommand command = new(
        Guid.NewGuid(),
        Guid.NewGuid(),
        new DateOnly(2024, 1, 1),
        new DateOnly(2025, 1, 1)
        );


    public ReservaAlquilerTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _vehiculoRepositoryMock = Substitute.For<IVehiculoRepository>();
        _alquilerRepositoryMock = Substitute.For<IAlquilerRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        IDateTimeProvider dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        dateTimeProviderMock.currentTime.Returns(UtcNow);

        _handlerMock = new ReservarAlquilerCommandHandler(
            _userRepositoryMock,
            _alquilerRepositoryMock,
             _vehiculoRepositoryMock,
            new PrecioService(),
            _unitOfWork,
            dateTimeProviderMock
        );
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
    {

        // Arrange
        _userRepositoryMock.GetByIdAsync(
            new UserId(command.UserId),
            Arg.Any<CancellationToken>()
        )
        .Returns((User?)null);

        //  Act

        var resultado = await _handlerMock.Handle(command, default);

        // Assert

        resultado.Error.Should().Be(UserErrors.NotFound);
    }


    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenVehiculoIsNull()
    {

        // Arrange
        var user = UserMock.Create();

        //  Act

        _userRepositoryMock.GetByIdAsync(
            new UserId(command.UserId),
            Arg.Any<CancellationToken>()
        ).Returns(user);


        _vehiculoRepositoryMock.GetByIdAsync(
                  new VehiculoId(command.VehiculoId),
                  Arg.Any<CancellationToken>()
              ).Returns((Vehiculo?)null);


        var resultado = await _handlerMock.Handle(command, default);

        // Assert

        resultado.Error.Should().Be(VehiculoErrors.NotFound);
    }

}
