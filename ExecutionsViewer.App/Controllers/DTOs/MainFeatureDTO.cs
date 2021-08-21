using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class MainFeatureDTO
    {
        public int Id { get; set; }
        public string FeatureName { get; set; }

        public MainFeatureDTO(MainFeature mainFeature)
        {
            this.Id = mainFeature.Id;
            this.FeatureName = mainFeature.FeatureName;
        }
    }
}
