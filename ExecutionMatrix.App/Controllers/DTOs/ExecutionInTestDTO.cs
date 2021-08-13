﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Database.Tables;
using ExecutionMatrix.App.Database.Types;

namespace ExecutionMatrix.App.Controllers.DTOs
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