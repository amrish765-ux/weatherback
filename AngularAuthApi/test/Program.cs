using System.Security.Cryptography;

namespace test
{
    internal class Program
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static int SaltSize = 16;
        private static int HashSize = 20;
        private static int Iterations = 10000;
        static void Main(string[] args)
        {
            Console.WriteLine(VerifyPassword("Amrish@12","mM2nmdK0N78VDeFPGOEVvW9Yl0jC3qFpFgy4JNSReSJ1+rL9"));
        }
        public static bool VerifyPassword(string password, string base64Hash)
        {
            var hashBytes = Convert.FromBase64String(base64Hash);
            Console.WriteLine(hashBytes);
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            Console.WriteLine(key);
            byte[] hash = key.GetBytes(HashSize);
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i] != hash[i])
                {
                    Console.WriteLine(hash[i]+"fffffffffff");
                    Console.WriteLine(hashBytes[i + SaltSize] + "gggggggggg");
                    return false;
                }
            }
            return true;
        }
    }
}