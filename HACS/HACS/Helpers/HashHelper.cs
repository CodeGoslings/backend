using System.Security.Cryptography;
using System.Text;

namespace HACS.Helpers;

public static class HashHelper
{
    public static string HashPassword(string password)
    {
        var data = Encoding.ASCII.GetBytes(password);
        var sha512data = SHA512.HashData(data);
        return Encoding.ASCII.GetString(sha512data);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }
}