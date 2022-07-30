using System;
using System.Security.Cryptography;
using System.Text;

namespace OctopusModel
{
    public class Utility
    {
        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            string salt = "Oct@pUsr@nd3mSalt";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}