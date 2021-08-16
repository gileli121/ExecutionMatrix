using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionsViewer.App.Database.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using ExecutionsViewer.App.Services.Token;
using ExecutionsViewer.App.Database;

namespace ExecutionsViewer.App.Controllers.Base
{
    public class AppControllerBase<T> : ControllerBase
    {
        protected readonly ILogger<T> log;
        protected readonly ExecutionsViewerDbContext db;
        protected readonly IWebHostEnvironment env;

        protected AppControllerBase
        (
            ExecutionsViewerDbContext db,
            ILogger<T> log,
            IWebHostEnvironment env
        )
        {
            this.log = log;
            this.db = db;
            this.env = env;
        }

    }
}