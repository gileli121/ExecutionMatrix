
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class VersionBaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public VersionBaseDTO(Version version)
        {
            this.Id = version.Id;
            this.Name = version.Name;
        }
    }
}
