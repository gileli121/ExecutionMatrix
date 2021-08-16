using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;

namespace ExecutionsViewer.App.Database.Tables
{
    public class Test
    {
        public int Id { get; set; }

        public TestClass? TestClass { get; set; }
        public int? TestClassId { get; set; }

        public ICollection<Test> ChildTests { get; set; }

        public Test TestOwner { get; set; }
        public int? TestId { get; set; }

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
                ChildTests = new List<Test>();
                foreach (var executionDtoChildExecution in executionDto.ChildExecutions)
                {
                    ChildTests.Add(new Test(executionDtoChildExecution));
                }
            }
        }


        public void AddExecution(Execution execution)
        {

            Executions ??= new List<Execution>();

            Executions.Add(execution);
            if (execution.ChildExecutions == null)
                return;

            if (ChildTests == null)
                return;

            foreach (var childTest in ChildTests)
            {
                foreach (var executionChildExecution in execution.ChildExecutions)
                {
                    if (childTest.TestMethodName == executionChildExecution.Test.TestMethodName)
                    {
                        childTest.AddExecution(executionChildExecution);
                        break;
                    }
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


            var validCount = 0;
            foreach (var childExecutionDto in executionDto.ChildExecutions)
            {
                foreach (var childTest in ChildTests)
                {
                    if (TestMethodName != null && childTest.TestMethodName != null)
                    {
                        if (childExecutionDto.TestMethodName == childTest.TestMethodName)
                        {
                            if (childTest.IsMatchToExecution(childExecutionDto))
                                validCount++;
                            else
                                return false;

                            break;
                        }
                    }
                    else if (TestDisplayName != null && childTest.TestDisplayName != null)
                    {
                        if (childExecutionDto.TestDisplayName == childTest.TestDisplayName)
                        {
                            if (childTest.IsMatchToExecution(childExecutionDto))
                                validCount++;
                            else
                                return false;

                            break;
                        }
                    }
                    else if (TestMethodName == null && TestDisplayName == null)
                    {
                        throw new Exception("Invalid payload");
                    }

                }
            }

            return validCount == ChildTests.Count;
        }
    }
}