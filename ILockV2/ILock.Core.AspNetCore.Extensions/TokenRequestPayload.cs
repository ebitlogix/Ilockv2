namespace ILock.Core.AspNetCore.Extensions
{
    public enum GrantType
    {

        ClientCredentials,
        ResourceOwnerPassword,
        AuthenticationCode,
        Implicit
    }

    public class TokenRequestPayload
    {
        public string GrantType { get; set; }
        public string Scopes { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }

    }
}
