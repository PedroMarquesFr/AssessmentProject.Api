using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.DaqConfigComs.GetDaqConfigComById;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.DaqConfigComs.GetDaqConfigComByDaqId
{
    [ExcludeFromCodeCoverage]
    public class GetDaqConfigComByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidId_ReturnsDaqConfigCom()
        {
            // Arrange
            var fixture = new Fixture();
            var id = Guid.NewGuid();
            var daqConfigCom = fixture.Create<DaqConfigCom>();

            var queryRepositoryMock = new Mock<IDaqConfigComQueryRepository>();
            queryRepositoryMock.Setup(repo => repo.Get(id)).ReturnsAsync(daqConfigCom);

            var queryHandler = new GetDaqConfigComByIdQueryHandler(queryRepositoryMock.Object);

            var queryRequest = new GetDaqConfigComByIdQueryRequest(id);

            // Act
            var response = await queryHandler.Handle(queryRequest, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.DaqConfigCom.Should().BeEquivalentTo(daqConfigCom);
        }

        [Fact]
        public async Task Handle_EmptyId_ThrowsException()
        {
            // Arrange
            var queryRepositoryMock = new Mock<IDaqConfigComQueryRepository>();

            var queryHandler = new GetDaqConfigComByIdQueryHandler(queryRepositoryMock.Object);

            var queryRequest = new GetDaqConfigComByIdQueryRequest(Guid.Empty);

            // Act & Assert
            await Assert.ThrowsAsync<LabsoftDataAcquisitionException>(
                async () => await queryHandler.Handle(queryRequest, CancellationToken.None));
        }
    }
}
