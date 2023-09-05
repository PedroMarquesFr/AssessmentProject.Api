using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.EquipmentTypes.UpdateEquipmentType;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.EquipmentsTypes.UpdateEquipmentType
{
    public class UpdateEquipmentTypeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidEquipmentType_ShouldUpdateEquipmentAndReturnResponse()
        {
            // Arrange
            var equipmentTypeId = Guid.NewGuid();

            var equipmentType = new EquipmentType(
                id: equipmentTypeId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var mockEquipmentTypeQueryRepository = new Mock<IEquipmentTypeQueryRepository>();
            mockEquipmentTypeQueryRepository
                .Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(equipmentType);

            var mockEquipmentTypeRepository = new Mock<IEquipmentTypeCommandRepository>();
            mockEquipmentTypeRepository.Setup(repo => repo.Update(It.IsAny<EquipmentType>()))
                .ReturnsAsync(equipmentType);

            var updateEquipmentTypeCommandHandler = new UpdateEquipmentTypeCommandHandler(
                mockEquipmentTypeRepository.Object,
                mockEquipmentTypeQueryRepository.Object);

            var updateEquipmentTypeCommandRequest = new UpdateEquipmentTypeCommandRequest(equipmentType);

            // Act
            var response = await updateEquipmentTypeCommandHandler.Handle(updateEquipmentTypeCommandRequest, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.EquipmentType.Should().NotBeNull();
            response.EquipmentType!.Id.Should().Be(equipmentTypeId);
        }

        [Fact]
        public void Handle_WithNullEquipmentType_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var updateEquipmentTypeCommandRequest = new UpdateEquipmentTypeCommandRequest(null);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentTypeCommandHandler = new UpdateEquipmentTypeCommandHandler(
                    Mock.Of<IEquipmentTypeCommandRepository>(),
                    Mock.Of<IEquipmentTypeQueryRepository>());
                await updateEquipmentTypeCommandHandler.Handle(updateEquipmentTypeCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithEmptyIdentification_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentTypeId = Guid.NewGuid();

            var equipmentType = new EquipmentType(
                id: equipmentTypeId,
                identification: string.Empty,
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentTypeCommandRequest = new UpdateEquipmentTypeCommandRequest(equipmentType);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentTypeCommandHandler = new UpdateEquipmentTypeCommandHandler(
                    Mock.Of<IEquipmentTypeCommandRepository>(),
                    Mock.Of<IEquipmentTypeQueryRepository>());
                await updateEquipmentTypeCommandHandler.Handle(updateEquipmentTypeCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithEmptyAccountId_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentTypeId = Guid.NewGuid();

            var equipmentType = new EquipmentType(
                id: equipmentTypeId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.Empty,
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentTypeCommandRequest = new UpdateEquipmentTypeCommandRequest(equipmentType);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentTypeCommandHandler = new UpdateEquipmentTypeCommandHandler(
                    Mock.Of<IEquipmentTypeCommandRepository>(),
                    Mock.Of<IEquipmentTypeQueryRepository>());
                await updateEquipmentTypeCommandHandler.Handle(updateEquipmentTypeCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithEmptyEditionUserId_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentTypeId = Guid.NewGuid();

            var equipmentType = new EquipmentType(
                id: equipmentTypeId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.Empty,
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentTypeCommandRequest = new UpdateEquipmentTypeCommandRequest(equipmentType);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentTypeCommandHandler = new UpdateEquipmentTypeCommandHandler(
                    Mock.Of<IEquipmentTypeCommandRepository>(),
                    Mock.Of<IEquipmentTypeQueryRepository>());
                await updateEquipmentTypeCommandHandler.Handle(updateEquipmentTypeCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithNullableEditionDateTime_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentTypeId = Guid.NewGuid();

            var equipmentType = new EquipmentType(
                id: equipmentTypeId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: null,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentTypeCommandRequest = new UpdateEquipmentTypeCommandRequest(equipmentType);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentTypeCommandHandler = new UpdateEquipmentTypeCommandHandler(
                    Mock.Of<IEquipmentTypeCommandRepository>(),
                    Mock.Of<IEquipmentTypeQueryRepository>());
                await updateEquipmentTypeCommandHandler.Handle(updateEquipmentTypeCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }
    }
}
