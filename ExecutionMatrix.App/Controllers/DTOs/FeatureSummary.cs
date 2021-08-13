﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Database.Tables;

namespace ExecutionMatrix.App.Controllers.DTOs
{
    public class FeatureSummary
    {
        public int Id { get; set; }

        public string FeatureName { get; set; }

        public int TotalTests { get; set; }

        public int TotalExecutedTests { get; set; }

        public int TotalPassedTests { get; set; }


        public FeatureSummary()
        {

        }

        public FeatureSummary(Feature feature, int totalTests, int totalExecutedTests, int totalPassedTests)
        {
            this.Id = feature.Id;
            this.FeatureName = feature.FeatureName;
            this.TotalTests = totalTests;
            this.TotalExecutedTests = totalExecutedTests;
            this.TotalPassedTests = totalPassedTests;
        }
    }
}
