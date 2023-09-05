using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType;
using Labsoft.DataAcquisition.Application.CQRS.Commands.EquipmentTypes.DeactivateEquipmentType;
using Labsoft.DataAcquisition.Application.CQRS.Commands.EquipmentTypes.UpdateEquipmentType;
using Labsoft.DataAcquisition.Application.CQRS.Queries.EquipmentsTypes.GetEquipmentType;
using Labsoft.DataAcquisition.Application.CQRS.Queries.EquipmentsTypes.GetEquipmentTypeById;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Service.Api.Controllers;
using Labsoft.DataAcquisition.Service.Api.Dtos;
using Labsoft.DataAcquisition.Shared;
using Labsoft.DataAcquisition.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Xunit;

namespace Labsoft.DataAcquisition.Service.Api.Tests.Controllers
{
    public class EquipmentTypeControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<EquipmentTypeController>> _logger;
        private readonly EquipmentTypeController _equipmentTypeController;

        public EquipmentTypeControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<EquipmentTypeController>>();
            _equipmentTypeController = new EquipmentTypeController(_mediator.Object, _logger.Object);
        }

        [Fact]
        public async Task Get_EquipmentType_ByAccountId_ShouldReturn_Ok()
        {
            #region Arrange     
            var mockGetEquipmentTypeListQueryResponse = new GetEquipmentTypeListQueryResponse(
                new List<EquipmentTypeList> {
                    new EquipmentTypeList(Guid.NewGuid(), "Identification 1", true),
                    new EquipmentTypeList(Guid.NewGuid(), "Identification 2", true),
                    new EquipmentTypeList(Guid.NewGuid(), "Identification 3", true),
                    new EquipmentTypeList(Guid.NewGuid(), "Identification 4", true),
                    new EquipmentTypeList(Guid.NewGuid(), "Identification 5", true)
                });

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentTypeListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeListQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentTypeList_ByAccountId_EquipmentTypeListEmpty_ShouldReturn_NoContent()
        {
            #region Arrange     
            var mockGetEquipmentTypeListQueryResponse = new GetEquipmentTypeListQueryResponse(
                new List<EquipmentTypeList> { });

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentTypeListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeListQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentTypeList_ByAccountId_EquipmentTypeListIsNull_ShouldReturn_NoContent()
        {
            #region Arrange     
            var mockGetEquipmentTypeListQueryResponse = new GetEquipmentTypeListQueryResponse(null);

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentTypeListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeListQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentTypeList_ByAccountId_AccountIdIsZero_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.GetEquipmentTypeListQueryAccountIdIsZeroError,
                    errorCode: ELabsoftDataAcquisitionErrorCode.GetEquipmentTypeListQueryAccountIdIsZeroError);

            var accountId = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentTypeListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentTypeController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentTypeList_ByAccountId_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var accountId = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentTypeListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _equipmentTypeController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentType_ShouldReturn_Ok()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var mockGetEquipmentTypeByIdQueryResponse = new GetEquipmentTypeByIdQueryResponse(
                equipmentType: new EquipmentType(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeByIdQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentType_ShouldReturn_NoContent()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var mockGetEquipmentTypeByIdQueryResponse = new GetEquipmentTypeByIdQueryResponse(
                equipmentType: null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeByIdQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentType_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentType_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Post_EquipmentType_ShouldReturn_Created()
        {
            #region Arrange   
            var equipmentTypeToCreateDto = new EquipmentTypeToCreateDto(
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var mockCreateEquipmentTypeCommandResponse = new CreateEquipmentTypeCommandResponse(
                equipmentType: new EquipmentType(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<CreateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockCreateEquipmentTypeCommandResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Create(equipmentTypeToCreateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.Created);
            #endregion
        }

        [Fact]
        public async Task Post_EquipmentType_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var equipmentTypeToCreateDto = new EquipmentTypeToCreateDto(
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<CreateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Create(equipmentTypeToCreateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Post_EquipmentType_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var equipmentTypeToCreateDto = new EquipmentTypeToCreateDto(
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<CreateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Create(equipmentTypeToCreateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Put_EquipmentType_ShouldReturn_OK()
        {
            #region Arrange   
            var equipmentTypeToUpdateDto = new EquipmentTypeToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var mockUpdateEquipmentTypeCommandResponse = new UpdateEquipmentTypeCommandResponse(
                equipmentType: new EquipmentType(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentTypeCommandResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Update(equipmentTypeToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Put_EquipmentType_ShouldReturn_NoContent()
        {
            #region Arrange   
            var equipmentTypeToUpdateDto = new EquipmentTypeToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var mockUpdateEquipmentTypeCommandResponse = new UpdateEquipmentTypeCommandResponse(null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentTypeCommandResponse);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Update(equipmentTypeToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Put_EquipmentType_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var equipmentTypeToUpdateDto = new EquipmentTypeToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Update(equipmentTypeToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Put_EquipmentType_ShouldReturn_InternalSeverError()
        {
            #region Arrange   
            var equipmentTypeToUpdateDto = new EquipmentTypeToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Update(equipmentTypeToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Delete_DeactivateEquipmentType_ShouldReturn_NoContent()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<DeactivateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()));

            #endregion

            #region Act
            var response = await _equipmentTypeController.Deactivate(id, userId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Delete_DeactivateEquipmentType_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var id = Guid.Empty;
            var userId = Guid.Empty;
            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<DeactivateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Deactivate(id, userId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Delete_DeactivateEquipmentType_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var id = Guid.Empty;
            var userId = Guid.Empty;
            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<DeactivateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentTypeController.Deactivate(id, userId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion

        }

        [Fact]
        public async Task Put_ActivateEquipmentType_ShouldReturn_OK()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var mockGetEquipmentTypeByIdQueryResponse = new GetEquipmentTypeByIdQueryResponse(
                equipmentType: new EquipmentType(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            var mockUpdateEquipmentTypeCommandResponse = new UpdateEquipmentTypeCommandResponse(
                equipmentType: new EquipmentType(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeByIdQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentTypeCommandResponse);

            #endregion

            #region Act
            var responseUpdate = await _equipmentTypeController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipmentType_ShouldReturn_NotFound()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var mockGetEquipmentTypeByIdQueryResponse = new GetEquipmentTypeByIdQueryResponse(
                equipmentType: null);

            var mockUpdateEquipmentTypeCommandResponse = new UpdateEquipmentTypeCommandResponse(
                equipmentType: new EquipmentType(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeByIdQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentTypeCommandResponse);

            #endregion

            #region Act
            var responseUpdate = await _equipmentTypeController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipmentType_ShouldReturn_NoContent()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var mockGetEquipmentTypeByIdQueryResponse = new GetEquipmentTypeByIdQueryResponse(
                equipmentType: new EquipmentType(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            var mockUpdateEquipmentTypeCommandResponse = new UpdateEquipmentTypeCommandResponse(
                equipmentType: null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentTypeByIdQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentTypeCommandResponse);

            #endregion

            #region Act
            var responseUpdate = await _equipmentTypeController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipmentType_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(It.IsAny<UpdateEquipmentTypeCommandResponse>());

            #endregion

            #region Act
            var responseUpdate = await _equipmentTypeController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipmentType_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentTypeByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentTypeCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(It.IsAny<UpdateEquipmentTypeCommandResponse>());

            #endregion

            #region Act
            var responseUpdate = await _equipmentTypeController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }
    }
}
