namespace Etdb.ServiceBase.DocumentRepository.Abstractions.Context
{
    public class DocumentDbContextOptions
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}