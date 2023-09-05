using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.DaqConfigComs.GetDaqConfigComById;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Service.Api.Controllers;
using Labsoft.DataAcquisition.Shared.Constants;
using Labsoft.DataAcquisition.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Xunit;
using Labsoft.DataAcquisition.Application.CQRS.Queries.DaqConfigComs.GetDaqConfigComByDaqId;
using Labsoft.DataAcquisition.Application.CQRS.Commands.DaqConfigComs.CreateDaqConfigCom;
using Labsoft.DataAcquisition.Service.Api.Dtos;
using AutoFixture;
using Azure;

namespace Labsoft.DataAcquisition.Service.Api.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class DaqConfigComControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<DaqConfigComController>> _logger;
        private readonly DaqConfigComController _daqConfigComController;
        private readonly Fixture _fixture;

        public DaqConfigComControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<DaqConfigComController>>();
            _daqConfigComController = new DaqConfigComController(_mediator.Object, _logger.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Get_DaqConfigCom_ShouldReturn_Ok()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var mockResponse = new GetDaqConfigComByIdQueryResponse(
                daqConfigCom: new DaqConfigCom(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    daqId: It.IsAny<Guid>(),
                    portName: It.IsAny<string>(),
                    baudRate: It.IsAny<int>(),
                    parity: It.IsAny<int>(),
                    dataBits: It.IsAny<int>(),
                    stopBit: It.IsAny<int>(),
                    dtrEnable: It.IsAny<bool>(),
                    rtsEnable: It.IsAny<bool>(),
                    readInterval: It.IsAny<int>(),
                    timeout: It.IsAny<int>(),
                    stopText: It.IsAny<string>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    editionUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockResponse);

            #endregion

            #region Act
            var response = await _daqConfigComController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Get_DaqConfigCom_ShouldReturn_NotFound()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var mockResponse = new GetDaqConfigComByIdQueryResponse(
                daqConfigCom: null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockResponse);

            #endregion

            #region Act
            var response = await _daqConfigComController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Get_DaqConfigCom_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var id = Guid.Empty;
            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.RequestDaqConfigComIdIsEmpty,
                    errorCode: ELabsoftDataAcquisitionErrorCode.RequestDaqConfigComIdIsEmpty);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqConfigComController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Get_DaqConfigCom_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var id = Guid.Empty;
            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqConfigComController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task GetByDaqId_DaqConfigCom_ShouldReturn_Ok()
        {
            #region Arrange   
            var daqId = It.IsAny<Guid>();
            var mockResponse = new GetDaqConfigComByDaqIdQueryResponse(
                daqConfigCom: new DaqConfigCom(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    daqId: It.IsAny<Guid>(),
                    portName: It.IsAny<string>(),
                    baudRate: It.IsAny<int>(),
                    parity: It.IsAny<int>(),
                    dataBits: It.IsAny<int>(),
                    stopBit: It.IsAny<int>(),
                    dtrEnable: It.IsAny<bool>(),
                    rtsEnable: It.IsAny<bool>(),
                    readInterval: It.IsAny<int>(),
                    timeout: It.IsAny<int>(),
                    stopText: It.IsAny<string>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    editionUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByDaqIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockResponse);

            #endregion

            #region Act
            var response = await _daqConfigComController.GetByDaqId(daqId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task GetByDaqId_DaqConfigCom_ShouldReturn_NotFound()
        {
            #region Arrange   
            var daqId = Guid.Empty;
            var mockResponse = new GetDaqConfigComByDaqIdQueryResponse(
                daqConfigCom: null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByDaqIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockResponse);

            #endregion

            #region Act
            var response = await _daqConfigComController.GetByDaqId(daqId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task GetByDaqId_DaqConfigCom_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var daqId = Guid.Empty;
            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.RequestDaqConfigComDaqIdIsEmpty,
                    errorCode: ELabsoftDataAcquisitionErrorCode.RequestDaqConfigComDaqIdIsEmpty);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByDaqIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqConfigComController.GetByDaqId(daqId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task GetByDaqId_DaqConfigCom_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var daqId = Guid.Empty;
            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetDaqConfigComByDaqIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqConfigComController.GetByDaqId(daqId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Create_ValidDto_ReturnsCreatedWithId()
        {
            #region Arrange  
            var dto = _fixture.Create<DaqConfigComToCreateDto>();
            var daqConfigComId = Guid.NewGuid();
            var response = new CreateDaqConfigComCommandResponse(new DaqConfigCom(
                    id: daqConfigComId,
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    daqId: It.IsAny<Guid>(),
                    portName: It.IsAny<string>(),
                    baudRate: It.IsAny<int>(),
                    parity: It.IsAny<int>(),
                    dataBits: It.IsAny<int>(),
                    stopBit: It.IsAny<int>(),
                    dtrEnable: It.IsAny<bool>(),
                    rtsEnable: It.IsAny<bool>(),
                    readInterval: It.IsAny<int>(),
                    timeout: It.IsAny<int>(),
                    stopText: It.IsAny<string>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    editionUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>()));

            _mediator
                .Setup(m => m.Send(
                    It.IsAny<CreateDaqConfigComCommandRequest>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);
            #endregion

            #region Act
            var result = await _daqConfigComController.Create(dto);
            #endregion

            #region Assert
            result.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.Created);
            #endregion
        }

        [Fact]
        public async Task Create_InvalidDto_ReturnsBadRequest()
        {
            #region Arrange  
            var dto = new DaqConfigComToCreateDto(
                identification: string.Empty,
                    active: It.IsAny<bool>(),
                    daqId: It.IsAny<Guid>(),
                    portName: It.IsAny<string>(),
                    baudRate: It.IsAny<int>(),
                    parity: It.IsAny<int>(),
                    dataBits: It.IsAny<int>(),
                    stopBit: It.IsAny<int>(),
                    dtrEnable: It.IsAny<bool>(),
                    rtsEnable: It.IsAny<bool>(),
                    readInterval: It.IsAny<int>(),
                    timeout: It.IsAny<int>(),
                    stopText: It.IsAny<string>(),
                    userId: It.IsAny<Guid>()); // Create an invalid DTO

            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.RequestDaqConfigComDaqIdIsEmpty,
                    errorCode: ELabsoftDataAcquisitionErrorCode.RequestDaqConfigComDaqIdIsEmpty);
            _mediator.Setup(m => m.Send(
                    It.IsAny<CreateDaqConfigComCommandRequest>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
            #endregion

            #region Act
            var response = await _daqConfigComController.Create(dto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Create_LabsoftDataAcquisitionException_ReturnsBadRequest()
        {
            #region Arrange  
            var dto = _fixture.Create<DaqConfigComToCreateDto>();
            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.RequestDaqConfigComDaqIdIsEmpty,
                    errorCode: ELabsoftDataAcquisitionErrorCode.RequestDaqConfigComDaqIdIsEmpty);
            _mediator.Setup(m => m.Send(
                    It.IsAny<CreateDaqConfigComCommandRequest>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
            #endregion

            #region Act
            var response = await _daqConfigComController.Create(dto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Create_GenericException_ReturnsInternalServerError()
        {
            #region Arrange  
            var dto = _fixture.Create<DaqConfigComToCreateDto>();
            var exceptionMessage = "Some generic exception";
            _mediator
                .Setup(m => m.Send(
                    It.IsAny<CreateDaqConfigComCommandRequest>(), 
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception(exceptionMessage));
            #endregion

            #region Act
            var response = await _daqConfigComController.Create(dto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

    }
}
