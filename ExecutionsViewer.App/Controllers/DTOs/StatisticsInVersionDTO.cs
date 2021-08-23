using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class StatisticsInVersionDTO
    {
        public int VersionId { get; set; }

        public string VersionName { get; set; }

        public int TotalTests { get; set; }

        public int TotalExecutedTests { get; set; }

        public int TotalPassedTests { get; set; }

        public StatisticsInVersionDTO(Version version, int totalTests, int totalExecutions, int totalPassedExecutions)
        {
            this.VersionId = version.Id;
            this.VersionName = version.Name;
            this.TotalTests = totalTests;
            this.TotalExecutedTests = totalExecutions;
            this.TotalPassedTests = totalPassedExecutions;
        }
    }
}
