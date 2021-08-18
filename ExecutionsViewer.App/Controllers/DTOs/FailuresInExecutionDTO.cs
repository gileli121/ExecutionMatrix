using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;
using ExecutionsViewer.App.Database.Tables.VirtualTables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class FailuresInExecutionDTO
    {
        public string FailureMessage { get; set; }

        public string Expected { get; set; }

        public string Actual { get; set; }

        public FailuresInExecutionDTO()
        {

        }

        public FailuresInExecutionDTO(Failure failure)
        {
            this.FailureMessage = failure.FailureMessage;
            this.Expected = failure.Expected;
            this.Actual = failure.Actual;
        }

    }
}
