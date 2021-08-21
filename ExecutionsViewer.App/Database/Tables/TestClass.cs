using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;

namespace ExecutionsViewer.App.Database.Tables
{
    public class TestClass
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string PackageName { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string ClassName { get; set; }

        [MaxLength(250)]
        public string DisplayName { get; set; }

        public ICollection<Test> Tests { get; set; } = new List<Test>();

        public virtual ICollection<MainFeature> MainFeatures { get; set; } = new List<MainFeature>();

        public TestClass()
        {

        }

        public TestClass(PostTestClassDTO postTestClassDto)
        {
            this.PackageName = postTestClassDto.PackageName;
            this.ClassName = postTestClassDto.ClassName;
            this.DisplayName = postTestClassDto.DisplayName;
        }
    }
}
