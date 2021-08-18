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
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(t => t.ChildTests).HasJsonConversion();
        }
    }

    public class Test
    {
        public int Id { get; set; }

        public TestClass TestClass { get; set; }
        public int TestClassId { get; set; }


        public List<ChildTest> ChildTests { get; set; }

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
            if (executionDto.ChildExecutions is {Count: > 0})
            {
                ChildTests = new List<ChildTest>();
                foreach (var executionDtoChildExecution in executionDto.ChildExecutions)
                {
                    ChildTests.Add(new ChildTest(executionDtoChildExecution));
                }
            }
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

            if ((ChildTests == null || ChildTests.Count == 0) &&
                (executionDto.ChildExecutions == null || executionDto.ChildExecutions.Count == 0))
                return true;


            if (ChildTests == null && executionDto.ChildExecutions == null)
                return true;
            else if (ChildTests == null || executionDto.ChildExecutions == null)
                return false;


            return true;

            // var validCount = 0;
            // foreach (var childExecutionDto in executionDto.ChildExecutions)
            // {
            //     foreach (var childTest in ChildTests)
            //     {
            //         if (TestMethodName != null && childTest.TestMethodName != null)
            //         {
            //             if (childExecutionDto.TestMethodName == childTest.TestMethodName)
            //             {
            //                 if (childTest.IsMatchToExecution(childExecutionDto))
            //                     validCount++;
            //                 else
            //                     return false;
            //
            //                 break;
            //             }
            //         }
            //         else if (TestDisplayName != null && childTest.TestDisplayName != null)
            //         {
            //             if (childExecutionDto.TestDisplayName == childTest.TestDisplayName)
            //             {
            //                 if (childTest.IsMatchToExecution(childExecutionDto))
            //                     validCount++;
            //                 else
            //                     return false;
            //
            //                 break;
            //             }
            //         }
            //         else if (TestMethodName == null && TestDisplayName == null)
            //         {
            //             throw new Exception("Invalid payload");
            //         }
            //
            //     }
            // }
            //
            // return validCount == ChildTests.Count;
        }
    }
}