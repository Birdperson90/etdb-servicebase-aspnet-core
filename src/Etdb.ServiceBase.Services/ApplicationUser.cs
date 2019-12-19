using System;
using System.Linq;
using Etdb.ServiceBase.Services.Abstractions;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace Etdb.ServiceBase.Services
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApplicationUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid Id => this.GetUserId();
        public string? UserName => this.GetUserName();

        public string? ImageUrl => this.GetImageUrl();

        private string? GetImageUrl()
        {
            if (!this.IsAuthenticated()) return null;

            var profileClaim = this.httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim =>
                claim.Type == JwtClaimTypes.Profile);

            return profileClaim?.Value;
        }

        private string GetUserName()
        {
            if (!this.IsAuthenticated())
            {
                return string.Empty;
            }

            return this.httpContextAccessor.HttpContext.User.Claims
                .First(claim => claim.Type == JwtClaimTypes.PreferredUserName).Value;
        }

        public bool IsAuthenticated() => this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        private Guid GetUserId()
        {
            if (!this.IsAuthenticated()) return Guid.Empty;

            var idClaim = this.httpContextAccessor.HttpContext.User.Claims
                .First(claim => claim.Type == JwtClaimTypes.Subject);

            return Guid.Parse(idClaim.Value);
        }
    }
}