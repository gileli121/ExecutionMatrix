using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExecutionMatrix.App.Database.Tables
{
    public class Feature
    {

        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string FeatureName { get; set; }
        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

        public virtual ICollection<TestClass> TestClasses { get; set; }

        public Feature()
        {
        }

        public Feature(string featureName)
        {
            this.FeatureName = featureName;
        }

    }
}
