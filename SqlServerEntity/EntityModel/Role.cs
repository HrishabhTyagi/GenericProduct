namespace SqlServerEntity.EntityModel
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
