using System.Text;
using KURS.Utils;

namespace KURS.Crypto
{
    public static class BlockHasher
    {
        /// Calcula el hash SHA256 de un bloque basado en sus propiedade
        public static string CalculateHash(int index, long timestamp, string previousHash, string[] transactions, int nonce)
        {
            string blockData = index.ToString() + timestamp.ToString() + previousHash + string.Join("", transactions) + nonce.ToString();
            
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(blockData);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return Hex.ToHexString(hashBytes);
            }
        }
    }
}
