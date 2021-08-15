using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ExecutionMatrix.App.Controllers.Base;
using ExecutionMatrix.App.Controllers.DTOs;
using ExecutionMatrix.App.Database;
using ExecutionMatrix.App.Database.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionMatrix.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : AppControllerBase<TestController>
    {
        public TestController(ExecutionMatrixDbContext db, ILogger<TestController> log, IWebHostEnvironment env) : base(
            db, log, env)
        {
        }


        [HttpGet("GetTestsWithExecutions")]
        public async Task<ActionResult<ICollection<TestSummaryDTO>>> GetExecutions
        (
            [FromQuery] int? versionId,
            [FromQuery] int? testClassId,
            [FromQuery] int? requirementId)
        {
            var executionsQuery = db.Executions
                .Where(e => e.ExecutionId == null)
                .Include(e => e.Version)
                .AsQueryable();

            if (versionId != null)
                executionsQuery = executionsQuery.Where(e => e.VersionId == versionId);

            if (testClassId != null)
                executionsQuery = executionsQuery.Where(e => e.Test.TestClassId == testClassId);

            if (requirementId != null)
                executionsQuery = executionsQuery.Where(e => e.Test.Features.Any(f => f.Id == requirementId));

            var executions = await executionsQuery.ToListAsync();

            var testsWithExecutions = executions
                .AsEnumerable()
                .GroupBy(e => e.TestId)
                .Select(group =>
                    new
                    {
                        TestId = group.Key,
                        Executions = group.OrderByDescending(e => e.ExecutionDate)
                    })
                .OrderByDescending(t => t.TestId);

            var testsWithExecutionsDtoList = new List<TestSummaryDTO>();

            foreach (var testWithExecutions in testsWithExecutions)
            {
                var test = db.Tests
                    .Where(t => t.Id == testWithExecutions.TestId)
                    .Include(t => t.Features)
                    .Include(t => t.TestClass)
                    .First();

                testsWithExecutionsDtoList.Add(new TestSummaryDTO(test, testWithExecutions.Executions.ToList()));
            }

            return Ok(testsWithExecutionsDtoList);
        }


        [HttpGet("GetTestsSummary")]
        public async Task<ActionResult<ICollection<TestSummaryDTO>>> GetTestsSummary
        (
            [FromQuery] int? versionId,
            [FromQuery] int? testClassId,
            [FromQuery] int? featureId)
        {
            var testsQ = db.Tests.Where(t => t.TestOwner == null);

            if (versionId != null)
                testsQ = db.Tests.Where(t => versionId >= t.FirstVersionId);

            if (testClassId != null)
                testsQ = testsQ.Where(t => t.TestClassId == testClassId);

            if (featureId != null)
                testsQ = testsQ
                    .Include(t => t.Features)
                    .Where(t => t.Features.Any(f => f.Id == featureId));

            testsQ = testsQ
                .Include(t => t.Executions).ThenInclude(e => e.Version)
                .Include(t => t.Features)
                .Include(t => t.TestClass);

            var tests = await testsQ.ToListAsync();

            var testsDtos = new List<TestSummaryDTO>();

            if (tests == null)
                return testsDtos;

            if (versionId == null)
            {
                testsDtos.AddRange(tests.Select(test => new TestSummaryDTO(test, test.Executions?.ToList())));
            }
            else
            {
                var testsWithExecutions = new List<TestSummaryDTO>();
                var testsWithoutExecutions = new List<TestSummaryDTO>();

                foreach (var test in tests)
                {
                    List<Execution> matchedExecutions = null;
                    if (test.Executions?.Count > 0)
                        matchedExecutions = test.Executions.Where(e => e.VersionId == versionId).ToList();


                    if (matchedExecutions?.Count > 0)
                        testsWithExecutions.Add(new TestSummaryDTO(test, matchedExecutions));
                    else
                        testsWithoutExecutions.Add(new TestSummaryDTO(test, null));
                }

                testsDtos.AddRange(testsWithExecutions);
                testsDtos.AddRange(testsWithoutExecutions);
            }


            return Ok(testsDtos);
        }
    }
}