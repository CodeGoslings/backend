using System.Security.Cryptography;
using System.Text;

namespace HACS.Helpers.DonorManagement;

public static class HashHelper
{
    public static string HashPassword(string password)
    {
        var data = Encoding.ASCII.GetBytes(password);
        var sha512 = SHA512.Create();
        var sha512data = sha512.ComputeHash(data);
        return Encoding.ASCII.GetString(sha512data);
    }
    
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }
}