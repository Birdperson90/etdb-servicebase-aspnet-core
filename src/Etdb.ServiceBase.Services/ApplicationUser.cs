using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Guid Id => GetUserId();
        public string UserName => GetUserName();

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
            if (!this.IsAuthenticated())
            {
                return Guid.Empty;
            }

            var idClaimValue = this.httpContextAccessor.HttpContext.User.Claims
                .First(claim => claim.Type == JwtClaimTypes.Subject).Value;

            return Guid.Parse(idClaimValue);
        }
    }
}