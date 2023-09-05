using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Equipments.CreateEquipment;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Equipments.DeactivateEquipment;
using Labsoft.DataAcquisition.Application.CQRS.Commands.Equipments.UpdateEquipment;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Equipments.GetEquipmentList;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Equipments.GetEquipmentsById;
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
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Xunit;

namespace Labsoft.DataAcquisition.Service.Api.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class EquipmentControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<EquipmentController>> _logger;
        private readonly EquipmentController _equipmentController;

        public EquipmentControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<EquipmentController>>();
            _equipmentController = new EquipmentController(_mediator.Object, _logger.Object);
        }

        [Fact]
        public async Task Get_EquipmentList_ByAccountId_ShouldReturn_Ok()
        {
            #region Arrange     
            var mockGetEquipmentListQueryResponse = new GetEquipmentListQueryResponse(
                new List<EquipmentList> {
                    new EquipmentList(Guid.NewGuid(), "Identification 1", "DaqType 1", true),
                    new EquipmentList(Guid.NewGuid(), "Identification 2", "DaqType 2", true),
                    new EquipmentList(Guid.NewGuid(), "Identification 3", "DaqType 3", true),
                    new EquipmentList(Guid.NewGuid(), "Identification 4", "DaqType 4", true),
                    new EquipmentList(Guid.NewGuid(), "Identification 5", "DaqType 5", true)
                });

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentListQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentList_ByAccountId_EquipmentListEmpty_ShouldReturn_NoContent()
        {
            #region Arrange     
            var mockGetEquipmentListQueryResponse = new GetEquipmentListQueryResponse(
                new List<EquipmentList> { });

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentListQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentList_ByAccountId_EquipmentListIsNull_ShouldReturn_NoContent()
        {
            #region Arrange     
            var mockGetEquipmentListQueryResponse = new GetEquipmentListQueryResponse(null);

            var accountId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentListQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentList_ByAccountId_AccountIdIsZero_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var exception = new LabsoftDataAcquisitionException(
                    errorDescription: LabsoftDataAcquisitionErrorMessages.GetEquipmentListQueryAccountIdIsZeroError,
                    errorCode: ELabsoftDataAcquisitionErrorCode.GetEquipmentListQueryAccountIdIsZeroError);

            var accountId = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Get_EquipmentList_ByAccountId_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var accountId = Guid.Empty;

            _mediator
                 .Setup(x => x.Send(
                 It.IsAny<GetEquipmentListQueryRequest>(),
                 It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception());

            #endregion

            #region Act
            var response = await _equipmentController.GetByAccountId(accountId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Get_Equipment_ShouldReturn_Ok()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var mockGetEquipmentByIdQueryResponse = new GetEquipmentByIdQueryResponse(
                equipment: new Equipment(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    equipmentTypeId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentByIdQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Get_Equipment_ShouldReturn_NotFound()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var mockGetEquipmentByIdQueryResponse = new GetEquipmentByIdQueryResponse(
                equipment: null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentByIdQueryResponse);

            #endregion

            #region Act
            var response = await _equipmentController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Get_Equipment_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Get_Equipment_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var id = It.IsAny<Guid>();
            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentController.Get(id);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Post_Equipment_ShouldReturn_Created()
        {
            #region Arrange   
            var equipmentToCreateDto = new EquipmentToCreateDto(
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                equipmentTypeId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var mockCreateEquipmentCommandResponse = new CreateEquipmentCommandResponse(
                equipment: new Equipment(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    equipmentTypeId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<CreateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockCreateEquipmentCommandResponse);

            #endregion

            #region Act
            var response = await _equipmentController.Create(equipmentToCreateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.Created);
            #endregion
        }

        [Fact]
        public async Task Post_Equipment_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var equipmentToCreateDto = new EquipmentToCreateDto(
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                equipmentTypeId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<CreateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentController.Create(equipmentToCreateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Post_Equipment_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var equipmentToCreateDto = new EquipmentToCreateDto(
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                equipmentTypeId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<CreateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentController.Create(equipmentToCreateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Put_Equipment_ShouldReturn_OK()
        {
            #region Arrange   
            var equipmentToUpdateDto = new EquipmentToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                equipmentTypeId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var mockUpdateEquipmentCommandResponse = new UpdateEquipmentCommandResponse(
                equipment: new Equipment(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    equipmentTypeId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentCommandResponse);

            #endregion

            #region Act
            var response = await _equipmentController.Update(equipmentToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Put_Equipment_ShouldReturn_NoContent()
        {
            #region Arrange   
            var equipmentToUpdateDto = new EquipmentToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                equipmentTypeId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var mockUpdateEquipmentCommandResponse = new UpdateEquipmentCommandResponse(null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentCommandResponse);

            #endregion

            #region Act
            var response = await _equipmentController.Update(equipmentToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Put_Equipment_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var equipmentToUpdateDto = new EquipmentToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                equipmentTypeId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentController.Update(equipmentToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Put_Equipment_ShouldReturn_InternalSeverError()
        {
            #region Arrange   
            var equipmentToUpdateDto = new EquipmentToUpdateDto(
                id: Guid.NewGuid(),
                identification: It.IsAny<string>(),
                active: true,
                accountId: It.IsAny<Guid>(),
                equipmentTypeId: It.IsAny<Guid>(),
                userId: It.IsAny<Guid>());

            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentController.Update(equipmentToUpdateDto);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }

        [Fact]
        public async Task Delete_DeactivateEquipment_ShouldReturn_NoContent()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<DeactivateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()));

            #endregion

            #region Act
            var response = await _equipmentController.Deactivate(id, userId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Delete_DeactivateEquipment_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var id = Guid.Empty;
            var userId = Guid.Empty;
            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<DeactivateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            #endregion

            #region Act
            var response = await _equipmentController.Deactivate(id, userId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Delete_DeactivateEquipment_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var id = Guid.Empty;
            var userId = Guid.Empty;
            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<DeactivateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            #endregion

            #region Act
            var response = await _equipmentController.Deactivate(id, userId);
            #endregion

            #region Assert
            response.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion

        }

        [Fact]
        public async Task Put_ActivateEquipment_ShouldReturn_OK()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var mockGetEquipmentByIdQueryResponse = new GetEquipmentByIdQueryResponse(
                equipment: new Equipment(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    equipmentTypeId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            var mockUpdateEquipmentCommandResponse = new UpdateEquipmentCommandResponse(
                equipment: new Equipment(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    equipmentTypeId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentByIdQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentCommandResponse);

            #endregion

            #region Act
            var responseUpdate = await _equipmentController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.OK);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipment_ShouldReturn_NotFound()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var mockGetEquipmentByIdQueryResponse = new GetEquipmentByIdQueryResponse(
                equipment: null);

            var mockUpdateEquipmentCommandResponse = new UpdateEquipmentCommandResponse(
                equipment: new Equipment(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    equipmentTypeId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentByIdQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentCommandResponse);

            #endregion

            #region Act
            var responseUpdate = await _equipmentController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipment_ShouldReturn_NoContent()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var mockGetEquipmentByIdQueryResponse = new GetEquipmentByIdQueryResponse(
                equipment: new Equipment(
                    id: It.IsAny<Guid>(),
                    identification: It.IsAny<string>(),
                    active: It.IsAny<bool>(),
                    accountId: It.IsAny<Guid>(),
                    equipmentTypeId: It.IsAny<Guid>(),
                    editionUserId: It.IsAny<Guid>(),
                    editionDateTime: It.IsAny<DateTime>(),
                    activationUserId: It.IsAny<Guid>(),
                    activationDateTime: It.IsAny<DateTime>()));

            var mockUpdateEquipmentCommandResponse = new UpdateEquipmentCommandResponse(
                equipment: null);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockGetEquipmentByIdQueryResponse);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(mockUpdateEquipmentCommandResponse);

            #endregion

            #region Act
            var responseUpdate = await _equipmentController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipment_ShouldReturn_BadRequest()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var labsoftDataAcquisitionException = new LabsoftDataAcquisitionException(
                errorDescription: It.IsAny<string>(),
                errorCode: It.IsAny<ELabsoftDataAcquisitionErrorCode>());

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(labsoftDataAcquisitionException);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(It.IsAny<UpdateEquipmentCommandResponse>());

            #endregion

            #region Act
            var responseUpdate = await _equipmentController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            #endregion
        }

        [Fact]
        public async Task Put_ActivateEquipment_ShouldReturn_InternalServerError()
        {
            #region Arrange   
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var exception = new Exception();

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<GetEquipmentByIdQueryRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ThrowsAsync(exception);

            _mediator
                 .Setup(x => x.Send(
                     It.IsAny<UpdateEquipmentCommandRequest>(),
                     It.IsAny<CancellationToken>()))
                 .ReturnsAsync(It.IsAny<UpdateEquipmentCommandResponse>());

            #endregion

            #region Act
            var responseUpdate = await _equipmentController.Activate(id, userId);
            #endregion

            #region Assert
            responseUpdate.Should().BeOfType<ObjectResult>()
                .Subject.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            #endregion
        }
    }
}
