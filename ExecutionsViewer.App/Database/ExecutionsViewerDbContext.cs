using ExecutionsViewer.App.Database.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Version = ExecutionsViewer.App.Database.Tables.Version;

namespace ExecutionsViewer.App.Database
{
    public class ExecutionsViewerDbContext : DbContext 
    {
        // DbContextOptions<ExecutionsViewerDbContext> contextOptions;

        public DbSet<Test> Tests { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<MainFeature> MainFeatures { get; set; }

        public DbSet<Execution> Executions { get; set; }

        public DbSet<Tables.Version> Versions { get; set; }

        public DbSet<TestClass> TestClasses { get; set; }

        public ExecutionsViewerDbContext(DbContextOptions<ExecutionsViewerDbContext> contextOptions) : base(contextOptions)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ExecutionConfiguration());
        }


        public void UpdateOrAdd(Test test)
        {
            if (test.Id == 0)
                Tests.Add(test);
            else
                Tests.Update(test);
        }


        public void UpdateOrAdd(Version version)
        {
            if (version.Id == 0)
                Versions.Add(version);
            else
                Versions.Update(version);
        }

        public void UpdateOrAdd(Feature feature)
        {
            if (feature.Id == 0)
                Features.Add(feature);
            else
                Features.Update(feature);
        }

        public void UpdateOrAdd(MainFeature mainFeature)
        {
            if (mainFeature.Id == 0)
                MainFeatures.Add(mainFeature);
            else
                MainFeatures.Update(mainFeature);
        }

        public void UpdateOrAdd(TestClass testClass)
        {
            if (testClass.Id == 0)
                TestClasses.Add(testClass);
            else
                TestClasses.Update(testClass);
        }
    }
}