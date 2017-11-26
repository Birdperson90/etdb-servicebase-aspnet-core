using System;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Base
{
    public class Entity : IEntity
    {
        public Guid Id { get; protected set; }
        public byte[] RowVersion { get; set; }
    }
}
