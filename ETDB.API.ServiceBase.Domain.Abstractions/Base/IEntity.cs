using System;

namespace ETDB.API.ServiceBase.Domain.Abstractions.Base
{
    public interface IEntity
    {
        Guid Id
        {
            get;
            set;
        }

        byte[] RowVersion
        {
            get;
            set;
        }
    }
}
