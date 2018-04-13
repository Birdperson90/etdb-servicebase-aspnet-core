namespace Etdb.ServiceBase.Cryptography.Abstractions.Hashing
{
    public interface IHasher
    {
        byte[] GenerateSalt();

        string CreateSaltedHash(string unhashed, byte[] salt);
    }
}