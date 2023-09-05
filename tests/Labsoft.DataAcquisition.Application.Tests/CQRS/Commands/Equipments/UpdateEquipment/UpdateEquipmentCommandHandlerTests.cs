using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Equipments.UpdateEquipment;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.Equipments.UpdateEquipment
{
    public class UpdateEquipmentCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WithValidEquipment_ShouldUpdateEquipmentAndReturnResponse()
        {
            // Arrange
            var equipmentId = Guid.NewGuid();

            var equipment = new Equipment(
                id: equipmentId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var mockEquipmentQueryRepository = new Mock<IEquipmentQueryRepository>();
            mockEquipmentQueryRepository
                .Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(equipment);

            var mockEquipmentRepository = new Mock<IEquipmentCommandRepository>();
            mockEquipmentRepository.Setup(repo => repo.Update(It.IsAny<Equipment>()))
                .ReturnsAsync(equipment);

            var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                mockEquipmentRepository.Object,
                mockEquipmentQueryRepository.Object);

            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(equipment);

            // Act
            var response = await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Equipment.Should().NotBeNull();
            response.Equipment!.Id.Should().Be(equipmentId);
        }

        [Fact]
        public void Handle_WithInvalidEquipmentId_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentId = Guid.Empty;

            var equipment = new Equipment(
                id: equipmentId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(equipment);

            // Act
            Func <Task> act = async () =>
            {
                var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                    Mock.Of<IEquipmentCommandRepository>(),
                    Mock.Of<IEquipmentQueryRepository>());
                await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithNullEquipment_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(null);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                    Mock.Of<IEquipmentCommandRepository>(),
                    Mock.Of<IEquipmentQueryRepository>());
                await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithEmptyIdentification_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentId = Guid.NewGuid();

            var equipment = new Equipment(
                id: equipmentId,
                identification: string.Empty,
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                    Mock.Of<IEquipmentCommandRepository>(),
                    Mock.Of<IEquipmentQueryRepository>());
                await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithEmptyAccountId_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentId = Guid.NewGuid();

            var equipment = new Equipment(
                id: equipmentId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.Empty,
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                    Mock.Of<IEquipmentCommandRepository>(),
                    Mock.Of<IEquipmentQueryRepository>());
                await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithEmptyEquipmentTypeId_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentId = Guid.NewGuid();

            var equipment = new Equipment(
                id: equipmentId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.Empty,
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                    Mock.Of<IEquipmentCommandRepository>(),
                    Mock.Of<IEquipmentQueryRepository>());
                await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithEmptyEditionUserId_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentId = Guid.NewGuid();

            var equipment = new Equipment(
                id: equipmentId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.Empty,
                editionDateTime: DateTime.Now,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                    Mock.Of<IEquipmentCommandRepository>(),
                    Mock.Of<IEquipmentQueryRepository>());
                await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_WithNullableEditionDateTime_ShouldThrowLabsoftDataAcquisitionException()
        {
            // Arrange
            var equipmentId = Guid.NewGuid();

            var equipment = new Equipment(
                id: equipmentId,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: null,
                activationUserId: null,
                activationDateTime: null);

            var updateEquipmentCommandRequest = new UpdateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () =>
            {
                var updateEquipmentCommandHandler = new UpdateEquipmentCommandHandler(
                    Mock.Of<IEquipmentCommandRepository>(),
                    Mock.Of<IEquipmentQueryRepository>());
                await updateEquipmentCommandHandler.Handle(updateEquipmentCommandRequest, CancellationToken.None);
            };

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }
    }
}
