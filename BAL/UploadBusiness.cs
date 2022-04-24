using Common.RequestModel;
using Common.ResponseModel;
using DAL;
using SqlServerEntity.EntityModel;
using System.Collections.Generic;

namespace BAL
{
    public class UploadedBusiness : BaseBusiness
    {
        private readonly UploadDataAccess uploadDataAccess;
        public UploadedBusiness()
        {
            uploadDataAccess = new UploadDataAccess();
        }

        public List<UploadedFileResponse> GetFileListByUploadedFileId(int uploadedFileId)
        {
            List<UploadedFile> FilesList = uploadDataAccess.GetFileListByUploadedFileId(uploadedFileId);
            List<UploadedFileResponse> uploadedFilesResponse = ListMapping<UploadedFile, UploadedFileResponse>(FilesList);
            return uploadedFilesResponse;
        }

        public int PostFiles(List<UploadedFileRequest> uploadedFiles)
        {
            List<UploadedFile> files = ListMapping<UploadedFileRequest, UploadedFile>(uploadedFiles);
            return uploadDataAccess.PostFiles(files);
        }

        public int PutFiles(List<UploadedFileRequest> uploadedFiles)
        {
            List<UploadedFile> files = ListMapping<UploadedFileRequest, UploadedFile>(uploadedFiles);
            return uploadDataAccess.PutFiles(files);
        }

        public int DeleteFiles(int uploadedFileId)
        {
            int result = uploadDataAccess.DeleteFiles(uploadedFileId);
            return result;
        }
    }
}
