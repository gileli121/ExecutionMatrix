using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExecutionMatrix.App.Database;
using ExecutionMatrix.App.Database.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using ExecutionMatrix.App.Services.Token;

namespace ExecutionMatrix.App.Controllers.Base
{
    public class AppControllerBase<T> : ControllerBase
    {
        protected readonly ILogger<T> log;
        protected readonly ExecutionMatrixDbContext db;
        protected readonly IWebHostEnvironment env;

        protected AppControllerBase
        (
            ExecutionMatrixDbContext db,
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