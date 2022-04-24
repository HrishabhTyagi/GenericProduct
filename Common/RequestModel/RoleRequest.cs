namespace Common.RequestModel
{
    using Common.ProductContant;
    using System;
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = (int)RoleConstant.Administrator;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; } = (int)RoleConstant.Administrator;
    }
}
