using AssessmentProject.Shared;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AssessmentProject.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class AssessmentProjectException : Exception
    {
        public AssessmentProjectException(
            string errorDescription,
            EAssessmentProjectErrorCode errorCode) :
                base(message: $"AssessmentProjectErrorCode: {errorCode}" +
                    $" | AssessmentProjectErrorDescription: {errorDescription}")
        {
        }

        protected AssessmentProjectException(SerializationInfo serializationInfo, StreamingContext streamingContext) : 
            base(serializationInfo, streamingContext)
        {
        }
    }
}
