using System;
using System.Collections.Generic;

namespace SqlServerEntity.EntityModel
{
    public partial class User
    {
        public int UserId { get; set; }
        public int? UploadedFileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string ManagerEmail { get; set; }
        public int DepartmentId { get; set; }
        public int? UserTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
