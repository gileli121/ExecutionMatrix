using ExecutionsViewer.App.Database.Tables;
using Microsoft.EntityFrameworkCore;
using Version = ExecutionsViewer.App.Database.Tables.Version;

namespace ExecutionsViewer.App.Database
{
    public class ExecutionsViewerDbContext : DbContext 
    {
        // DbContextOptions<ExecutionsViewerDbContext> contextOptions;

        public DbSet<Test> Tests { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Execution> Executions { get; set; }

        public DbSet<Tables.Version> Versions { get; set; }

        public DbSet<TestClass> TestClasses { get; set; }

        public ExecutionsViewerDbContext(DbContextOptions<ExecutionsViewerDbContext> contextOptions) : base(contextOptions)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ExecutionConfiguration());
            builder.ApplyConfiguration(new TestConfiguration());
        }
    }
}