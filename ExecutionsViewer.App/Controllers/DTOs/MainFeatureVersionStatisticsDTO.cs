using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class MainFeatureVersionStatisticsDTO
    {

        public int MainFeatureId { get; set; }
        public string MainFeatureName { get; set; }

        public ICollection<StatisticsInVersionDTO> Statistics { get; set; }


        public MainFeatureVersionStatisticsDTO(MainFeature mainFeature, ICollection<StatisticsInVersionDTO> statistics)
        {
            this.MainFeatureId = mainFeature.Id;
            this.MainFeatureName = mainFeature.FeatureName;
            this.Statistics = statistics;
        }
    }
}
