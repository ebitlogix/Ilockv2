using ILock.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILock.Core.AspNetCore.Extensions.Controllers
{
    /// <summary>
    /// The auth controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authenticateService;
        private readonly ISSOService sSOService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        /// <param name="sSOService">The s s o service.</param>
        public AuthController(IAuthenticationService authenticationService, ISSOService sSOService)
        {
            this.authenticateService = authenticationService;
            this.sSOService = sSOService;
        }

        /// <summary>
        /// Generates Auth Token
        /// </summary>
        /// <param name="tokenRequestPayload">The token request payload.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        [Route("Token")]
        public IActionResult Token([FromBody] TokenRequestPayload tokenRequestPayload)
        {
            var authenticationResult = authenticateService.AuthenticateWithPassword(HttpContext.Request.Headers, HttpContext.Connection.RemoteIpAddress.ToString());

            if (authenticationResult.Success)
            {
                return Ok(authenticationResult);
            }

            return Unauthorized(authenticationResult);
        }
    }
}
