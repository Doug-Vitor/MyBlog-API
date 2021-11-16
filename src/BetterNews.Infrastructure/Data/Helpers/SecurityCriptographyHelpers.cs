using System.Security.Cryptography;
using System.Text;

public static class SecurityCriptographyHelpers
{
    public static string ToHash(this string source)
    {
        using HashAlgorithm algorithm = SHA256.Create();
        byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
        return BitConverter.ToString(algorithm.ComputeHash(sourceBytes));
    }
}
