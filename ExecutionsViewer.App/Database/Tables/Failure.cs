using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;

namespace ExecutionsViewer.App.Database.Tables
{
    public class Failure
    {
        public int Id { get; set; }

        public Execution Execution { get; set; }
        public int ExecutionId { get; set; }

        public string FailureMessage { get; set; }

        public string Expected { get; set; }

        public string Actual { get; set; }


        public Failure()
        {

        }

        public Failure(FailuresInExecutionDTO failuresDto)
        {
            this.FailureMessage = failuresDto.FailureMessage;
            this.Expected = failuresDto.Expected;
            this.Actual = failuresDto.Actual;
        }
    }
}