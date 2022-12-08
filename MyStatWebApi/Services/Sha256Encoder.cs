using System.Security.Cryptography;
using System.Text;

namespace MyStatWebApi.Services;

public class Sha256Encoder : ISha256Encoder
{
    public string ComputeSha256Hash(string rawData)
    {
        using var sha256Hash = SHA256.Create();

        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        return Encoding.Default.GetString(bytes);
    }
}