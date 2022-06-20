// <copyright file="JWTService.cs" company="Retail Insight Ltd">
// Copyright (c) Retail Insight Ltd. All rights reserved.
// </copyright>
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ILock.Core.Data;
using ILock.Core.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ILock.Core.Services.Implmentations
{
    internal sealed class JWTService
    {
        private readonly TokenSettings tokenSettings;
        private readonly ILogger logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="JWTService"/> class.
        /// </summary>
        /// <param name="tokenSettings">The token settings.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public JWTService(TokenSettings tokenSettings, ILoggerFactory loggerFactory)
        {
            this.tokenSettings = tokenSettings;
            this.logger = loggerFactory.CreateLogger<JWTService>();
        }

        /// <summary>
        /// Gets the j w t auth key.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="expiry">The expiry.</param>
        /// <param name="guid">The guid.</param>
        /// <param name="issuedAt">The issued at.</param>
        /// <returns>A string.</returns>
        internal string GetJWTAuthKey(Data.Entities.User user, DateTime expiry, string guid, DateTime issuedAt)
        {
            var securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key));

            var credentials = new SigningCredentials(securtityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Sid, guid),
                new Claim("IssuedAt", issuedAt.ToString())
            };

            if ((user.Roles.Count) > 0)
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                expires: expiry,
                signingCredentials: credentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// Creates the auth token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>An AuthToken.</returns>
        public AuthToken CreateAuthToken(User user)
        {
            var issuedAt = DateTime.UtcNow;
            var expiry = DateTime.UtcNow.AddMinutes(tokenSettings.ExpiresIn);
            var guid = Guid.NewGuid().ToString();
            return new AuthToken
            {
                Type = TokenType.Bearer,
                ID = guid,
                IssuedAt = issuedAt,
                ExpiryDate = expiry,
                Token = GetJWTAuthKey(user, expiry, guid, issuedAt)
            };
        }
    }
}
