using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Daqs.GetDaqList;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Shared;
using Labsoft.DataAcquisition.Shared.Constants;
using MediatR;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.Daqs.GetDaqList
{
    [ExcludeFromCodeCoverage]
    public class GetDaqListQueryHandlerTests
    {
        private readonly Mock<IDaqListQueryRepository> _daqListQueryRepositoryMock;
        private readonly IRequestHandler<GetDaqListQueryRequest, GetDaqListQueryResponse> _handler;
        private readonly Fixture _fixture;

        public GetDaqListQueryHandlerTests()
        {
            _daqListQueryRepositoryMock = new Mock<IDaqListQueryRepository>();
            _handler = new GetDaqListQueryHandler(_daqListQueryRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsExpectedResponse()
        {
            #region arrange
            var accountId = Guid.NewGuid();
            var getDaqListQueryRequest = new GetDaqListQueryRequest(accountId);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: getDaqListQueryRequest,
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
            var getDaqListQueryRequest = new GetDaqListQueryRequest(accountId);
            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.GetDaqListQueryAccountIdIsZeroError,
                errorCode: ELabsoftDataAcquisitionErrorCode.GetDaqListQueryAccountIdIsZeroError);
            #endregion

            #region act
            var result = async () =>
                await _handler.Handle(
                    request: getDaqListQueryRequest,
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
