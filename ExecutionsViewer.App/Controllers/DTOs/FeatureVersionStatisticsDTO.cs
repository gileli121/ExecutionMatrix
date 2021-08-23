
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class FeatureVersionStatisticsDTO
    {

        public int FeatureId { get; set; }
        public string FeatureName { get; set; }

        public ICollection<StatisticsInVersionDTO> Statistics { get; set; }


        public FeatureVersionStatisticsDTO(Feature feature, ICollection<StatisticsInVersionDTO> statistics)
        {
            this.FeatureId = feature.Id;
            this.FeatureName = feature.FeatureName;
            this.Statistics = statistics;
        }

    }
}
