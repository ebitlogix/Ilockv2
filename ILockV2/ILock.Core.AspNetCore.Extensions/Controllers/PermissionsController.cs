// <copyright file="PermissionsController.cs" company="PlaceholderCompany">
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
    /// The permissions controller.
    /// </summary>
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService permissionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsController"/> class.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        public PermissionsController(IPermissionService permissions)
        {
            this.permissionService = permissions;
        }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("GetPermissions")]
        [ProducesResponseType(typeof(ApiResponseBase<Permission>), StatusCodes.Status200OK)]

        public IActionResult GetPermissions()
        {
            var res = permissionService.GetAll();
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        [HttpGet]
        [Route("GetPermissionsByRoleId")]
        [ProducesResponseType(typeof(ApiResponseBase<Permission>), StatusCodes.Status200OK)]
        public IActionResult GetPermissionsByRoleId([FromQuery] int id)
        {
            var res = permissionService.GetPermissionsByRoleId(id);
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        /// <summary>
        /// Gets the permissions by role.
        /// </summary>
        /// <param name="rolePermissionAssignmentPayload">The role permission assignment payload.</param>
        /// <returns>A Task.</returns>
        [HttpGet]
        [Route("GetPermissionsByRole")]
        [ProducesResponseType(typeof(ApiResponseBase<Data.Models.RolePermissionAssignmentPayload>), StatusCodes.Status200OK)]

        public async Task<IActionResult> AssignPermissionsWithRole([FromBody] RolePermissionAssignmentPayload rolePermissionAssignmentPayload)
        {
            var res = await permissionService.AssignPermissionsWithRoleId(rolePermissionAssignmentPayload);
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }
    }
}
