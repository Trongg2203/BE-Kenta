using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace KenTaShop.Services
{
    public class PasswordHasher
    {
        public byte[] GetRandom(int leng)
        {
            byte[] ramdom = new byte[leng];

            using (var ng = RandomNumberGenerator.Create())
            {
                ng.GetBytes(ramdom);// tao so ngau nhien tùy thuộc vào leng
            }
            return ramdom;
        }
        public string GetRandomPassword()
        {
            int len = 6;
            var charecter = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=<>?";
            char[] pas = new char[len];
            using (var rng = new RNGCryptoServiceProvider()) //tao 1 class de random
            {
                for (int i = 0; i < len; i++)
                {
                    byte[] bytes = new byte[1]; //tao 1 doi tuong chua 1 du lieu
                    rng.GetBytes(bytes); //random 1 so nao do
                    pas[i] = charecter[bytes[0] % charecter.Length]; //truyen du lieu for vo pas =char va lay gtri trong byte
                }
            }
            string passhash = new string(pas);
            return passhash;
        }
        public string HashPassword(string password) // abc@132
        {
            var salt = GetRandom(16);

            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 32
                ));
            string res = Convert.ToBase64String(salt) + "|" + passwordHash;
            return res;
        }
        public bool verifyPassword(string password, string savePassHash)
        {
            string[] pass = savePassHash.Split('|');
            byte[] salt = Convert.FromBase64String(pass[0]);
            string passhash = pass[1];

            string passhashinput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
                ));
            return passhash == passhashinput;
        }
    }
}
