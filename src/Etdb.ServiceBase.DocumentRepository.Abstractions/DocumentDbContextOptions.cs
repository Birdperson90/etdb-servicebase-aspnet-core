namespace Etdb.ServiceBase.DocumentRepository.Abstractions
{
    public class DocumentDbContextOptions
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
    }
}