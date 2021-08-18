using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;
using ExecutionsViewer.App.Database.Tables.VirtualTables;
using ExecutionsViewer.App.Database.Types;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class ExecutionInTestDTO
    {
        public int? Id { get; set; }

        public string ExecutionOutput { get; set; }

        public ExecutionResult ExecutionResult { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public VersionInExecutionDTO Version { get; set; }

        public List<ExecutionInTestDTO> ChildExecutions { get; set; }

        public ExecutionInTestDTO()
        {

        }

        public ExecutionInTestDTO(ChildExecution childExecution)
        {
            this.ExecutionOutput = childExecution.ExecutionOutput;
            this.ExecutionResult = childExecution.ExecutionResult;
            if (childExecution.ChildExecutions != null)
            {
                this.ChildExecutions = new List<ExecutionInTestDTO>();
                foreach (var childExecutionChildExecution in childExecution.ChildExecutions)
                    this.ChildExecutions.Add(new ExecutionInTestDTO(childExecutionChildExecution));
                
            }
        }

        public ExecutionInTestDTO(Execution execution)
        {
            this.Id = execution.Id;
            this.ExecutionOutput = execution.ExecutionOutput;
            this.ExecutionResult = execution.ExecutionResult;
            this.ExecutionDate = execution.ExecutionDate;
            if (execution.Version != null)
                this.Version = new VersionInExecutionDTO(execution.Version);
            if (execution.ChildExecutions != null)
            {
                this.ChildExecutions = new List<ExecutionInTestDTO>();
                foreach (var executionChildExecution in execution.ChildExecutions)
                    this.ChildExecutions.Add(new ExecutionInTestDTO(executionChildExecution));
            }
        }
    }
}