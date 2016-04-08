namespace Tournamentz.Host.Extensions
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;

    public static class IdentityExtensions
    {
        public static Guid GetUserGuid(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return Guid.Empty;
            }

            Claim idClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            Guid id;
            if (idClaim == null || !Guid.TryParse(idClaim.Value, out id))
            {
                return Guid.Empty;
            }

            return id;
        }
    }
}