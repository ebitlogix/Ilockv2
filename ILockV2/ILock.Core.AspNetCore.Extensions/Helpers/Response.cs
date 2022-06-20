// <copyright file="Response.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace ILock.Core.AspNetCore.Extensions.Helpers
{
    /// <summary>
    /// The response.
    /// </summary>
    public class Response<T> : ActionResult
    {
        private readonly ContentResult contentResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class.
        /// </summary>
        public Response()
        {
            this.contentResult = new ContentResult();
        }

        /// <summary>
        /// Gets or sets status
        /// <list type="ResponseStatus">
        /// <item>OK</item>
        /// <item>Info</item>
        /// <item>Error</item>
        /// <item>Warning</item>
        /// <item>LimitExceeded</item>
        /// <item>Forbidden</item>
        /// <item>Unauthorized</item>
        /// </list>
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets data
        /// </summary>
        public T Data { get; set; }
    }
}
