using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.EquipmentsTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class CreateEquipmentTypeCommandHandlerTests
    {
        
        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateEquipmentType()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var equipmentType = new EquipmentType(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),     
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);

            var equipmentTypeCommandRepositoryMock = fixture.Freeze<Mock<IEquipmentTypeCommandRepository>>();
            equipmentTypeCommandRepositoryMock.Setup(repo => repo.Create(It.IsAny<EquipmentType>()))
                                         .ReturnsAsync(equipmentType);

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();
            var request = new CreateEquipmentTypeCommandRequest(equipmentType);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.EquipmentType.Should().BeEquivalentTo(equipmentType);
        }

        [Fact]
        public void Handle_NullRequest_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();
            var request = new CreateEquipmentTypeCommandRequest(null);

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

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();

            var equipmentType = new EquipmentType(
                id: Guid.NewGuid(), // Invalid Id
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentTypeCommandRequest(equipmentType);

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

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();
            var equipmentType = new EquipmentType(
                id: Guid.Empty,
                identification: string.Empty, //invalid identification
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentTypeCommandRequest(equipmentType);

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

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();
            var equipmentType = new EquipmentType(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.Empty, //invalid Account Id
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentTypeCommandRequest(equipmentType);

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

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();
            var equipmentType = new EquipmentType(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: null, // invalid Edition User Id
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentTypeCommandRequest(equipmentType);

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

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();
            var equipmentType = new EquipmentType(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.Empty, // invalid Edition User Id
                editionDateTime: DateTime.UtcNow,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentTypeCommandRequest(equipmentType);

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

            var handler = fixture.Create<CreateEquipmentTypeCommandHandler>();
            var equipmentType = new EquipmentType(
                id: Guid.Empty,
                identification: "Equipment123",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: null,
                activationUserId: null,
                activationDateTime: null);
            var request = new CreateEquipmentTypeCommandRequest(equipmentType);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }
    }
}
