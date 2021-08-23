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
using Microsoft.AspNetCore.Razor.TagHelpers;
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

            var version = await db.Versions
                .Where(v => v.Id == versionId)
                .Include(v => v.MainFeatures)
                .ThenInclude(mf => mf.TestClasses)
                .ThenInclude(tc => tc.Tests)
                .ThenInclude(t => t.Executions)
                .FirstOrDefaultAsync();

            if (version == null)
                return NotFound("versionId not found");

            foreach (var mainFeature in version.MainFeatures)
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

                        test.Executions = test.Executions.OrderByDescending(e => e.Id).ToList();

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


        [HttpGet("GetMainFeatureVersionStatistics")]
        public async Task<ActionResult<ICollection<MainFeatureVersionStatisticsDTO>>> GetMainFeatureVersionStatistics(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            var output = new List<MainFeatureVersionStatisticsDTO>();

            var mainFeatures = await db.MainFeatures
                .Include(mf => mf.TestClasses).ThenInclude(tc => tc.Tests)
                .Include(mf => mf.Versions)
                .ToListAsync();

            var versions = await db.Versions
                .Include(v => v.Executions)
                .OrderByDescending(v => v.Id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();


            foreach (var mainFeature in mainFeatures)
            {
                var statistics = new List<StatisticsInVersionDTO>();
                foreach (var version in versions)
                {
                    var totalTests = 0;
                    var totalExecutions = 0;
                    var totalPassedExecutions = 0;
                    foreach (var testClass in mainFeature.TestClasses.Where(
                        tc => mainFeature.Versions.Contains(version)))
                    {
                        foreach (var test in testClass.Tests)
                        {
                            if (version.Id < test.FirstVersionId)
                                continue;

                            totalTests++;

                            var lastExecution = version.Executions
                                .Where(e => e.TestId == test.Id)
                                .OrderByDescending(e => e.Id)
                                .FirstOrDefault();

                            if (lastExecution == null)
                                continue;

                            totalExecutions++;

                            if (lastExecution.ExecutionResult == ExecutionResult.Passed)
                                totalPassedExecutions++;
                        }
                    }

                    statistics.Add(new StatisticsInVersionDTO(version, totalTests, totalExecutions,
                        totalPassedExecutions));
                }

                output.Add(new MainFeatureVersionStatisticsDTO(mainFeature, statistics));
            }

            return output;
        }
    }
}