namespace ETDB.API.ServiceBase.General.Abstractions.Hasher
{
    public interface IHasher
    {
        byte[] GenerateSalt();

        string CreateSaltedHash(string unhashed, byte[] salt);
    }
}
