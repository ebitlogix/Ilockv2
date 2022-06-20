// <copyright file="ResponseEnumMessages.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace ILock.Core.AspNetCore.Extensions.Helpers.Enums
{
    /// <summary>
    /// Contains static response messages
    /// </summary>
    public enum ResponseEnumMessages
    {
        /// <summary>
        /// Default success message
        /// </summary>
        [Description("Request processed successfully.")]
        DefaultSuccessMessage = 0,

        /// <summary>
        /// Default error message
        /// </summary>
        [Description("Failure: Unknown error occured.Please try again later.")]
        DefaultErrorMessage = 1,

        /// <summary>
        /// Default Authorization message
        /// </summary>
        [Description("User is not authorized!!")]
        DefaultUserNotAuthorized = 2,

        /// <summary>
        /// Default parameters null message
        /// </summary>
        [Description("Parameters cannot be null!!")]
        DefaultParametersCanNotBeNull = 3,

        /// <summary>
        /// Record found successfull message
        /// </summary>
        [Description("Record found successfully!!")]
        RecordFoundSuccess = 4,

        /// <summary>
        /// Default record not found message
        /// </summary>
        [Description("Record not found!!")]
        RecordNotFound = 5,

        /// <summary>
        /// Default token expire message
        /// </summary>
        [Description("Refresh Token Expired!")]
        RefereshTokenExpired = 5,

        /// <summary>
        /// Invalid email
        /// </summary>
        [Description("Email is Invalid!")]
        InvalidEmail = 6,
    }
}
