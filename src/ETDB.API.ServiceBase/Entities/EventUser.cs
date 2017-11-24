using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ETDB.API.ServiceBase.Domain.Abstractions.Base;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace ETDB.API.ServiceBase.Entities
{
    public class EventUser : IEventUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        // TODO: check if this is working
        public string UserName => this.httpContextAccessor
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(claim => claim.Type == JwtClaimTypes.PreferredUserName)?.Value;

        public bool IsAuthenticated()
        {
            return this.httpContextAccessor
                .HttpContext
                .User
                .Identity
                .IsAuthenticated;
        }
    }
}
