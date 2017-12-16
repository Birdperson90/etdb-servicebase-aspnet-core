using System;

namespace Etdb.ServiceBase.Domain.Abstractions.Base
{
    public interface IEntity
    {
        Guid Id
        {
            get;
        }

        byte[] RowVersion
        {
            get;
            set;
        }
    }
}
