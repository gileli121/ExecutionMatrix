using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database.Types;

namespace ExecutionsViewer.App.Database.Tables.VirtualTables
{
    public class ChildExecution
    {
        [MaxLength(250)] public string TestMethodName { get; set; }

        [MaxLength(250)] public string TestDisplayName { get; set; }

        public string ExecutionOutput { get; set; }

        [Required] public ExecutionResult ExecutionResult { get; set; }

        public ICollection<ChildExecution> ChildExecutions { get; set; }

        public List<Failure> Failures { get; set; }


        public ChildExecution()
        {
        }


        public ChildExecution(PostExecutionDTO executionDto)
        {
            this.TestDisplayName = executionDto.TestDisplayName;
            this.TestMethodName = executionDto.TestMethodName;
            this.ExecutionOutput = executionDto.Output;
            this.ExecutionResult = executionDto.Result;

            if (executionDto.Failures?.Count > 0)
            {
                this.Failures = new List<Failure>();
                foreach (var failuresInExecutionDto in executionDto.Failures)
                    this.Failures.Add(new Failure(failuresInExecutionDto));
            }

            if (executionDto.ChildExecutions?.Count > 0)
            {
                this.ChildExecutions = new List<ChildExecution>();
                foreach (var executionDtoChildExecution in executionDto.ChildExecutions)
                    this.ChildExecutions.Add(new ChildExecution(executionDtoChildExecution));
            }
        }
    }
}