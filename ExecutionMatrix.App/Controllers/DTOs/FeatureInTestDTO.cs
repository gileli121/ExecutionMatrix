using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Database.Tables;

namespace ExecutionMatrix.App.Controllers.DTOs
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
