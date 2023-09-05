using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.DaqConfigComs.GetDaqConfigComByDaqId;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.DaqConfigComs.GetDaqConfigComByDaqId
{
    [ExcludeFromCodeCoverage]
    public class GetDaqConfigComByDaqIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsDaqConfigCom()
        {
            // Arrange
            var daqId = Guid.NewGuid();
            var expectedDaqConfigCom = new DaqConfigCom(
                daqId, "Identification", true, daqId, "PortName", 9600, 0, 8, 1, true,
                true, 100, 1000, "StopText", DateTime.Now, Guid.NewGuid(), DateTime.Now, Guid.NewGuid());

            var daqConfigComQueryRepositoryMock = new Mock<IDaqConfigComQueryRepository>();
            daqConfigComQueryRepositoryMock
                .Setup(repo => repo.GetByDaqId(daqId))
                .ReturnsAsync(expectedDaqConfigCom);

            var handler = new GetDaqConfigComByDaqIdQueryHandler(daqConfigComQueryRepositoryMock.Object);

            var request = new GetDaqConfigComByDaqIdQueryRequest(daqId);

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.DaqConfigCom.Should().NotBeNull();
            response.DaqConfigCom.Should().BeEquivalentTo(expectedDaqConfigCom);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ThrowsException()
        {
            // Arrange
            var daqConfigComQueryRepositoryMock = new Mock<IDaqConfigComQueryRepository>();
            var handler = new GetDaqConfigComByDaqIdQueryHandler(daqConfigComQueryRepositoryMock.Object);

            var request = new GetDaqConfigComByDaqIdQueryRequest(Guid.Empty);

            // Act
            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<LabsoftDataAcquisitionException>();
        }
    }
}
