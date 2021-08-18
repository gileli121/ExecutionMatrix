using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database.Extensions;
using ExecutionsViewer.App.Database.Tables.VirtualTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExecutionsViewer.App.Database.Tables
{

    public class Test
    {
        public int Id { get; set; }

        public TestClass TestClass { get; set; }
        public int TestClassId { get; set; }

        public Version FirstVersion { get; set; }
        public int? FirstVersionId { get; set; }

        [MaxLength(250)] public string TestMethodName { get; set; }

        [MaxLength(250)] public string TestDisplayName { get; set; }

        public virtual List<Feature> Features { get; set; } = new List<Feature>();

        public ICollection<Execution> Executions { get; set; }

        public Test()
        {
        }

        public Test(PostExecutionDTO executionDto, TestClass testClass = null, Version firstVersion = null)
        {
            this.TestClass = testClass;
            this.FirstVersion = firstVersion;
            this.TestMethodName = executionDto.TestMethodName;
            this.TestDisplayName = executionDto.TestDisplayName;
        }

        public bool IsMatchToExecution(PostExecutionDTO executionDto)
        {
            if (executionDto.TestClass != null && TestClass != null)
            {
                if (TestClass.PackageName != executionDto.TestClass?.PackageName ||
                    TestClass.ClassName != executionDto.TestClass?.ClassName)
                    return false;
            }

            if (TestMethodName != null && executionDto.TestMethodName != null)
            {
                if (TestMethodName != executionDto.TestMethodName)
                    return false;
            }
            else if (TestDisplayName != null && executionDto.TestDisplayName != null)
            {
                if (TestDisplayName != executionDto.TestDisplayName)
                    return false;
            }

            return true;

        }
    }
}