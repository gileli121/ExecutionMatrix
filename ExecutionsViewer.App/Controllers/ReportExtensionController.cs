using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers.Base;
using ExecutionsViewer.App.Controllers.DTOs;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Version = ExecutionsViewer.App.Database.Tables.Version;

namespace ExecutionsViewer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportExtensionController : AppControllerBase<ReportExtensionController>
    {
        public ReportExtensionController(ExecutionsViewerDbContext db, ILogger<ReportExtensionController> log,
            IWebHostEnvironment env) : base(
            db, log, env)
        {
        }

        [HttpPost("SubmitExecution")]
        public async Task<ActionResult> PostExecution(PostExecutionDTO body)
        {
            // Create the version if not exists
            var version = await db.Versions
                .Where(v => v.Name == body.TestClass.VersionName)
                .FirstOrDefaultAsync() ?? new Version(body.TestClass.VersionName);


            // Create the test class if not exists
            var testClass = await db.TestClasses.Where(tc =>
                    tc.ClassName == body.TestClass.ClassName && tc.PackageName == body.TestClass.PackageName)
                .FirstOrDefaultAsync() ?? new TestClass(body.TestClass);

            // Create the test if not exists
            Test test = null;
            if (testClass.Id != 0)
                test = await db.Tests.Include(t => t.Features).FirstOrDefaultAsync(
                    t => t.TestClassId == testClass.Id && t.TestMethodName == body.TestMethodName);

            test ??= new Test(body, testClass, version);

            // Create each feature if not exists. For each feature, add the test if the test not found in the feature
            foreach (var featureStr in body.Features)
            {
                var feature = await db.Features
                    .Where(f => f.FeatureName == featureStr)
                    .Include(f => f.Tests)
                    .FirstOrDefaultAsync() ?? new Feature(featureStr, version);


                if (!feature.Tests.Contains(test))
                    feature.Tests.Add(test);

                db.UpdateOrAdd(feature);

            }

            // Create each main feature if not exists and add each one to TestClass & Version entity
            foreach (var mainFeatureStr in body.TestClass.MainFeatures)
            {
                var mainFeature = await db.MainFeatures
                    .Include(mf => mf.Features)
                    .Include(mf => mf.Versions)
                    .Include(mf => mf.TestClasses)
                    .FirstOrDefaultAsync(mf => mf.FeatureName == mainFeatureStr) ?? new MainFeature(mainFeatureStr);

                // Add any feature to the main feature that not included under the main feature
                foreach (var feature in test.Features.Where(feature => !mainFeature.Features.Contains(feature)))
                    mainFeature.Features.Add(feature);


                if (!mainFeature.Versions.Contains(version))
                    mainFeature.Versions.Add(version);


                if (!mainFeature.TestClasses.Contains(testClass))
                    mainFeature.TestClasses.Add(testClass);


                db.UpdateOrAdd(mainFeature);
            }

            db.UpdateOrAdd(version);
            db.UpdateOrAdd(testClass);


            var execution = new Execution(test, body, version);
            db.Executions.Add(execution);

            await db.SaveChangesAsync();

            return Ok();
        }
    }
}