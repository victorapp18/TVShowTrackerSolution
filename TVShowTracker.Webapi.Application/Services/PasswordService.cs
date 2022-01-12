using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace TVShowTracker.Webapi.Application.Services
{
    internal static class PasswordService
    {
        //https://docs.microsoft.com/pt-br/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0
        internal static string GenerateHash(string input, bool isRandom = true) 
        {
            string result = string.Empty;
            byte[] salt = new byte[128 / 8];

            if (isRandom)
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            result = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return result;
        }

        internal static string GeneratePassword() 
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[8];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++) 
                stringChars[i] = chars[random.Next(chars.Length)];

            return new string(stringChars);
        }
    }
}
