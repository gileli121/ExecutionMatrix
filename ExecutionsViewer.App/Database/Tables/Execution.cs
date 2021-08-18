using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database.Extensions;
using ExecutionsViewer.App.Database.Tables.VirtualTables;
using ExecutionsViewer.App.Database.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExecutionsViewer.App.Database.Tables
{
    public class ExecutionConfiguration : IEntityTypeConfiguration<Execution>
    {
        public void Configure(EntityTypeBuilder<Execution> builder)
        {
            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.Failures).HasJsonConversion();
            builder.Property(e => e.ChildExecutions).HasJsonConversion();
        }
    }

    public sealed class Execution
    {
        public int Id { get; set; }

        public Version Version { get; set; }
        public int VersionId { get; set; }

        public Test Test { get; set; }
        public int TestId { get; set; }

        public string ExecutionOutput { get; set; }

        [Required] public ExecutionResult ExecutionResult { get; set; }
        public DateTime ExecutionDate { get; set; }


        public List<ChildExecution> ChildExecutions { get; set; }

        public List<Failure> Failures { get; set; }

        public Execution()
        {
        }

        // Constructor that used for parent execution


        public Execution(Test test, PostExecutionDTO executionDto, Version version = null)
        {
            this.Test = test;
            this.ExecutionResult = executionDto.Result;
            this.ExecutionOutput = executionDto.Output;
            this.ExecutionDate = DateTime.Now;
            this.Version = version;
            if (executionDto.Failures?.Count > 0)
            {
                this.Failures = new List<Failure>();
                foreach (var differenceInExecutionDto in executionDto.Failures)
                    this.Failures.Add(new Failure(differenceInExecutionDto));
            }

            if (executionDto.ChildExecutions != null && test.ChildTests != null)
            {
                ChildExecutions = new List<ChildExecution>();
                foreach (var executionDtoChildExecution in executionDto.ChildExecutions)
                {
                    foreach (var testChildTest in test.ChildTests)
                    {
                        if (testChildTest.TestMethodName == executionDtoChildExecution.TestMethodName)
                        {
                            ChildExecutions.Add(new ChildExecution(executionDtoChildExecution));
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