using Microsoft.EntityFrameworkCore;

namespace SqlServerEntity.EntityModel
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
      
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<MenuRoleMapping> MenuRoleMappings { get; set; }
        public virtual DbSet<UploadedFile> UploadedFiles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserSessionTracking> UserSessionTrackings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(@"Data Source = localhost; Initial Catalog = GenericDb; Integrated Security = True;");
            }
        }
    }
}
