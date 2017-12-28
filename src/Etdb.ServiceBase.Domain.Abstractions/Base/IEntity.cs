using System;

namespace Etdb.ServiceBase.Domain.Abstractions.Base
{
    public interface IEntity
    {
        string Id
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
