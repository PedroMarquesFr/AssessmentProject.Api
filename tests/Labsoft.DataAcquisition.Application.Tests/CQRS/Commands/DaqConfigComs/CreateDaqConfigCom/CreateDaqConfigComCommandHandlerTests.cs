using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.DaqConfigComs.CreateDaqConfigCom;
using Labsoft.DataAcquisition.Domain.CQRS.Commands;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Shared.Constants;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Commands.DaqConfigComs.CreateDaqConfigCom
{
    [ExcludeFromCodeCoverage]
    public class CreateDaqConfigComCommandHandlerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IDaqConfigComCommandRepository> _repositoryMock;
        private readonly CreateDaqConfigComCommandHandler _handler;

        public CreateDaqConfigComCommandHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IDaqConfigComCommandRepository>();
            _handler = new CreateDaqConfigComCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsResponse()
        {
            // Arrange
            var daqConfigCom = GetDaqConfigComWithEmptyId();
            var request = new CreateDaqConfigComCommandRequest(daqConfigCom);
            var expectedDaqConfigCom = _fixture.Create<DaqConfigCom>();

            _repositoryMock
                .Setup(repo => repo.Create(It.IsAny<DaqConfigCom>()))
                .ReturnsAsync(expectedDaqConfigCom);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.DaqConfigCom.Should().BeEquivalentTo(expectedDaqConfigCom);
        }

        [Fact]
        public void Handle_NullRequest_ThrowsException()
        {
            // Arrange
            var request = new CreateDaqConfigComCommandRequest(null);

            // Act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>()
                .WithMessage(LabsoftDataAcquisitionErrorMessages.RequestDaqConfigComIsNull);
        }

        [Fact]
        public void Handle_NonEmptyId_ThrowsException()
        {
            // Arrange
            var daqConfigCom = GetDaqConfigComWithValidId();
            var request = new CreateDaqConfigComCommandRequest(daqConfigCom);
            
            // Act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>()
                .WithMessage(LabsoftDataAcquisitionErrorMessages.RequestDaqConfigComIdIsNotEmpty);
        }

        [Fact]
        public void Handle_ValidateRequest_NullableProperties_ThrowsException()
        {
            // Arrange
            var daqConfigCom = GetDaqConfigComWithNullableProperties();
            var request = new CreateDaqConfigComCommandRequest(daqConfigCom);

            // Act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<LabsoftDataAcquisitionException>()
                .WithMessage(LabsoftDataAcquisitionErrorMessages.RequestDaqConfigComHasNullableProperties);
        }

        private static DaqConfigCom GetDaqConfigComWithEmptyId() =>
            new(id: Guid.Empty,
                identification: "SampleIdentification",
                active: true,
                daqId: Guid.NewGuid(),
                portName: "COM1",
                baudRate: 9600,
                parity: 0,
                dataBits: 8,
                stopBit: 1,
                dtrEnable: true,
                rtsEnable: true,
                readInterval: 100,
                timeout: 5000,
                stopText: "STOP",
                editionDateTime: DateTime.UtcNow,
                editionUserId: Guid.NewGuid(),
                activationDateTime: DateTime.UtcNow,
                activationUserId: Guid.NewGuid());


        private static DaqConfigCom GetDaqConfigComWithValidId() =>
            new(id: Guid.NewGuid(),
                identification: "SampleIdentification",
                active: true,
                daqId: Guid.NewGuid(),
                portName: "COM1",
                baudRate: 9600,
                parity: 0,
                dataBits: 8,
                stopBit: 1,
                dtrEnable: true,
                rtsEnable: true,
                readInterval: 100,
                timeout: 5000,
                stopText: "STOP",
                editionDateTime: DateTime.UtcNow,
                editionUserId: Guid.NewGuid(),
                activationDateTime: DateTime.UtcNow,
                activationUserId: Guid.NewGuid());

        private static DaqConfigCom GetDaqConfigComWithNullableProperties() =>
            new(id: Guid.Empty,
                identification: null,
                active: null,
                daqId: null,
                portName: null,
                baudRate: null,
                parity: null,
                dataBits: null,
                stopBit: null,
                dtrEnable: null,
                rtsEnable: null,
                readInterval: null,
                timeout: null,
                stopText: null,
                editionDateTime: null,
                editionUserId: null,
                activationDateTime: null,
                activationUserId: null);
    }
}
