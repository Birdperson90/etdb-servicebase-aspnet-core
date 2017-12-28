using System;
using System.Linq;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using IdentityModel;
using Microsoft.AspNetCore.Http;

namespace Etdb.ServiceBase.EventSourcing.Base
{
    public class EventUser : IEventUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string UserName => this.httpContextAccessor
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(claim => claim.Type == JwtClaimTypes.PreferredUserName)?.Value;

        public Guid UserId
        {
            get
            {
                var stringId = this.httpContextAccessor
                    .HttpContext
                    .User
                    .Claims
                    .FirstOrDefault(claim => claim.Type == JwtClaimTypes.Subject)?.Value;

                return Guid.TryParse(stringId, out var result) ? result : Guid.Empty;
            }
        }

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
