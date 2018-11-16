namespace Etdb.ServiceBase.DocumentRepository.Abstractions
{
    public class DocumentDbContextOptions
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}