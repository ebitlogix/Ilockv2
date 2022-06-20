// <copyright file="EnumExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;

namespace ILock.Core.AspNetCore.Extensions.Helpers.Enums
{
    /// <summary>
    /// Implements Enum extension methods
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Get enum description
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>message</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                return null;
            }

            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
