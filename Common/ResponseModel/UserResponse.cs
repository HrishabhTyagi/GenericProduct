using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ResponseModel
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string ManagerEmail { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? UserTypeId { get; set; }
        public bool IsDeleted { get; set; } 
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Token { get; set; }
    }
}
