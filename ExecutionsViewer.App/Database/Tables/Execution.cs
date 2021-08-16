using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database.Types;

namespace ExecutionsViewer.App.Database.Tables
{
    public sealed class Execution
    {
        public int Id { get; set; }
        public int? ExecutionId { get; set; }

        public Version Version { get; set; }
        public int? VersionId { get; set; }

        public Test Test { get; set; }
        public int TestId { get; set; }

        public string ExecutionOutput { get; set; }

        [Required] public ExecutionResult ExecutionResult { get; set; }
        public DateTime? ExecutionDate { get; set; }

        public Execution ExecutionOwner { get; set; }

        public ICollection<Execution> ChildExecutions { get; set; }

        public Execution()
        {
        }

        // Constructor that used for parent execution
        public Execution(Test test, ExecutionResult executionResult, Version version,
            ICollection<Feature> features)
        {
            this.Test = test;
            this.ExecutionResult = executionResult;
            this.ExecutionDate = DateTime.Now;
            this.Version = version;
        }


        // Constructor that used for child execution
        public Execution(Test test, ExecutionResult executionResult, string executionOutput = null)
        {
            this.Test = test;
            this.ExecutionResult = executionResult;
            this.ExecutionOutput = executionOutput;
            this.ExecutionDate = DateTime.Now;
        }


        public Execution(Test test, PostExecutionDTO executionDto, Version version = null)
        {
            this.Test = test;
            this.ExecutionResult = executionDto.Result;
            this.ExecutionOutput = executionDto.Output;
            this.ExecutionDate = DateTime.Now;
            this.Version = version;
            if (executionDto.ChildExecutions != null && test.ChildTests != null)
            {
                ChildExecutions = new List<Execution>();
                foreach (var executionDtoChildExecution in executionDto.ChildExecutions)
                {
                    foreach (var testChildTest in test.ChildTests)
                    {
                        if (testChildTest.TestMethodName == executionDtoChildExecution.TestMethodName)
                        {
                            ChildExecutions.Add(new Execution(testChildTest, executionDtoChildExecution));
                            break;
                        }
                    }
                }
            }
        }


        // public void AddChildExecutions(ICollection<PostExecutionDTO> childExecutionDtos, Test childTestOfChildExecution)
        // {
        //     if (childExecutionDtos == null || childExecutionDtos.Count == 0)
        //         return;
        //
        //     foreach (var childExecutionDto in childExecutionDtos)
        //     {
        //         var execution = new Execution(childTestOfChildExecution, childExecutionDto.Result);
        //         execution.AddChildExecutions(childExecutionDto.ChildExecutions);
        //
        //         ChildExecutions ??= new List<Execution>();
        //         ChildExecutions.Add(execution);
        //     }
        // }
    }
}