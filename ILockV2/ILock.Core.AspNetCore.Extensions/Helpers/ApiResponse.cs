// <copyright file="ApiResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ILock.Core.AspNetCore.Extensions.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILock.Core.AspNetCore.Extensions.Helpers
{
    /// <summary>
    /// Implements Controller Extensions.
    /// </summary>
    public static class ApiResponse
    {
        /// <summary>
        /// Result for object.
        /// </summary>
        /// <typeparam name="T">Model which belongs to object</typeparam>
        /// <param name="controller">Controller to attach extension method</param>
        /// <param name="status">Response Status</param>
        /// <param name="data">object</param>
        /// <param name="message">Message</param>
        /// <returns>Action response</returns>
        public static ActionResult Result<T>(this ControllerBase controller, int status, T data = default(T), ResponseEnumMessages message = ResponseEnumMessages.DefaultSuccessMessage)
        {
            return new Response<T>()
            {
                Status = status,
                Data = data,
                Message = message.GetDescription(),
            };
        }

        /// <summary>
        /// Result for type object
        /// </summary>
        /// <param name="controller">Controller to attach extension method</param>
        /// <param name="status">Response Status</param>
        /// <param name="data">type object</param>
        /// <param name="message">Message</param>
        /// <returns>Action response</returns>
        public static IActionResult Result(this ControllerBase controller, int status, object data = null, ResponseEnumMessages message = ResponseEnumMessages.DefaultSuccessMessage)
        {
            if (status == StatusCodes.Status200OK)
            {
                return new Response<object>()
                {
                    Status = status,
                    Data = data,
                    Message = message.GetDescription(),
                };
            }
            else
            {
                return ErrorResult(controller, status, message);
            }
        }

        /// <summary>
        /// Error Result
        /// Make Error response
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="status">Response Status </param>
        /// <param name="message">Error Message </param>
        /// <returns>Result</returns>
        public static IActionResult ErrorResult(this ControllerBase controller, int status, ResponseEnumMessages message = ResponseEnumMessages.DefaultErrorMessage)
        {
            var responseObject = new Response<object>()
            {
                Status = status,
                Data = null,
                Message = message.GetDescription(),
            };

            if (status == StatusCodes.Status500InternalServerError)
            {
                return new ObjectResult(responseObject)
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
            else if (status == StatusCodes.Status400BadRequest)
            {
                return new BadRequestObjectResult(responseObject);
            }
            else if (status == StatusCodes.Status401Unauthorized)
            {
                return new UnauthorizedObjectResult(responseObject);
            }
            else
            {
                return new BadRequestObjectResult(responseObject);
            }
        }
    }
}
