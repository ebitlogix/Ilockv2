using System.Security.Authentication;
using ILock.Core.Data.Models;
using ILock.Core.Extensions.AspNetCore.Mvc.Saml2.Identity;
using ILock.Core.Extensions.AspNetCore.Mvc.Saml2.Models;
using ILock.Core.Services.Abstractions;
using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.MvcCore;
using ITfoxtec.Identity.Saml2.Schemas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ILock.Core.Extensions.AspNetCore.Mvc.Saml2.Controllers
{
    /// <summary>
    /// The sso controller.
    /// </summary>
    [AllowAnonymous]
    [Route("SSO")]
    public class SSOController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ISSOService ssoService;
        private readonly IAuthenticationService authenticationService;
        const string relayStateReturnUrl = "ReturnUrl";
        private readonly Saml2Configuration config;
        private readonly ILogger<SSOController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SSOController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configAccessor">The config accessor.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="sSOService">The s s o service.</param>
        /// <param name="authenticationService">The authentication service.</param>
        public SSOController(ILogger<SSOController> logger, IOptions<Saml2Configuration> configAccessor, IUserService userService, ISSOService sSOService, IAuthenticationService authenticationService)
        {
            config = configAccessor.Value;
            this.logger = logger;
            this.userService = userService;
            this.ssoService = sSOService;
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Logins the.
        /// </summary>
        /// <param name="returnUrl">The return url.</param>
        /// <returns>An IActionResult.</returns>
        [Route("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            var binding = new Saml2RedirectBinding();
            binding.SetRelayStateQuery(new Dictionary<string, string> { { relayStateReturnUrl, returnUrl ?? Url.Content("~/") } });
            return binding.Bind(new Saml2AuthnRequest(config)
            {
                Subject = new Subject { NameID = new NameID { ID = "Email" } },
                NameIdPolicy = new NameIdPolicy { AllowCreate = true, Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:persistent" },
            }).ToActionResult();
        }

        /// <summary>
        /// Assertions the consumer service.
        /// </summary>
        /// <returns>A Task.</returns>
        [Route("AssertionConsumerService")]
        public async Task<IActionResult> AssertionConsumerService()
        {
            var binding = new Saml2PostBinding();
            var saml2AuthnResponse = new Saml2AuthnResponse(config);
            binding.ReadSamlResponse(Request.ToGenericHttpRequest(), saml2AuthnResponse);
            if (saml2AuthnResponse.Status != Saml2StatusCodes.Success)
            {
                throw new AuthenticationException($"SAML Response status: {saml2AuthnResponse.Status}");
            }
            string emailAddress = saml2AuthnResponse.NameId.Value;
            string sessionIndex = saml2AuthnResponse.SessionIndex;
            string token = Guid.NewGuid().ToString();
            // TODO: Find Internal User based on email address
            var userId = this.userService.GetUserIdByEmailAddress(emailAddress);

            if (userId == null)
            {
                throw new AuthenticationException($"SAML Response status: User not found");
            }
            else
            {

                // TODO: Genrate Token(Not JWT Token) AuthToken[ID,UserID,TokenType=SSO,SessionIndex,IssuedAt,Expirey]
                var currentDateTime = DateTime.UtcNow;
                ssoService.Add(new SSOAuthToken()
                {
                    ID = token,
                    SessionIndex = sessionIndex,
                    UserID = userId ?? 0,
                    IssuedAt = currentDateTime,
                    ExpiryDate = currentDateTime.AddMinutes(10),
                });
                ssoService.Commit();
                binding.Unbind(Request.ToGenericHttpRequest(), saml2AuthnResponse);
                await saml2AuthnResponse.CreateSession(HttpContext, claimsTransform: (claimsPrincipal) => ClaimsTransform.Transform(claimsPrincipal));
                var relayStateQuery = binding.GetRelayStateQuery();
                var returnUrl = relayStateQuery.ContainsKey(relayStateReturnUrl) ? relayStateQuery[relayStateReturnUrl] : Url.Content("~/");
                returnUrl += $"/{token}";
                return Redirect(returnUrl);
            }
        }

        /// <summary>
        /// Logouts the.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Content("~/"));
            }
            var binding = new Saml2PostBinding();
            var saml2LogoutRequest = await new Saml2LogoutRequest(config, User).DeleteSession(HttpContext);
            return binding.Bind(saml2LogoutRequest).ToActionResult();
        }

        /// <summary>
        /// Loggeds the out.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [Route("LoggedOut")]
        public IActionResult LoggedOut()
        {
            var binding = new Saml2PostBinding();
            binding.Unbind(Request.ToGenericHttpRequest(), new Saml2LogoutResponse(config));
            return Redirect(Url.Content("~/"));
        }

        /// <summary>
        /// Singles the logout.
        /// </summary>
        /// <returns>A Task.</returns>
        [Route("SingleLogout")]
        public async Task<IActionResult> SingleLogout()
        {
            Saml2StatusCodes status;
            var requestBinding = new Saml2PostBinding();
            var logoutRequest = new Saml2LogoutRequest(config, User);
            try
            {
                requestBinding.Unbind(Request.ToGenericHttpRequest(), logoutRequest);
                status = Saml2StatusCodes.Success;
                await logoutRequest.DeleteSession(HttpContext);
            }
            catch (Exception exc)
            {
                logger.LogError(exc.Message, exc);
                status = Saml2StatusCodes.RequestDenied;
            }
            var responsebinding = new Saml2PostBinding();
            responsebinding.RelayState = requestBinding.RelayState;
            var saml2LogoutResponse = new Saml2LogoutResponse(config)
            {
                InResponseToAsString = logoutRequest.IdAsString,
                Status = status,
            };
            return responsebinding.Bind(saml2LogoutResponse).ToActionResult();
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="ssoToken">The sso token.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost]
        [Route("ValidateToken")]
        public IActionResult ValidateToken([FromBody] SSOToken ssoToken)
        {
            var token = ssoService.GetById(ssoToken.Token);

            if (token != null && token.ExpiryDate > DateTime.UtcNow)
            {
                var authenticationResult = authenticationService.AuthenticateWithSSO(token.UserID);

                if (authenticationResult.Success)
                {
                    return Ok(authenticationResult);
                }

            }

            return Unauthorized(new Data.AuthenticationResult(
                false,
                "Unauthorized",
                null,
                null
            ));
        }
    }
}