using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Types;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class PostExecutionDTO
    {
        [MaxLength(250)]
        public string TestMethodName { get; set; }

        [MaxLength(250)]
        public string TestDisplayName { get; set; }

        public PostTestClassDTO TestClass { get; set; }

        public ICollection<string> FeatureNames { get; set; }
        public string VersionName { get; set; }

        [Required]
        public ExecutionResult Result { get; set; }
        public string Output { get; set; }

        public ICollection<FailuresInExecutionDTO> Failures { get; set; }

        public ICollection<PostExecutionDTO> ChildExecutions { get; set; }
        public int ExecutionId { get; set; }

    }
}
