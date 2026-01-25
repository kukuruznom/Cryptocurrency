using NBitcoin;
using NBitcoin.Crypto;
using KURS.Utils;

namespace KURS.Crypto
{
    public static class BlockSigner
    {

        /// Firma un bloque con una clave privada
        public static string SignBlock(string privateKeyHex, string blockHash)
        {
            byte[] privateKeyBytes = Hex.FromHexString(privateKeyHex);
            var privateKey = new Key(privateKeyBytes);
            
            var signature = privateKey.Sign(uint256.Parse(blockHash));
            return Hex.ToHexString(signature.ToDER());
        }
        /// Verifica la firma de un bloque

        public static bool VerifySignature(string publicKeyHex, string blockHash, string signatureHex)
        {
            try
            {
                byte[] publicKeyBytes = Hex.FromHexString(publicKeyHex);
                var publicKey = new PubKey(publicKeyBytes);
                var signature = ECDSASignature.FromDER(Hex.FromHexString(signatureHex));
                return publicKey.Verify(uint256.Parse(blockHash), signature);
            }
            catch
            {
                return false;
            }
        }
    }
}
