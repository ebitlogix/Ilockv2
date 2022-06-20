// <copyright file="RolesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.AspNetCore.Extensions.Helpers;
using ILock.Core.Data.Entities;
using ILock.Core.Data.Models;
using ILock.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILock.Core.AspNetCore.Extensions.Controllers
{
    /// <summary>
    /// The roles controller.
    /// </summary>
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="policy">The policy.</param>
        public RolesController(IRoleService role)
        {
            this.roleService = role;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("GetRoles")]
        [ProducesResponseType(typeof(ApiResponseBase<List<Role>>), 200)]

        public IActionResult GetRoles()
        {
            var res = roleService.GetAll();
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        /// <summary>
        /// Gets the roles by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An IActionResult.</returns>

        [HttpGet]
        [Route("GetRolesById")]
        [ProducesResponseType(typeof(ApiResponseBase<Role>), 200)]
        public IActionResult GetRolesById([FromQuery] int id)
        {
            var res = roleService.GetById(id);
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

    }
}
