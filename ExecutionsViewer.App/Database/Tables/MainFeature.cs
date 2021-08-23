using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExecutionsViewer.App.Database.Tables
{
    public class MainFeature
    {
        public int Id { get; set; }

        // [Required]
        // public Version FirstVersion { get; set; }

        // public int? FirstVersionId { get; set; }

        public string FeatureName { get; set; }
        public virtual ICollection<TestClass> TestClasses { get; set; } = new List<TestClass>();
        public virtual ICollection<Version> Versions { get; set; } = new List<Version>();
        public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();


        public MainFeature()
        {

        }

        public MainFeature(string featureName)
        {
            this.FeatureName = featureName;
        }


    }
}
