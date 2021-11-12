namespace PollosHermano.MicroFramework.Tools.Security
{
    public interface ICryptSecurity
    {
        string EncryptDecrypt(string cipherText, EncryptionMode mode);
    }
}