namespace MyStatWebApi.Services;

public interface ISha256Encoder
{
    string ComputeSha256Hash(string rawData);
}