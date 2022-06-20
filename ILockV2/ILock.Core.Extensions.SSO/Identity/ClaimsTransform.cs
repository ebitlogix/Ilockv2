using ITfoxtec.Identity.Saml2.Claims;
using System.Security.Claims;

namespace ILock.Core.Extensions.AspNetCore.Mvc.Saml2.Identity
{
    /// <summary>
    /// The claims transform.
    /// </summary>
    public static class ClaimsTransform
    {
        /// <summary>
        /// Transforms the.
        /// </summary>
        /// <param name="incomingPrincipal">The incoming principal.</param>
        /// <returns>A ClaimsPrincipal.</returns>
        public static ClaimsPrincipal Transform(ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return incomingPrincipal;
            }

            return CreateClaimsPrincipal(incomingPrincipal);
        }

        /// <summary>
        /// Creates the claims principal.
        /// </summary>
        /// <param name="incomingPrincipal">The incoming principal.</param>
        /// <returns>A ClaimsPrincipal.</returns>
        private static ClaimsPrincipal CreateClaimsPrincipal(ClaimsPrincipal incomingPrincipal)
        {
            var claims = new List<Claim>();

            // All claims
            claims.AddRange(incomingPrincipal.Claims);

            // Or custom claims
            //claims.AddRange(GetSaml2LogoutClaims(incomingPrincipal));
            //claims.Add(new Claim(ClaimTypes.NameIdentifier, GetClaimValue(incomingPrincipal, ClaimTypes.NameIdentifier)));

            return new ClaimsPrincipal(new ClaimsIdentity(claims, incomingPrincipal.Identity.AuthenticationType, ClaimTypes.NameIdentifier, ClaimTypes.Role)
            {
                BootstrapContext = ((ClaimsIdentity)incomingPrincipal.Identity).BootstrapContext
            });
        }

        /// <summary>
        /// Gets the saml2 logout claims.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>A list of Claims.</returns>
        private static IEnumerable<Claim> GetSaml2LogoutClaims(ClaimsPrincipal principal)
        {
            yield return GetClaim(principal, Saml2ClaimTypes.NameId);
            yield return GetClaim(principal, Saml2ClaimTypes.NameIdFormat);
            yield return GetClaim(principal, Saml2ClaimTypes.SessionIndex);
        }

        /// <summary>
        /// Gets the claim.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="claimType">The claim type.</param>
        /// <returns>A Claim.</returns>
        private static Claim GetClaim(ClaimsPrincipal principal, string claimType)
        {
            return ((ClaimsIdentity)principal.Identity).Claims.Where(c => c.Type == claimType).FirstOrDefault();
        }

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="claimType">The claim type.</param>
        /// <returns>A string.</returns>
        private static string GetClaimValue(ClaimsPrincipal principal, string claimType)
        {
            var claim = GetClaim(principal, claimType);
            return claim != null ? claim.Value : null;
        }
    }
}
