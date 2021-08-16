using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class FeatureInTestDTO
    {
        public int Id { get; set; }
        public string FeatureName { get; set; }

        public FeatureInTestDTO()
        {}

        public FeatureInTestDTO(Feature feature)
        {
            this.Id = feature.Id;
            this.FeatureName = feature.FeatureName;
        }
    }

}
