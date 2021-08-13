using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Database.Tables;

namespace ExecutionMatrix.App.Controllers.DTOs
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        public string FeatureName { get; set; }

        public FeatureDTO()
        {

        }

        public FeatureDTO(Feature feature)
        {
            this.Id = feature.Id;
            this.FeatureName = feature.FeatureName;
        }
    }
}
