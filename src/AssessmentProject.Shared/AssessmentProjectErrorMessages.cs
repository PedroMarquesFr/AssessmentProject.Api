using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Shared.Constants
{
    [ExcludeFromCodeCoverage]
    public static class AssessmentProjectErrorMessages
    {
        public const string SqlConnectionFactoryNotCreatedError = "SqlConnectionFactory has not been created.";
        public const string GetDaqListQueryRequestIsNullError = "GetDaqListQueryRequest is null.";
        public const string GetDaqListQueryAccountIdIsZeroError = "GetDaqListQueryRequest AccountId request value is zero.";
        public const string GetEquipmentListQueryRequestIsNullError = "GetEquipmentListQueryRequest is null.";
        public const string GetEquipmentListQueryAccountIdIsZeroError = "GetEquipmentListQueryRequest AccountId request value is zero.";
        public const string GetDaqQueryIdIsZeroError = "GetDaqByIdQueryRequest Id request value is zero.";
        public const string UpdateDaqCommandResponseIsNullError = "UpdateDaqCommandResponse is null.";
        public const string GetEquipmentByIdQueryRequestPropertyIdIsEmptyError = "GetEquipmentByIdQueryRequest property Id is empty.";
        public const string EquipmentIsNullError = "Equipment is null.";
        public const string CreateEquipmentCommandRequestEquipmentIdIsNotEmptyError = "CreateEquipmentCommandRequest EquipmentId is not empty.";
        public const string EquipmentIdentificationIsNullOrEmptyError = "Equipment Identification Is Null Or Empty.";
        public const string AccountIdIsEmptyError = "Account Id Is Empty in the Equipment.";
        public const string EquipmentTypeIdIsEmptyError = "Equipment Type Id Is Empty  in the Equipment.";
        public const string EditionUserIdIsNullOrEmptyError = "Edition User Id Is Null or Empty  in the Equipment.";
        public const string EditionDateTimeIsNullError = "Edition Date Time Is Null  in the Equipment.";
        public const string UpdateEquipmentCommandRequestEquipmentIdIsEmptyError = "UpdateEquipmentCommandRequest EquipmentId is empty.";
        public const string DeactivateEquipmentCommandRequestEquipmentIdIsEmpty = "DeactivateEquipmentCommandRequest is with Id empty.";
        public const string DeactivateEquipmentCommandRequestEquipmentNotFound = "DeactivateEquipmentCommandRequest Equipment not found.";
        public const string DeactivateEquipmentCommandRequestUserIdIsEmpty = "DeactivateEquipmentCommandRequest is with User Id empty.";
        public const string CreateEquipmentCommandRequestEquipmentTypeIdIsNotEmptyError = "CreateEquipmentTypeCommandRequest EquipmentTypeId is not empty.";
        public const string EquipmentTypeIsNullError = "EquipmentType is null.";
        public const string EquipmentTypeIdentificationIsNullOrEmptyError = "EquipmentType Identification Is Null Or Empty.";
        public const string DeactivateEquipmentTypeCommandRequestEquipmentIdIsEmpty = "DeactivateEquipmentTypeCommandRequest is with Id empty.";
        public const string DeactivateEquipmentTypeCommandRequestUserIdIsEmpty = "DeactivateEquipmentTypeCommandRequest is with Id empty.";
        public const string DeactivateEquipmentTypeCommandRequestEquipmentNotFound = "DeactivateEquipmentCommandRequest Equipment not found.";
        public const string UpdateEquipmentTypeCommandRequestEquipmentTypeIdIsEmptyError = "UpdateEquipmentTypeCommandRequest EquipmentTypeId is empty.";
        public const string EquipmentTypeOfTypeIdIsEmptyError = "Equipment Type Id Is Empty  in the EquipmentType.";
        public const string AccountIdTypeIsEmptyError = "Account Id Is Empty in the EquipmentType.";
        public const string EditionUserIdTypeIsNullOrEmptyError = "Edition User Id Is Null or Empty  in the EquipmentType.";
        public const string EditionDateTimeTypeIsNullError = "Edition Date Time Is Null  in the EquipmentType.";
        public const string GetEquipmentTypeListQueryAccountIdIsZeroError = "GetEquipmentTypeListQueryRequest AccountId request value is zero.";
        public const string GetEquipmentTypeByIdQueryRequestPropertyIdIsEmptyError = "GetEquipmentByIdQueryRequest property Id is empty.";
        public const string RequestDaqConfigComIsNull = "DaqConfigCom is null.";
        public const string RequestDaqConfigComIdIsNotEmpty = "DaqConfigCom Id is not empty.";
        public const string RequestDaqConfigComHasNullableProperties = "DaqConfigCom has nullable properties.";
        public const string RequestDaqConfigComIdIsEmpty = "DaqConfigCom Id is empty.";
        public const string RequestDaqConfigComDaqIdIsEmpty = "DaqConfigCom DaqId is empty.";
        public const string RequestDaqConfigFileDaqIdIsEmpty = "DaqConfigCom DaqId is empty.";
        public const string RequestDaqConfigFileIdIsEmpty = "DaqConfigFile Id is empty.";
    }
}
