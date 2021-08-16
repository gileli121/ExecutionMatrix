using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;

namespace ExecutionsViewer.App.Controllers.DTOs
{
    public class TestClassDTO
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string ClassName { get; set; }
        public string DisplayName { get; set; }

        public TestClassDTO()
        {

        }

        public TestClassDTO(TestClass testClass)
        {
            this.Id = testClass.Id;
            this.PackageName = testClass.PackageName;
            this.ClassName = testClass.ClassName;
            this.DisplayName = testClass.DisplayName;
        }
    }
}
