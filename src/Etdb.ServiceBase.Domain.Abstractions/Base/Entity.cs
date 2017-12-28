using System;

namespace Etdb.ServiceBase.Domain.Abstractions.Base
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
