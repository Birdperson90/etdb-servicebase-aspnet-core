using System;
using ETDB.API.ServiceBase.Generics.Base;

namespace ETDB.API.ServiceBase.TestBase.Mocks
{
    public class EntityMock : IEntity
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
