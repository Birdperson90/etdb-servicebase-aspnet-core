using System;

namespace Etdb.ServiceBase.TestInfrastructure.AutoMapper.DataTransferObjects
{
    public class TodoDto
    {
        public Guid ValueResolvedId { get; set; }
        
        public string Title { get; set; }

        public int Prio { get; set; }

        public bool Done { get; set; }
    }
}