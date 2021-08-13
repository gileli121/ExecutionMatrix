using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Database.Tables;
using ExecutionMatrix.App.Database.Types;

namespace ExecutionMatrix.App.Controllers.DTOs
{
    public class ExecutionResultDTO
    {
        public string TestMethodName { get; set; }
        public string TestDisplayName { get; set; }
        public ExecutionResult ExecutionResult { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string ExecutionOutput { get; set; }
        public List<ExecutionResultDTO> ChildExecutions { get; set; }

        public ExecutionResultDTO(Execution execution)
        {
            this.TestMethodName = execution.Test.TestMethodName;
            this.TestDisplayName = execution.Test.TestDisplayName;
            this.ExecutionResult = execution.ExecutionResult;
            this.ExecutionDate = execution.ExecutionDate;
            this.ExecutionOutput = execution.ExecutionOutput;
            if (execution.ChildExecutions?.Count > 0)
            {
                this.ChildExecutions = new List<ExecutionResultDTO>();
                foreach (var executionChildExecution in execution.ChildExecutions)
                    this.ChildExecutions.Add(new ExecutionResultDTO(executionChildExecution));
            }
        }
    }
}