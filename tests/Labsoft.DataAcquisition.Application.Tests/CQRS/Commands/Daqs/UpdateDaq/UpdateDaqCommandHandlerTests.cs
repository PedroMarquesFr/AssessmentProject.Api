using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Daqs.UpdateDaq;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.Entity;
using MediatR;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.Daqs.UpdateDaq
{
    [ExcludeFromCodeCoverage]
    public class UpdateDaqCommandHandlerTests
    {
        private readonly Mock<IDaqCommandRepository> _daqCommandRepositoryMock;
        private readonly IRequestHandler<UpdateDaqCommandRequest, UpdateDaqCommandResponse> _handler;
        private readonly Fixture _fixture;

        public UpdateDaqCommandHandlerTests()
        {
            _daqCommandRepositoryMock = new Mock<IDaqCommandRepository>();
            _handler = new UpdateDaqCommandHandler(_daqCommandRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_ValidRequest_Returns_Sucess()
        {
            #region arrange
            DaqCreate daqUpdate = new DaqCreate(
                id: Guid.NewGuid(),
                identification: "Update Record",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentId: Guid.NewGuid(),
                daqTypeId: Guid.NewGuid(),
                realTime: false,
                host: "10.10.5.10",
                userId: Guid.NewGuid());

            var updateDaqCommandRequest = new UpdateDaqCommandRequest(daqUpdate);
            var daqResponse = _fixture.Create<DaqCreate>();
            _daqCommandRepositoryMock
                .Setup(s => s.Create(daqUpdate))
                .ReturnsAsync(daqResponse);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: updateDaqCommandRequest,
                cancellationToken: new CancellationToken());
            #endregion

            #region assert
            result.Should().NotBeNull();
            #endregion
        }

        [Fact]
        public async Task Handle_ShouldReturns_DaqIsNull()
        {
            #region arrange
            DaqCreate daqUpdate = new DaqCreate(
                id: Guid.NewGuid(),
                identification: "Update Record",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentId: Guid.NewGuid(),
                daqTypeId: Guid.NewGuid(),
                realTime: false,
                host: "10.10.5.10",
                userId: Guid.NewGuid());

            var updateDaqCommandRequest = new UpdateDaqCommandRequest(daqUpdate);
            DaqCreate? daqResponse = null;
            _daqCommandRepositoryMock
                .Setup(s => s.Create(daqUpdate))
                .ReturnsAsync(daqResponse);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: updateDaqCommandRequest,
                cancellationToken: new CancellationToken());
            #endregion

            #region assert
            result.Daq.Should().BeNull();
            #endregion
        }
    }
}
