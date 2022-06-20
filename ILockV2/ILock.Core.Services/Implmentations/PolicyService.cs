// <copyright file="PolicyService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>

using ILock.Core.Data;
using ILock.Core.Data.Entities;
using ILock.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Implements IPolicyService Interface
    /// </summary>
    public class PolicyService : BaseEntityService<Policy>, IPolicyService
    {
        private readonly AuthDBContext authDbContext;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyService"/> class.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public PolicyService(AuthDBContext dbContext, ILoggerFactory loggerFactory)
            : base(dbContext)
        {
            authDbContext = dbContext;
            this.logger = loggerFactory.CreateLogger<PolicyService>();
        }
    }
}
