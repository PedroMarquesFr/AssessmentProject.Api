using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Daqs.CreateDaq;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Daqs.UpdateDaq;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Daqs.GetDaqById;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Daqs.GetDaqList;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Service.Api.Controllers;
using Labsoft.DataAcquisition.Shared;
using Labsoft.DataAcquisition.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Xunit;

namespace Labsoft.DataAcquisition.Service.Api.Tests.API
{
    [ExcludeFromCodeCoverage]
    public class DaqControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<DaqController>> _logger;
        private readonly DaqController _daqController;

        public DaqControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<DaqController>>();
            _daqController = new DaqController(_mediator.Object, _logger.Object);
        }

        [Fact]
        public async Task Get_DaqList_ByAccountId_ShouldReturn_Ok()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqListQueryResponse(
                new List<DaqList> {
                    new DaqList(Guid.NewGuid(), "Identification 1", "Equipment 1", "Host 1", "DaqType 1", true, true),
                    new DaqList(Guid.NewGuid(), "Identification 2", "Equipment 2", "Host 2", "DaqType 2", true, true),
                    new DaqList(Guid.NewGuid(), "Identification 3", "Equipment 3", "Host 3", "DaqType 3", true, true),
                    new DaqList(Guid.NewGuid(), "Identification 4", "Equipment 4", "Host 4", "DaqType 4", true, true),
                    new DaqList(Guid.NewGuid(), "Identification 5", "Equipment 5", "Host 5", "DaqType 5", true, true)
                });

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            #endregion

            #region Act
            var response = await _daqController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Get_DaqList_ByAccountId_DaqListEmpty_ShouldReturn_NoContent()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqListQueryResponse(
                new List<DaqList> { });

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            #endregion

            #region Act
            var response = await _daqController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Get_DaqList_ByAccountId_DaqListIsNull_ShouldReturn_NoContent()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqListQueryResponse(null);

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            #endregion

            #region Act
            var response = await _daqController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Get_DaqList_ByAccountId_AccountIdIsZero_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.GetDaqListQueryAccountIdIsZeroError,
                    errorCode: ELabsoftDataAcquisitionErrorCode.GetDaqListQueryAccountIdIsZeroError);

            var accountId = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Get_DaqList_ByAccountId_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var accountId = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _daqController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Get_DaqById_ShouldReturn_Ok()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification 1", Guid.NewGuid(), "Equipment 1", "Host 1", Guid.NewGuid(), "Type 1", false, true));

            var id = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            #endregion

            #region Act
            var response = await _daqController.GetById(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Get_DaqById_DaqIsNull_ShouldReturn_NotFount()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(null);

            var id = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            #endregion

            #region Act
            var response = await _daqController.GetById(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Get_DaqById_DaqIdIsZero_ShouldReturn_BadRequest()
        {
            #region Arrange     
            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.GetDaqQueryIdIsZeroError,
                    errorCode: ELabsoftDataAcquisitionErrorCode.GetDaqQueryIdIsZeroError);

            var id = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqController.GetById(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Get_DaqById_ShouldReturn_InternalServerError()
        {
            #region Arrange     
            var id = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _daqController.GetById(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Create_Daq_ShouldReturn_Ok()
        {
            #region Arrange     
            var mockCreateDaqCommandResponse = new CreateDaqCommandResponse(
                new DaqCreate(
                    id: Guid.NewGuid(),
                    identification: "New Record",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid()));

            var daqPost = new Dtos.DaqCreateDto(
                    id: null,
                    identification: "New Record",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<CreateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockCreateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Post(daqPost);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.Created);
            #endregion
        }

        [Fact]
        public async Task Create_Daq_ShouldReturn_NotFound()
        {
            #region Arrange     
            var mockCreateDaqCommandResponse = new CreateDaqCommandResponse(null);

            var daqPost = new Dtos.DaqCreateDto(
                    id: null,
                    identification: "New Record",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<CreateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockCreateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Post(daqPost);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Create_Daq_DaqIsNull_BadRequest()
        {
            #region Arrange     
            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.UpdateDaqCommandResponseIsNullError,
                errorCode: ELabsoftDataAcquisitionErrorCode.UpdateDaqCommandResponseIsNullError);

            var daqPost = new Dtos.DaqCreateDto(
                    id: null,
                    identification: "New Record",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<CreateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqController.Post(daqPost);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Create_Daq_ShouldRetorn_InternalServerError()
        {
            #region Arrange     
            var daqPost = new Dtos.DaqCreateDto(
                    id: null,
                    identification: "New Record",
                    active: true,
                    accountId: Guid.NewGuid(), 
                    equipmentId: Guid.NewGuid(), 
                    daqTypeId: Guid.NewGuid(), 
                    realTime: false, 
                    host: "10.10.5.10", 
                    userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<CreateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _daqController.Post(daqPost);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Update_Daq_ShouldReturn_NoContent()
        {
            #region Arrange     
            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(
                new DaqCreate(
                    id: Guid.NewGuid(),
                    identification: "Identification",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid()));

            var daqPut = new Dtos.DaqCreateDto(
                    id: Guid.NewGuid(),
                    identification: "New Identification",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Put(daqPut);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Update_Daq_ShouldReturn_NotFound()
        {
            #region Arrange     
            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(null);

            var daqPut = new Dtos.DaqCreateDto(
                    id: Guid.NewGuid(),
                    identification: "New Identification",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Put(daqPut);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Update_Daq_DaqIsNull_BadRequest()
        {
            #region Arrange     
            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.UpdateDaqCommandResponseIsNullError,
                errorCode: ELabsoftDataAcquisitionErrorCode.UpdateDaqCommandResponseIsNullError);


            var daqPut = new Dtos.DaqCreateDto(
                id: Guid.NewGuid(), 
                identification: "New Identification", 
                active: true, 
                accountId: Guid.NewGuid(), 
                equipmentId: Guid.NewGuid(), 
                daqTypeId: Guid.NewGuid(), 
                realTime: false, 
                host: "10.10.5.10", 
                userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqController.Put(daqPut);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Update_Daq_ShouldRetorn_InternalServerError()
        {
            #region Arrange     
            var daqPut = new Dtos.DaqCreateDto(
                id: Guid.NewGuid(), 
                identification: "New Identification", 
                active: true, 
                accountId: Guid.NewGuid(), 
                equipmentId: Guid.NewGuid(), 
                daqTypeId: Guid.NewGuid(), 
                realTime: false, 
                host: "10.10.5.10", 
                userId: Guid.NewGuid());

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _daqController.Put(daqPut);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Active_Daq_ShouldReturn_Create()
        {
            #region Arrange   
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, false));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(
                new DaqCreate(
                    id: Guid.NewGuid(),
                    identification: "Identification",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid()));

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Activate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.Created);
            #endregion
        }

        [Fact]
        public async Task Active_Daq_ShouldReturn_Ok()
        {
            #region Arrange   
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, true));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(
                new DaqCreate(
                    id: Guid.NewGuid(), 
                    identification: "Identification", 
                    active: true, 
                    accountId: Guid.NewGuid(), 
                    equipmentId: Guid.NewGuid(), 
                    daqTypeId: Guid.NewGuid(), 
                    realTime: false, 
                    host: "10.10.5.10", 
                    userId: Guid.NewGuid()));

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Activate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Active_Daq_ShouldReturn_NotFound_1()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(null);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(null);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Activate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Active_Daq_ShouldReturn_NotFound_2()
        {
            #region Arrange   
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, false));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(null);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Activate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Active_Daq_DaqIsNull_BadRequest()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, false));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.UpdateDaqCommandResponseIsNullError,
                errorCode: ELabsoftDataAcquisitionErrorCode.UpdateDaqCommandResponseIsNullError);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqController.Activate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Active_Daq_ShouldRetorn_InternalServerError()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, false));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _daqController.Activate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Deactive_Daq_ShouldReturn_Create()
        {
            #region Arrange   
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, true));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(
                new DaqCreate(
                    id: Guid.NewGuid(),
                    identification: "Identification",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid()));

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Deactivate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.Created);
            #endregion
        }

        [Fact]
        public async Task Deactive_Daq_ShouldReturn_Ok()
        {
            #region Arrange   
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, false));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(
                new DaqCreate(
                    id: Guid.NewGuid(),
                    identification: "Identification",
                    active: true,
                    accountId: Guid.NewGuid(),
                    equipmentId: Guid.NewGuid(),
                    daqTypeId: Guid.NewGuid(),
                    realTime: false,
                    host: "10.10.5.10",
                    userId: Guid.NewGuid()));

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Deactivate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Deactive_Daq_ShouldReturn_NotFound_1()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(null);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(null);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Deactivate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Deactive_Daq_ShouldReturn_NotFound_2()
        {
            #region Arrange   
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, true));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var mockUpdateDaqCommandResponse = new UpdateDaqCommandResponse(null);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateDaqCommandResponse);

            #endregion

            #region Act
            var response = await _daqController.Deactivate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Deactive_Daq_DaqIsNull_BadRequest()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, true));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.UpdateDaqCommandResponseIsNullError,
                errorCode: ELabsoftDataAcquisitionErrorCode.UpdateDaqCommandResponseIsNullError);
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _daqController.Deactivate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Deactive_Daq_ShouldRetorn_InternalServerError()
        {
            #region Arrange     
            var mockGetDaqListQueryResponse = new GetDaqByIdQueryResponse(
                new Daq(Guid.NewGuid(), Guid.NewGuid(), "Identification", Guid.NewGuid(), "Equipment", "Host", Guid.NewGuid(), "Type", false, true));
            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetDaqByIdQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetDaqListQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<UpdateDaqCommandRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _daqController.Deactivate(Guid.NewGuid());
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }



    }
}
