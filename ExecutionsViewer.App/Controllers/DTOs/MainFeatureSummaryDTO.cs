using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class MainFeatureSummaryDTO
    {
        public int Id { get; set; }
        public string FeatureName { get; set; }

        public int TotalTests { get; set; }

        public int TotalTestsCollections { get; set; }

        public int TotalExecutedTests { get; set; }

        public int TotalPassedTests { get; set; }

        public MainFeatureSummaryDTO(MainFeature mainFeature, int totalTests, int totalTestsCollections, int totalExecutedTests,
            int totalPassedTests)
        {
            this.Id = mainFeature.Id;
            this.FeatureName = mainFeature.FeatureName;
            this.TotalTests = totalTests;
            this.TotalTestsCollections = totalTestsCollections;
            this.TotalExecutedTests = totalExecutedTests;
            this.TotalPassedTests = totalPassedTests;
        }
    }
}
