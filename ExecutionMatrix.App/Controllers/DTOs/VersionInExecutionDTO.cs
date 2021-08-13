
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Database.Tables;

namespace ExecutionMatrix.App.Controllers.DTOs
{
    public class VersionInExecutionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public VersionInExecutionDTO()
        {

        }

        public VersionInExecutionDTO(Version version)
        {
            this.Id = version.Id;
            this.Name = version.Name;
        }
    }
}
