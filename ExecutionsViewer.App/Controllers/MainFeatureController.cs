using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.Base;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionsViewer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainFeatureController : AppControllerBase<MainFeatureController>
    {
        public MainFeatureController(ExecutionsViewerDbContext db, ILogger<MainFeatureController> log,
            IWebHostEnvironment env) : base(db, log, env)
        {
        }


        [HttpGet("GetMainFeatures")]
        public async Task<ActionResult<ICollection<MainFeatureDTO>>> GetMainFeatures()
        {
            var mainFeatures = await db.MainFeatures.ToListAsync();
            var mainFeatureDtos = mainFeatures.Select(mainFeature => new MainFeatureDTO(mainFeature)).ToList();

            return Ok(mainFeatureDtos);
        }


        [HttpGet("GetMainFeaturesSummary")]
        public async Task<ActionResult<ICollection<MainFeatureSummaryDTO>>> GetMainFeaturesSummary(
            [FromQuery] int versionId)
        {
            var output = new List<MainFeatureSummaryDTO>();

            var mainFeatures = await db.MainFeatures
                .Where(mf => versionId >= mf.FirstVersionId)
                .Include(mf => mf.TestClasses)
                .ThenInclude(tc => tc.Tests)
                .ThenInclude(t => t.Executions)
                .ToListAsync();


            foreach (var mainFeature in mainFeatures)
            {
                var totalTestClasses = 0;
                var totalTests = 0;
                var totalExecutions = 0;
                var totalPassedExecutions = 0;
                foreach (var testClass in mainFeature.TestClasses)
                {
                    foreach (var test in testClass.Tests)
                    {
                        if (versionId < test.FirstVersionId)
                            continue;

                        totalTests++;

                        var lastExecution = test.Executions.FirstOrDefault(t => t.VersionId == versionId);
                        if (lastExecution == null)
                            continue;

                        totalExecutions++;
                        if (lastExecution.ExecutionResult == ExecutionResult.Passed)
                            totalPassedExecutions++;
                    }
                }

                output.Add(new MainFeatureSummaryDTO(mainFeature, totalTests, totalTestClasses, totalExecutions,
                    totalPassedExecutions));
            }

            return Ok(output);
        }
    }
}