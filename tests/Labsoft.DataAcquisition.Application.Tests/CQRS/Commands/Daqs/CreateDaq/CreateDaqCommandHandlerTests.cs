using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Daqs.CreateDaq;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.Entity;
using MediatR;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.Daqs.CreateDaq
{
    [ExcludeFromCodeCoverage]
    public class CreateDaqCommandHandlerTests
    {
        private readonly Mock<IDaqCommandRepository> _daqCommandRepositoryMock;
        private readonly IRequestHandler<CreateDaqCommandRequest, CreateDaqCommandResponse> _handler;
        private readonly Fixture _fixture;

        public CreateDaqCommandHandlerTests()
        {
            _daqCommandRepositoryMock = new Mock<IDaqCommandRepository>();
            _handler = new CreateDaqCommandHandler(_daqCommandRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_ValidRequest_Returns_Sucess()
        {
            #region arrange
            DaqCreate daqCreate = new DaqCreate(
                id: null,
                identification: "New Record",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentId: Guid.NewGuid(),
                daqTypeId: Guid.NewGuid(),
                realTime: false,
                host: "10.10.5.10",
                userId: Guid.NewGuid());

            var createDaqCommandRequest = new CreateDaqCommandRequest(daqCreate);
            var daqResponse = _fixture.Create<DaqCreate>();
            _daqCommandRepositoryMock
                .Setup(s => s.Create(daqCreate))
                .ReturnsAsync(daqResponse);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: createDaqCommandRequest,
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
            DaqCreate daqCreate = new DaqCreate(
                id: null,
                identification: "New Record",
                active: true,
                accountId: Guid.NewGuid(),
                equipmentId: Guid.NewGuid(),
                daqTypeId: Guid.NewGuid(),
                realTime: false,
                host: "10.10.5.10",
                userId: Guid.NewGuid());

            var createDaqCommandRequest = new CreateDaqCommandRequest(daqCreate);
            DaqCreate? daqResponse = null;
            _daqCommandRepositoryMock
                .Setup(s => s.Create(daqCreate))
                .ReturnsAsync(daqResponse);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: createDaqCommandRequest,
                cancellationToken: new CancellationToken());
            #endregion

            #region assert
            result.Daq.Should().BeNull();
            #endregion
        }

    }
}
