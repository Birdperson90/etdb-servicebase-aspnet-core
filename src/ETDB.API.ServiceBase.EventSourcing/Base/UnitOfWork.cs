using System;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Base;
using ETDB.API.ServiceBase.EventSourcing.Abstractions.Commands;
using ETDB.API.ServiceBase.EventSourcing.ContextBase;

namespace ETDB.API.ServiceBase.EventSourcing.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppContextBase context;

        public UnitOfWork(AppContextBase context)
        {
            this.context = context;
        }

        public SourcingCommandResponse Commit()
        {
            var rowsAffected = this.context.SaveChanges();
            return new SourcingCommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}
