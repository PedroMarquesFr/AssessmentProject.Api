using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class DaqConfigFile
    {
        public readonly Guid Id;
        public readonly string Identification;
        public readonly bool Active;
        public readonly Guid DaqId;
        public readonly string Path;
        public readonly string Filter;
        public readonly bool SearchInSubDir;
        public readonly string FileEncoding;
        public readonly bool ProcessExistingFiles;
        public readonly DateTime? EditionDateTime;
        public readonly Guid? EditionUserId;
        public readonly DateTime? ActivationDateTime;
        public readonly Guid? ActivationUserId;

        public DaqConfigFile(
            Guid id, 
            string identification, 
            bool active, 
            Guid daqId, 
            string path, 
            string filter, 
            bool searchInSubDir, 
            string fileEncoding, 
            bool processExistingFiles, 
            DateTime? editionDateTime, 
            Guid? editionUserId, 
            DateTime? activationDateTime, 
            Guid? activationUserId)
        {
            Id = id;
            Identification = identification;
            Active = active;
            DaqId = daqId;
            Path = path;
            Filter = filter;
            SearchInSubDir = searchInSubDir;
            FileEncoding = fileEncoding;
            ProcessExistingFiles = processExistingFiles;
            EditionDateTime = editionDateTime;
            EditionUserId = editionUserId;
            ActivationDateTime = activationDateTime;
            ActivationUserId = activationUserId;
        }
    }
}
