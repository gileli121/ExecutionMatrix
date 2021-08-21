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

        [Required]
        public Version FirstVersion { get; set; }

        public int FirstVersionId { get; set; }

        public string FeatureName { get; set; }
        public virtual ICollection<TestClass> TestClasses { get; set; } = new List<TestClass>();

        public MainFeature()
        {

        }

        public MainFeature(string featureName, Version firstVersion)
        {
            this.FeatureName = featureName;
            this.FirstVersion = firstVersion;
        }

    }
}
