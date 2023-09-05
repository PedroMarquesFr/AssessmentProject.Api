using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Equipments.CreateEquipment;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.Equipments.CreateEquipment
{
    [ExcludeFromCodeCoverage]
    public class CreateEquipmentCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateEquipment()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var equipment = new Equipment(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);

            var equipmentCommandRepositoryMock = fixture.Freeze<Mock<IEquipmentCommandRepository>>();
            equipmentCommandRepositoryMock.Setup(repo => repo.Create(It.IsAny<Equipment>()))
                                         .ReturnsAsync(equipment);

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Equipment.Should().BeEquivalentTo(equipment);
        }

        [Fact]
        public void Handle_NullRequest_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var request = new CreateEquipmentCommandRequest(null);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_IdWithValue_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();

            var equipment = new Equipment(
                id: Guid.NewGuid(), // Invalid Id
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_EmptyIdentification_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var equipment = new Equipment(
                id: Guid.Empty,
                identification: string.Empty, //invalid identification
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_EmptyAccountId_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var equipment = new Equipment(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.Empty, //invalid Account Id
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_EmptyEquipmentTypeId_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var equipment = new Equipment(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.Empty, //invalid Equipment Type
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_NullEditionUserId_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var equipment = new Equipment(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: null, // invalid Edition User Id
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_EmptyEditionUserId_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var equipment = new Equipment(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.Empty, // invalid Edition User Id
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }

        [Fact]
        public void Handle_NullEditionDateTime_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentCommandHandler>();
            var equipment = new Equipment(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: null,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentCommandRequest(equipment);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }
    }
}
