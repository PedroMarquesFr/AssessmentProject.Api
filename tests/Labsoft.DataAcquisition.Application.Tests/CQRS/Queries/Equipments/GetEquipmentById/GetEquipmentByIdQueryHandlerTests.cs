using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Equipments.GetEquipmentsById;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.Equipments.GetEquipmentById
{
    [ExcludeFromCodeCoverage]
    public class GetEquipmentByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnEquipment()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var equipmentId = Guid.NewGuid();
            var equipment = new Equipment(
                id: equipmentId,
                identification: "Equipment456",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentTypeId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: Guid.NewGuid(),
                activationDateTime: DateTime.UtcNow);

            var equipmentQueryRepositoryMock = fixture.Freeze<Mock<IEquipmentQueryRepository>>();
            equipmentQueryRepositoryMock.Setup(repo => repo.Get(equipmentId))
                                       .ReturnsAsync(equipment);

            var handler = fixture.Create<GetEquipmentByIdQueryHandler>();
            var request = new GetEquipmentByIdQueryRequest(equipmentId);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Equipment.Should().BeEquivalentTo(equipment);
        }

        [Fact]
        public void Handle_EmptyId_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<GetEquipmentByIdQueryHandler>();
            var request = new GetEquipmentByIdQueryRequest(Guid.Empty);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }
    }
}
