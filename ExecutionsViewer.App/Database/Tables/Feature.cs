using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExecutionsViewer.App.Database.Tables
{
    public class Feature
    {

        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public Version FirstVersion { get; set; }
        public int FirstVersionId { get; set; }
        public string FeatureName { get; set; }
        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
        public virtual ICollection<TestClass> TestClasses { get; set; }


        public Feature()
        {
        }

        public Feature(string featureName, Version firstVersion)
        {
            this.FeatureName = featureName;
            this.FirstVersion = firstVersion;
        }

    }
}
