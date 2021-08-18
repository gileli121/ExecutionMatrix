using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.DTOs;

namespace ExecutionsViewer.App.Database.Tables.VirtualTables
{
    public class ChildTest
    {
        [MaxLength(250)] public string TestMethodName { get; set; }

        [MaxLength(250)] public string TestDisplayName { get; set; }

        public List<ChildTest> ChildTests { get; set; }

        public ChildTest()
        {

        }

        public ChildTest(PostExecutionDTO executionDto)
        {
            this.TestMethodName = executionDto.TestMethodName;
            this.TestDisplayName = executionDto.TestDisplayName;
        }


        // public bool IsMatchToExecution(PostExecutionDTO childExecutionDto)
        // {
        //     if (this.TestMethodName != childExecutionDto.TestMethodName)
        //         return false;
        //
        //     if (childExecutionDto.ChildExecutions?.Count > 0)
        //     {
        //         foreach (var postExecutionDto in childExecutionDto.ChildExecutions)
        //         {
        //             if ()
        //         }
        //     }
        // }
    }
}