using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class TestClassSummaryDTO
    {
        public int Id { get; set; }

        public string PackageName { get; set; }

        public string ClassName { get; set; }

        public string DisplayName { get; set; }

        public int TotalTests { get; set; }

        public int TotalExecutedTests { get; set; }

        public int TotalPassedTests { get; set; }


        public TestClassSummaryDTO()
        {
        }

        public TestClassSummaryDTO(TestClass testClass, int totalTests, int totalExecutedTests, int totalPassedTests)
        {
            this.Id = testClass.Id;
            this.PackageName = testClass.PackageName;
            this.ClassName = testClass.ClassName;
            this.DisplayName = testClass.DisplayName;
            this.TotalTests = totalTests;
            this.TotalExecutedTests = totalExecutedTests;
            this.TotalPassedTests = totalPassedTests;
        }
    }
}