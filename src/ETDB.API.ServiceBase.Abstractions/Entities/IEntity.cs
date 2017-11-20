using System;

namespace ETDB.API.ServiceBase.Abstractions.Entities
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
