using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModel
{
    public partial class CloudMigrationTrackerContext : DbContext
    {
        public CloudMigrationTrackerContext()
        {
        }

        public CloudMigrationTrackerContext(DbContextOptions<CloudMigrationTrackerContext> options)
            : base(options)
        {
        }

      
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
    

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
