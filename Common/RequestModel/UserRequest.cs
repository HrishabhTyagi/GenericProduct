using System;
using System.Collections.Generic;
using System.Text;

namespace Common.RequestModel
{
    public class UserRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string ManagerEmail { get; set; }
        public int DepartmentId { get; set; }
        public int? UserTypeId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
