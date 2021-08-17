using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;
using ExecutionsViewer.App.Database.Types;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class ExecutionResultDTO
    {
        public string TestMethodName { get; set; }
        public string TestDisplayName { get; set; }
        public ExecutionResult ExecutionResult { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string ExecutionOutput { get; set; }
        public List<ExecutionResultDTO> ChildExecutions { get; set; }
        public List<FailuresInExecutionDTO> Failures { get; set; }

        public ExecutionResultDTO(Execution execution)
        {
            this.TestMethodName = execution.Test.TestMethodName;
            this.TestDisplayName = execution.Test.TestDisplayName;
            this.ExecutionResult = execution.ExecutionResult;
            this.ExecutionDate = execution.ExecutionDate;
            this.ExecutionOutput = execution.ExecutionOutput;
            if (execution.Failures?.Count > 0)
            {
                this.Failures = new List<FailuresInExecutionDTO>();
                foreach (var executionFailure in execution.Failures)
                    this.Failures.Add(new FailuresInExecutionDTO(executionFailure));
            }

            if (execution.ChildExecutions?.Count > 0)
            {
                this.ChildExecutions = new List<ExecutionResultDTO>();
                foreach (var executionChildExecution in execution.ChildExecutions)
                    this.ChildExecutions.Add(new ExecutionResultDTO(executionChildExecution));
            }
        }
    }
}