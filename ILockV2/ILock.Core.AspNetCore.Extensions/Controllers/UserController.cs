// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.AspNetCore.Extensions.Helpers;
using ILock.Core.Data.Models;
using ILock.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILock.Core.AspNetCore.Extensions.Controllers
{
    /// <summary>
    /// The user controller.
    /// </summary>
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UserController(IUserService user)
        {
            this.userService = user;
        }

        [HttpGet]
        [Route("GetUsers")]
        [ProducesResponseType(typeof(ApiResponseBase<List<Data.Entities.User>>), StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var res = userService.GetAll();
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("GetUsers")]
        [ProducesResponseType(typeof(ApiResponseBase<Data.Entities.User>), StatusCodes.Status200OK)]
        public IActionResult GetUserByID([FromQuery] int id)
        {
            var res = userService.GetById(id);
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);

        }

        /// <summary>
        /// Gets the roles by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("GetRolesByUserID")]
        [ProducesResponseType(typeof(ApiResponseBase<Data.Entities.Role>), StatusCodes.Status200OK)]

        public IActionResult GetRolesByUserID([FromQuery] int userId)
        {
            var res = userService.GetUsersRoles(userId);
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="addUserPayload">The add user payload.</param>
        /// <returns>An IActionResult.</returns>
        [HttpGet]
        [Route("AddUser")]
        [ProducesResponseType(typeof(ApiResponseBase<Data.Entities.User>), StatusCodes.Status200OK)]
        public IActionResult AddUser(Data.Entities.User addUserPayload)
        {
            var res = userService.Add(addUserPayload);
            return this.Result(StatusCodes.Status200OK, res, Helpers.Enums.ResponseEnumMessages.DefaultSuccessMessage);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="updateUserPayload">The update user payload.</param>
        [HttpGet]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(ApiResponseBase<Data.Entities.User>), StatusCodes.Status200OK)]

        public void UpdateUser(Data.Entities.User updateUserPayload)
        {
            userService.UpdateAsync(updateUserPayload);
        }
    }
}
