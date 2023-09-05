using AutoFixture;
using Labsoft.DataAcquisition.Application.CQRS.Commands.EquipmentTypes.DeactivateEquipmentType;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.EquipmentsTypes.DeactivateEquipmentType
{
    public class DeactivateEquipmentTypeCommandHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IEquipmentTypeCommandRepository> _mockEquipmentTypeCommandRepo;
        private readonly Mock<IEquipmentTypeQueryRepository> _mockEquipmentTypeQueryRepo;
        private readonly DeactivateEquipmentTypeCommandHandler _handler;

        public DeactivateEquipmentTypeCommandHandlerTests()
        {
            _fixture = new Fixture();
            _mockEquipmentTypeCommandRepo = new Mock<IEquipmentTypeCommandRepository>();
            _mockEquipmentTypeQueryRepo = new Mock<IEquipmentTypeQueryRepository>();
            _handler = new DeactivateEquipmentTypeCommandHandler(_mockEquipmentTypeCommandRepo.Object, _mockEquipmentTypeQueryRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_EquipmentTypeDeactivated()
        {
            // Arrange
            var request = _fixture.Create<DeactivateEquipmentTypeCommandRequest>();
            var equipmentType = _fixture.Create<EquipmentType>();
            equipmentType.Active = true;

            _mockEquipmentTypeQueryRepo.Setup(repo => repo.Get(request.Id))
                                   .ReturnsAsync(equipmentType);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockEquipmentTypeCommandRepo.Verify(repo => repo.Update(It.Is<EquipmentType>(e => !e.Active)), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequestId_ThrowsException()
        {
            // Arrange
            var request = new DeactivateEquipmentTypeCommandRequest(
                id: Guid.Empty,
                userId: Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<LabsoftDataAcquisitionException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_InvalidUserId_ThrowsException()
        {
            // Arrange
            var request = new DeactivateEquipmentTypeCommandRequest(
                id: Guid.NewGuid(),
                userId: Guid.Empty);

            // Act & Assert
            await Assert.ThrowsAsync<LabsoftDataAcquisitionException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NullableEquipment_ThrowsException()
        {
            // Arrange
            var request = _fixture.Create<DeactivateEquipmentTypeCommandRequest>();

            _mockEquipmentTypeQueryRepo
                .Setup(repo => repo.Get(request.Id))
                .ReturnsAsync((EquipmentType?)null);

            // Act & Assert
            await Assert.ThrowsAsync<LabsoftDataAcquisitionException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
