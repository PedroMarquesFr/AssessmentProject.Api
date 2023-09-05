using AutoFixture;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Equipments.DeactivateEquipment;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.Equipments.DeactivateEquipment
{
    public class DeactivateEquipmentCommandHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IEquipmentCommandRepository> _mockEquipmentCommandRepo;
        private readonly Mock<IEquipmentQueryRepository> _mockEquipmentQueryRepo;
        private readonly DeactivateEquipmentCommandHandler _handler;

        public DeactivateEquipmentCommandHandlerTests()
        {
            _fixture = new Fixture();
            _mockEquipmentCommandRepo = new Mock<IEquipmentCommandRepository>();
            _mockEquipmentQueryRepo = new Mock<IEquipmentQueryRepository>();
            _handler = new DeactivateEquipmentCommandHandler(_mockEquipmentCommandRepo.Object, _mockEquipmentQueryRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_EquipmentDeactivated()
        {
            // Arrange
            var request = _fixture.Create<DeactivateEquipmentCommandRequest>();
            var equipment = _fixture.Create<Equipment>();
            equipment.Active = true;

            _mockEquipmentQueryRepo.Setup(repo => repo.Get(request.Id))
                                   .ReturnsAsync(equipment);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockEquipmentCommandRepo.Verify(repo => repo.Update(It.Is<Equipment>(e => !e.Active)), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequestId_ThrowsException()
        {
            // Arrange
            var request = new DeactivateEquipmentCommandRequest(
                id: Guid.Empty,
                userId: Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<LabsoftDataAcquisitionException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_InvalidUserId_ThrowsException()
        {
            // Arrange
            var request = new DeactivateEquipmentCommandRequest(
                id: Guid.NewGuid(),
                userId: Guid.Empty);

            // Act & Assert
            await Assert.ThrowsAsync<LabsoftDataAcquisitionException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NullableEquipment_ThrowsException()
        {
            // Arrange
            var request = _fixture.Create<DeactivateEquipmentCommandRequest>();

            _mockEquipmentQueryRepo
                .Setup(repo => repo.Get(request.Id))
                .ReturnsAsync((Equipment?)null);

            // Act & Assert
            await Assert.ThrowsAsync<LabsoftDataAcquisitionException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
