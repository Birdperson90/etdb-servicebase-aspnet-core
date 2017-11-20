namespace ETDB.API.ServiceBase.Abstractions.Hasher
{
    public interface IHasher
    {
        byte[] GenerateSalt();

        string CreateSaltedHash(string unhashed, byte[] salt);
    }
}
