using BAL;
using Common.RequestModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace MigrationToolApi.Controllers
{
    public class UploadController : AdminBaseController
    {
        private IConfiguration _config;
        private IWebHostEnvironment _hostingEnvironment;
        private string uploadFolderName = string.Empty;
        private UploadedBusiness uploadFileBusiness;
        private string webRootPath;

        public UploadController(IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            uploadFolderName = config.GetSection("UploadFolderName").Value;
            webRootPath = _hostingEnvironment.WebRootPath;
            uploadFileBusiness = new UploadedBusiness();
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            List<UploadedFileRequest> uploadedFileList = new List<UploadedFileRequest>();
            try
            {
                if (Request.Form == null)
                    return BadRequest("Request.Form is null");

                if (Request.Form.Files.Count == 0)
                    return BadRequest("Uploaded file is not receving on server");

                var uploadedFiles = Request.Form.Files;
                string newPath = Path.Combine(webRootPath, uploadFolderName);

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);

                // Getting current user information
                var currentUser = GetCurrentUser();

                UploadedFileRequest fileObj;

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    var fi = uploadedFiles[i];
                    string fileName = ContentDispositionHeaderValue.Parse(fi.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    fi.CopyTo(stream);

                    // Creating new file object for uploading
                    fileObj = new UploadedFileRequest
                    {
                        FileExtension = fi.ContentType,
                        FileName = fi.FileName,
                        UploadedBy = currentUser.UserId,
                        FileType = GetFileType(fi.FileName),
                        FilePath = newPath + fi.FileName
                    };

                    if (fi.FileName.Contains('.'))
                        fileObj.FileExtension = fi.FileName.Substring(fi.FileName.LastIndexOf('.') + 1);

                    if (fileObj != null)
                        uploadedFileList.Add(fileObj);

                    // empty file object
                    fileObj = null;
                }

               var uploadedFileId = uploadFileBusiness.PostFiles(uploadedFileList);

                // if files successfull uploaded and saved in database then return its uploadedFileId
                return Ok(new { UploadedFileId = uploadedFileId });
            }
            catch (System.Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }
        }

        private string GetFileType(string fileName)
        {
            fileName = fileName.ToLower();

            if (fileName.Contains("pdf"))
                return "PDF";
            else if (fileName.Contains("xls") || fileName.Contains("xlsb") || fileName.Contains("xlsm") || fileName.Contains("xlsx") ||
                fileName.Contains("xla") || fileName.Contains("xlam") || fileName.Contains("xlsb"))
                return "Excel";
            else if (fileName.Contains("doc"))
                return "Word";
            return null;
        }
    }
}