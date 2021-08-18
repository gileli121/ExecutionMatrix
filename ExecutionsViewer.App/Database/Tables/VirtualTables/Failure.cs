using ExecutionsViewer.App.Controllers.DTOs;

namespace ExecutionsViewer.App.Database.Tables.VirtualTables
{
    public class Failure
    {

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