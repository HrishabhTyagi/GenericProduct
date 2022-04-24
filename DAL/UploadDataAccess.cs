using Common.RequestModel;
using SqlServerEntity.EntityModel;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UploadDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Fetch all files by uploadFileId
        /// </summary>
        /// <param name="uploadedFileId"></param>
        /// <returns></returns>
        public List<UploadedFile> GetFileListByUploadedFileId(int uploadedFileId)
        {
            List<UploadedFile> files = cache.GetValue<List<UploadedFile>>(CacheKeys.UploadedFile);

            if (files != null && files.Count > 0)
                return files.Where(file => !file.IsDeleted && file.UploadedFileId == uploadedFileId).ToList();

            files = _context.UploadedFiles.Where(file => !file.IsDeleted && file.UploadedFileId == uploadedFileId).ToList();
            cache.Add(CacheKeys.UploadedFile, _context.UploadedFiles.ToList());
            return files;
        }

        /// <summary>
        /// Insert new file list
        /// </summary>
        /// <param name="uploadedFiles"></param>
        /// <returns></returns>
        public int PostFiles(List<UploadedFile> uploadedFiles)
        {
            int nextUploadFileId = 1;
            RemoveCache();

            UploadedFile lstRecord = _context.UploadedFiles.AsEnumerable().Last();

            if (lstRecord == null)
                uploadedFiles.ForEach(a => a.UploadedFileId = nextUploadFileId);
            else
            {
                nextUploadFileId = lstRecord.UploadedFileId + 1;
                uploadedFiles.ForEach(a => a.UploadedFileId = nextUploadFileId);
            }

            _context.UploadedFiles.AddRange(uploadedFiles);

            int res = _context.SaveChanges();
            if (res > 0)
                return nextUploadFileId;
            else
                throw new Exception("Problem in file saving");
        }

        /// <summary>
        /// Update uploaded files
        /// </summary>
        /// <param name="uploadedFiles"></param>
        /// <returns></returns>
        public int PutFiles(List<UploadedFile> uploadedFiles)
        {
            RemoveCache();
            int uploadedFileId = uploadedFiles.First().UploadedFileId;

            // First removing all file by updating the status of IsDeleted flag
            List<UploadedFile> files = _context.UploadedFiles.Where(file => !file.IsDeleted && file.UploadedFileId == uploadedFileId).ToList();
            files.ForEach(file => file.IsDeleted = true);

            //inserting as new files
            _context.UploadedFiles.AddRange(uploadedFiles);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Soft delete of User
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int DeleteFiles(int uploadedFileId)
        {
            RemoveCache();
            List<UploadedFile> files = _context.UploadedFiles.Where(file => !file.IsDeleted && file.UploadedFileId == uploadedFileId).ToList();

            if (files.Count > 0)
            {
                files.ForEach(file => file.IsDeleted = true);
                return _context.SaveChanges();
            }
            return 0;
        }

        private void RemoveCache()
        {
            cache.Delete(CacheKeys.UploadedFile);
        }

    }
}
