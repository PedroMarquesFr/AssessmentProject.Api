using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Equipments.GetEquipmentList;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Entity;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Shared;
using Labsoft.DataAcquisition.Shared.Constants;
using MediatR;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.Equipments.GetEquipmentList
{
    [ExcludeFromCodeCoverage]
    public class GetEquipmentListQueryHandlerTests
    {
        private readonly Mock<IEquipmentQueryRepository> _equipmentListQueryRepositoryMock;
        private readonly IRequestHandler<GetEquipmentListQueryRequest, GetEquipmentListQueryResponse> _handler;
        private readonly Fixture _fixture;

        public GetEquipmentListQueryHandlerTests()
        {
            _equipmentListQueryRepositoryMock = new Mock<IEquipmentQueryRepository>();
            _handler = new GetEquipmentListQueryHandler(_equipmentListQueryRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsExpectedResponse()
        {
            #region arrange
            var accountId = Guid.NewGuid();
            var getEquipmentListQueryRequest = new GetEquipmentListQueryRequest(accountId);
            var equipmentList = _fixture.Create<List<EquipmentList>>();
            _equipmentListQueryRepositoryMock
                .Setup(s => s.GetEquipmentListByAccountId(accountId))
                .ReturnsAsync(equipmentList);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: getEquipmentListQueryRequest,
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
            var getEquipmentListQueryRequest = new GetEquipmentListQueryRequest(accountId);
            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.GetEquipmentListQueryAccountIdIsZeroError,
                errorCode: ELabsoftDataAcquisitionErrorCode.GetEquipmentListQueryAccountIdIsZeroError);
            #endregion

            #region act
            var result = async () =>
                await _handler.Handle(
                    request: getEquipmentListQueryRequest,
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
