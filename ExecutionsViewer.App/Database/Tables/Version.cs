using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExecutionsViewer.App.Database.Tables
{
    public class Version
    {
        public int Id { get; set; }

        // [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public ICollection<Execution> Executions { get; set; }

        public virtual ICollection<MainFeature> MainFeatures { get; set; } = new List<MainFeature>();

        public Version()
        {

        }

        public Version(string versionName)
        {
            this.Name = versionName;
        }
    }
}
