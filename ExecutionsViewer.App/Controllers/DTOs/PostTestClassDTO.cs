using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class PostTestClassDTO
    {
        [MaxLength(250)]
        public string PackageName { get; set; }

        [MaxLength(100)]
        [Required]
        public string ClassName { get; set; }

        [MaxLength(250)]
        public string DisplayName { get; set; }

    }
}
