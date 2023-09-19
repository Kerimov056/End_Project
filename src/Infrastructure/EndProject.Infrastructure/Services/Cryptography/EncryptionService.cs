using EndProject.Application.Abstraction.Services.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace EndProject.Infrastructure.Services.Cryptography;

public class EncryptionService : IEncryptionService
{
    public async Task<string> Encrypt(string text)
    {
        byte[] data = Encoding.UTF8.GetBytes(text);
        byte[] encryptedData = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
        return Convert.ToBase64String(encryptedData);
    }

    public async Task<string> Decrypt(string encryptedText)
    {
        byte[] encryptedData = Convert.FromBase64String(encryptedText);
        byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(decryptedData);
    }
}
