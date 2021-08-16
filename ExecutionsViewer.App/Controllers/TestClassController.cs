using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;
using ExecutionsViewer.App.Controllers.Base;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionsViewer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestClassController : AppControllerBase<TestClassController>
    {
        public TestClassController(ExecutionsViewerDbContext db, ILogger<TestClassController> log,
            IWebHostEnvironment env) : base(db, log, env)
        {
        }


        [HttpGet("GetTestClasses")]
        public async Task<ActionResult<ICollection<TestClassDTO>>> GetTestClasses()
        {
            var testClasses = await db.TestClasses.ToListAsync();

            var result = testClasses.Select(testClass => new TestClassDTO(testClass)).ToList();

            return Ok(result);
        }


        [HttpGet("GetTestClass")]
        public async Task<ActionResult<TestClassDTO>> GetTestClass([FromQuery] int classId)
        {
            var testClass = await db.TestClasses.FindAsync(classId);

            if (testClass == null)
                return NotFound();

            return Ok(new TestClassDTO(testClass));
        }


        [HttpGet("GetTestClassesSummary")]
        public async Task<ActionResult<ICollection<TestClassSummaryDTO>>> GetTestClassesSummary(
            [FromQuery] int? versionId)
        {
            var testClasses = await db.TestClasses
                .Include(tc => tc.Tests)
                .ToListAsync();

            var executionsQ = db.Executions.AsQueryable();

            if (versionId != null)
                executionsQ = executionsQ.Where(e => e.VersionId == versionId);

            var executions = await executionsQ.Where(e => e.ExecutionId == null)
                .OrderByDescending(e => e.ExecutionDate)
                .ToListAsync();

            var result = new List<TestClassSummaryDTO>();
            foreach (var testClass in testClasses)
            {
                var totalTests = 0;
                var totalExecutedTests = 0;
                var totalPassedTests = 0;
                foreach (var test in testClass.Tests)
                {
                    totalTests++;

                    var lastExecution = executions.FirstOrDefault(e => e.TestId == test.Id);

                    if (lastExecution == null)
                        continue;

                    if (lastExecution.ExecutionResult != ExecutionResult.Skipped &&
                        lastExecution.ExecutionResult != ExecutionResult.UnExecuted)
                        totalExecutedTests++;

                    if (lastExecution.ExecutionResult == ExecutionResult.Passed)
                        totalPassedTests++;
                }

                var classSummary = new TestClassSummaryDTO(testClass, totalTests, totalExecutedTests, totalPassedTests);
                result.Add(classSummary);
            }

            return Ok(result);
        }
    }
}