using KURS.Models;
using KURS.Storage;
using KURS.Crypto;
using KURS.Builders;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando...");
        ProcessGenesisBlock();
        ProcessAllBlocks();
    }

    static void ProcessGenesisBlock()
    {
        string GenPath = "C:/Users/Kukuruznom/Desktop/Programas/KURS/bloques/chain/block_0.json";
        string privateKeyHex = "5b123740d619014126bd1263e032545d6eb5b310a374daf45616b37db5ccc29b";
        string publicKeyHex = "047c06b9d0602b41fdfecc5022feb94201c3d222e61916de617b302840fcaf2cd65c518444c239b17c2374d3fda4652479c1c8612eaefbdc48be766bffe8be7d6b";
        
        // Leer bloque
        Block? block = BlockStore.LoadBlock(GenPath);
        if (block == null)
        {
            Console.WriteLine("Error al leer el bloque genesis");
            return;
        }

        // Calcular hash
        block.hash = BlockHasher.CalculateHash(block.index, block.timestamp, block.previousHash, block.transactions, block.nonce);

        // Firmar bloque
        block.firma = BlockSigner.SignBlock(privateKeyHex, block.hash);

        // Verificar firma
        bool isSignatureValid = BlockSigner.VerifySignature(publicKeyHex, block.hash, block.firma);
        Console.WriteLine($"Firma válida: {isSignatureValid}");

        // Guardar bloque
        BlockStore.SaveBlock(GenPath, block);

    }
    static void ProcessAllBlocks()
    {
        long Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        int index = 0;
        string PreviusHash = "";
        string blocksDirectory = "C:/Users/Kukuruznom/Desktop/Programas/KURS/bloques/chain/";
        string publicKeyHex = "047c06b9d0602b41fdfecc5022feb94201c3d222e61916de617b302840fcaf2cd65c518444c239b17c2374d3fda4652479c1c8612eaefbdc48be766bffe8be7d6b";
        //listar todos los archivos json en el directorio
        string[] blockFiles = Directory.GetFiles(blocksDirectory, "*.json");
        foreach (string blockFile in blockFiles)
        {
            Block? block = BlockStore.LoadBlock(blockFile);
            if (block == null)
            {
                Console.WriteLine($"Error al leer el bloque: {blockFile}");
                continue;
            }
            //verificar si el bloque tiene firma
            if (!string.IsNullOrEmpty(block.firma))
            {
                string calculatedHash = BlockHasher.CalculateHash(block.index, block.timestamp, block.previousHash, block.transactions,
                block.nonce);
                if (block.hash != calculatedHash)
                {
                Console.WriteLine($"Hash inválido en el bloque: {blockFile}");
                return;
                }
                PreviusHash = block.hash;
                //verificar firma
                bool isSignatureValid = BlockSigner.VerifySignature(publicKeyHex, block.hash, block.firma);
                if (!isSignatureValid)
                {
                    Console.WriteLine($"Firma invalida en el bloque:{blockFile}");
                    return;
                }
                Console.WriteLine($"Bloque verificado correctamente: {blockFile}");
                index++;

            }
        }
        Console.WriteLine("Comenzando desde el ultimo bloque correcto...");
    }
    static void CreateNewBlock(int index, long timestamp, string PreviusHash, string[] transactions, int nonce)
    {
        Block newBlock = BlockBuilder.CreateBlock(index, timestamp, PreviusHash, transactions, nonce);
        Console.WriteLine($"Nuevo bloque creado: Índice {newBlock.index}, Timestamp {newBlock.timestamp}");
    }
}