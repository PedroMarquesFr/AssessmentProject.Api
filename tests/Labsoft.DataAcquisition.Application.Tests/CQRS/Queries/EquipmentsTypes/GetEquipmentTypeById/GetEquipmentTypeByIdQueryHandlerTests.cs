using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.EquipmentsTypes.GetEquipmentTypeById;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.EquipmentsTypes.GetEquipmentTypeById
{
    [ExcludeFromCodeCoverage]
    public class GetEquipmentTypeByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnEquipment()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var equipmentTypeId = Guid.NewGuid();
            var equipmentType = new EquipmentType(
                id: equipmentTypeId,
                identification: "Equipment456",
                active: true,
                accountId: Guid.NewGuid(),
                editionUserId: Guid.NewGuid(),
                editionDateTime: DateTime.UtcNow,
                activationUserId: Guid.NewGuid(),
                activationDateTime: DateTime.UtcNow);

            var equipmentTypeQueryRepositoryMock = fixture.Freeze<Mock<IEquipmentTypeQueryRepository>>();
            equipmentTypeQueryRepositoryMock.Setup(repo => repo.Get(equipmentTypeId))
                                       .ReturnsAsync(equipmentType);

            var handler = fixture.Create<GetEquipmentTypeByIdQueryHandler>();
            var request = new GetEquipmentTypeByIdQueryRequest(equipmentTypeId);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.EquipmentType.Should().BeEquivalentTo(equipmentType);
        }

        [Fact]
        public void Handle_EmptyId_ShouldThrowException()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var handler = fixture.Create<GetEquipmentTypeByIdQueryHandler>();
            var request = new GetEquipmentTypeByIdQueryRequest(Guid.Empty);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }
    }
}
