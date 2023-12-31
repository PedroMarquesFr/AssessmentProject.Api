﻿namespace AssessmentProject.Shared
{
    public enum EAssessmentProjectErrorCode
    {
        ConnectionDataBaseError = 1,
        ConnectionStorageServiceError = 2,
        FileNotFoundStorageServiceError = 3,
        DocumentNotFoundInDatabaseError = 4,
        DocumentFileNameIsEmptyError = 5,
        PutApiAssessmentProjectRequestError = 6,
        PostApiMyLimsWebRequestError = 7,
        GetDaqListQueryRequestIsNullError = 8,
        GetDaqListQueryAccountIdIsZeroError = 9,
        GetEquipmentListQueryRequestIsNullError = 10,
        GetEquipmentListQueryAccountIdIsZeroError = 11,
        GetDaqQueryIdIsZeroError = 12,
        UpdateDaqCommandResponseIsNullError = 13,
        GetEquipmentByIdQueryRequestPropertyIdIsEmptyError = 14,
        CreateEquipmentCommandRequestPropertyEquipmentIsNullError = 15,
        CreateEquipmentCommandRequestEquipmentIdIsNotEmptyError = 16,
        EquipmentIdentificationIsNullOrEmptyError = 17,
        AccountIdIsEmptyError = 18,
        EquipmentTypeIdIsEmptyError = 19,
        EditionUserIdIsNullOrEmptyError = 20,
        EditionDateTimeIsNullError = 21,
        UpdateEquipmentCommandRequestEquipmentIdIsEmptyError = 22,
        DeactivateEquipmentCommandRequestEquipmentIdIsEmpty = 23,
        DeactivateEquipmentCommandRequestEquipmentNotFound = 24,
        DeactivateEquipmentCommandRequestUserIdIsEmpty = 25,
        CreateEquipmentCommandRequestEquipmentTypeIdIsNotEmptyError = 26,
        CreateEquipmentTypeCommandRequestPropertyEquipmentTypeIsNullError = 27,
        EquipmentTypeIdentificationIsNullOrEmptyError = 28,
        DeactivateEquipmentTypeCommandRequestEquipmentIdIsEmpty = 29,
        DeactivateEquipmentTypeCommandRequestUserIdIsEmpty = 30,
        DeactivateEquipmentTypeCommandRequestEquipmentNotFound = 31,
        UpdateEquipmentTypeCommandRequestEquipmentIdIsEmptyError = 32,
        EquipmentTypeOfTypeIdIsEmptyError = 33,
        AccountIdTypeIsEmptyError = 34,
        EditionUserIdTypeIsNullOrEmptyError = 35,
        EditionDateTimeTypeIsNullError = 36,
        GetEquipmentTypeListQueryAccountIdIsZeroError = 37,
        GetEquipmentTypeByIdQueryRequestPropertyIdIsEmptyError = 38,
        RequestDaqConfigComIsNull = 39,
        RequestDaqConfigComIdIsNotEmpty = 40,
        RequestDaqConfigComHasNullableProperties = 41,
        RequestDaqConfigComIdIsEmpty = 42,
        RequestDaqConfigComDaqIdIsEmpty = 43,
        RequestDaqConfigFileDaqIdIsEmpty = 44,
        RequestDaqConfigFileIdIsEmpty = 45

    }
}
