using System;
using System.Collections.Generic;
using System.Text;

namespace Etdb.ServiceBase.Domain.Abstractions.Base
{
    public abstract class TrackedEntity : Entity
    {
        public DateTime? CreatedAt { get; set; }

        public Guid? CreateUser { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid? UpdateUser { get; set; }
    }
}
