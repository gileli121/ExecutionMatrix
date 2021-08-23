using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class ClassVersionStatisticsDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDisplayName { get; set; }
        public string ClassPackageName { get; set; }

        public ICollection<StatisticsInVersionDTO> Statistics { get; set; }


        public ClassVersionStatisticsDTO(TestClass testClass, ICollection<StatisticsInVersionDTO> statistics)
        {
            this.ClassId = testClass.Id;
            this.ClassName = testClass.ClassName;
            this.ClassDisplayName = testClass.DisplayName;
            this.ClassPackageName = testClass.PackageName;
            this.Statistics = statistics;
        }
    }
}
