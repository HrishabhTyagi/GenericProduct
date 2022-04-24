namespace SqlServerEntity.EntityModel
{
    public class MenuRoleMapping
    {
        public int MenuRoleMappingId { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
