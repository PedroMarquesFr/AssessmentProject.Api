using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.EquipmentsTypes.GetEquipmentType;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Shared;
using Labsoft.DataAcquisition.Shared.Constants;
using MediatR;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.EquipmentsTypes.GetEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class GetEquipmentTypeLitsQueryHandlerTest
    {
        private readonly Mock<IEquipmentTypeQueryRepository> _equipmentTypeQueryRepositoryMock;
        private readonly IRequestHandler<GetEquipmentTypeListQueryRequest, GetEquipmentTypeListQueryResponse> _handler;
        private readonly Fixture _fixture;

        public GetEquipmentTypeLitsQueryHandlerTest()
        {
            _equipmentTypeQueryRepositoryMock = new Mock<IEquipmentTypeQueryRepository>();
            _handler = new GetEquipmentTypeListQueryHandler(_equipmentTypeQueryRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsExpectedResponse()
        {
            #region arrange
            var accountId = Guid.NewGuid();
            var getEquipmentTypeListQueryRequest = new GetEquipmentTypeListQueryRequest(accountId);
            var equipmentTypeList = _fixture.Create<List<EquipmentTypeList>>();
            _equipmentTypeQueryRepositoryMock
                .Setup(s => s.GetEquipmentTypeByAccountId(accountId))
                .ReturnsAsync(equipmentTypeList);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: getEquipmentTypeListQueryRequest,
                cancellationToken: new CancellationToken());
            #endregion

            #region assert
            result.Should().NotBeNull();
            #endregion
        }

        [Fact]
        public async Task Handle_ReturnException_AccountIdIsZero()
        {
            #region arrange
            var accountId = Guid.Empty;
            var getEquipmentTypeListQueryRequest = new GetEquipmentTypeListQueryRequest(accountId);
            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.GetEquipmentTypeListQueryAccountIdIsZeroError,
                errorCode: ELabsoftDataAcquisitionErrorCode.GetEquipmentTypeListQueryAccountIdIsZeroError);
            #endregion

            #region act
            var result = async () =>
                await _handler.Handle(
                    request: getEquipmentTypeListQueryRequest,
                    cancellationToken: new CancellationToken());
            #endregion

            #region assert
            await result.Should()
                .ThrowAsync<LabsoftDataAcquisitionException>()
                .WithMessage(exception.Message);
            #endregion
        }
    }
}
