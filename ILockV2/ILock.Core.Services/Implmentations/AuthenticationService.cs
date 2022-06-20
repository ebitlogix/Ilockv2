using ILock.Core.Data;
using ILock.Core.Data.Models;
using ILock.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace ILock.Core.Services.Implmentations
{
    /// <summary>
    /// Implements IAuthenticationService
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JWTService jwtService;
        private readonly AuthDBContext ilockAuthDbContext;
        private readonly TokenSettings tokenSettings;
        private readonly ICryptoService cryptoService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="lockDbContext">The lock db context.</param>
        /// <param name="cryptoService">The crypto service.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public AuthenticationService(IDbContextFactory<AuthDBContext> lockDbContext, ICryptoService cryptoService,
            Configuration configuration, ILoggerFactory loggerFactory)
        {
            this.ilockAuthDbContext = lockDbContext.CreateDbContext();
            this.tokenSettings = configuration.TokenSettings;
            this.jwtService = new JWTService(this.tokenSettings, loggerFactory);
            this.cryptoService = cryptoService;
            this.logger = loggerFactory.CreateLogger<AuthenticationService>();
        }

        /// <summary>
        /// Authenticates the with password.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>An AuthenticationResult.</returns>
        public AuthenticationResult AuthenticateWithPassword(IDictionary<string, StringValues> headers, string ipAddress)
        {
            var (email, password) = ExtractCredentialsFromHeaders(headers);
            var hashedPassword = cryptoService.ComputePassword(email, password);
            if (IsValid(hashedPassword))
            {
                var user = ilockAuthDbContext.Users.Include(u => u.Roles).Include(u => u.Policies).FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
                if (user.InActive)
                {
                    return new AuthenticationResult(false, "Forbid", null, null);
                }

                var authToken = jwtService.CreateAuthToken(user);
                this.SaveTokenToDB(authToken);
                // Save user details here
                this.SaveLoginHistoryToDB(new UserLoginHistory()
                {
                    Email = email,
                    IpAddress = ipAddress,
                    Status = "Success",
                    Date = DateTime.UtcNow,

                });
                return new AuthenticationResult(true, null, authToken, user);
            }
            else
            {
                this.SaveLoginHistoryToDB(new UserLoginHistory()
                {
                    Email = email,
                    IpAddress = ipAddress,
                    Status = "Fail",
                    Date = DateTime.UtcNow,

                });
                return new AuthenticationResult(false, "Forbid", null, null);
            }
        }

        /// <summary>
        /// Authenticates the with s s o.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An AuthenticationResult.</returns>
        public AuthenticationResult AuthenticateWithSSO(int userId)
        {
            var user = ilockAuthDbContext.Users.Include(u => u.Roles).Include(u => u.Policies).FirstOrDefault(u => u.ID == userId);

            var authToken = jwtService.CreateAuthToken(user);
            this.SaveTokenToDB(authToken);

            return new AuthenticationResult(true, null, authToken, user);
        }

        /// <summary>
        /// Extracts the credentials from headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>A (string, string) .</returns>
        private static (string, string) ExtractCredentialsFromHeaders(IDictionary<string, StringValues> headers)
        {
            var authHeader = headers.First(h => h.Key == "Authorization").Value.ToString();
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();
                System.Console.WriteLine(token);
                var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialstring.Split(':');
                return (credentials[0], credentials[1]);
            }
            return default;
        }

        /// <summary>
        /// Saves the token to d b.
        /// </summary>
        /// <param name="authToken">The auth token.</param>
        private void SaveTokenToDB(AuthToken authToken)
        {
            ilockAuthDbContext.AuthTokens.Add(authToken);
            ilockAuthDbContext.SaveChanges();
        }

        /// <summary>
        /// Are the valid.
        /// </summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns>A bool.</returns>
        private bool IsValid(string hashedPassword) => ilockAuthDbContext.Users.Any(u => u.Password == hashedPassword);

        /// <summary>
        /// Save login history to d b.
        /// </summary>
        /// <param name="userLoginHistory">user Login History.</param>
        private void SaveLoginHistoryToDB(UserLoginHistory userLoginHistory)
        {
            ilockAuthDbContext.UserLoginHistories.Add(userLoginHistory);
            ilockAuthDbContext.SaveChanges();
        }
    }
}
