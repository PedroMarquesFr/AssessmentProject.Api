using AutoFixture;
using FluentAssertions;
using Labsoft.DataAcquisition.Application.CQRS.Queries.Daqs.GetDaqById;
using Labsoft.DataAcquisition.Domain.CQRS.Queries;
using Labsoft.DataAcquisition.Domain.Exceptions;
using Labsoft.DataAcquisition.Shared;
using Labsoft.DataAcquisition.Shared.Constants;
using MediatR;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Labsoft.DataAcquisition.Application.Tests.CQRS.Queries.Daqs.GetDaqById
{
    [ExcludeFromCodeCoverage]
    public class GetDaqByIdQueryHandlerTests
    {
        private readonly Mock<IDaqListQueryRepository> _daqListQueryRepositoryMock;
        private readonly IRequestHandler<GetDaqByIdQueryRequest, GetDaqByIdQueryResponse> _handler;
        private readonly Fixture _fixture;

        public GetDaqByIdQueryHandlerTests()
        {
            _daqListQueryRepositoryMock = new Mock<IDaqListQueryRepository>();
            _handler = new GetDaqByIdQueryHandler(_daqListQueryRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsExpectedResponse()
        {
            #region arrange
            var id = Guid.NewGuid();
            var getDaqByIdQueryRequest = new GetDaqByIdQueryRequest(id);
            #endregion

            #region act
            var result = await _handler.Handle(
                request: getDaqByIdQueryRequest,
                cancellationToken: new CancellationToken());
            #endregion

            #region assert
            result.Should().NotBeNull();
            #endregion
        }

        [Fact]
        public async Task Handle_ReturnException_IdIsZero()
        {
            #region arrange
            var id = Guid.Empty;
            var getDaqByIdQueryRequest = new GetDaqByIdQueryRequest(id);
            var exception = new LabsoftDataAcquisitionException(
                errorDescription: LabsoftDataAcquisitionErrorMessages.GetDaqQueryIdIsZeroError,
                errorCode: ELabsoftDataAcquisitionErrorCode.GetDaqQueryIdIsZeroError);
            #endregion

            #region act
            var result = async () =>
                await _handler.Handle(
                    request: getDaqByIdQueryRequest,
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
