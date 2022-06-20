// <copyright file="PolicyController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ILock.Core.AspNetCore.Extensions.Controllers
{
    /// <summary>
    /// The policy controller.
    /// </summary>
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService policyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyController"/> class.
        /// </summary>
        /// <param name="policy">The policy.</param>
        public PolicyController(IPolicyService policy)
        {
            this.policyService = policy;
        }
    }
}
