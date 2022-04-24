using System;

namespace Common.RequestModel
{
    public class UploadedFileRequest
    {
        public int FileIndexId { get; set; }
        public int UploadedFileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int UploadedBy { get; set; }
        public DateTime? UploadedOn { get; set; } = DateTime.Now;
        public int DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
