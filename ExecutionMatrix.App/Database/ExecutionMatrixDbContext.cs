using ExecutionMatrix.App.Database.Tables;
using Microsoft.EntityFrameworkCore;
using Version = ExecutionMatrix.App.Database.Tables.Version;

namespace ExecutionMatrix.App.Database
{
    public class ExecutionMatrixDbContext : DbContext
    {
        // DbContextOptions<ExecutionMatrixDbContext> contextOptions;

        public DbSet<Test> Tests { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Execution> Executions { get; set; }

        public DbSet<Version> Versions { get; set; }

        public DbSet<TestClass> TestClasses { get; set; }

        public ExecutionMatrixDbContext(DbContextOptions<ExecutionMatrixDbContext> contextOptions) : base(contextOptions)
        {
        }
    }
}