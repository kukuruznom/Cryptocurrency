using KURS.Models;
using KURS.Storage;
using KURS.Crypto;
using KURS.Api;


class Program
{
    static async Task Main(string[] args)
    {
        string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        projectRoot = Path.GetFullPath(Path.Combine(projectRoot, "..", "..", ".."));
        string blockPath = Path.Combine(projectRoot, "blockchain");
        //string blockPath = "blockchain";
        string privateKeyHex = "5b123740d619014126bd1263e032545d6eb5b310a374daf45616b37db5ccc29b";
        string publicKeyHex = "047c06b9d0602b41fdfecc5022feb94201c3d222e61916de617b302840fcaf2cd65c518444c239b17c2374d3fda4652479c1c8612eaefbdc48be766bffe8be7d6b";
        
        Console.WriteLine("Iniciando...");
        ProcessGenesisBlock(blockPath, privateKeyHex, publicKeyHex);
        ProcessAllBlocks(blockPath, publicKeyHex);
        await commandLoop(blockPath);
    }
    static async Task commandLoop(string blockPath)
    {
        await Commands.Loop(blockPath);
    }
    static void ProcessGenesisBlock(string blockPath, string privateKeyHex, string publicKeyHex)
    {
        // Leer bloque
        Block? block = BlockStore.LoadBlock(Path.Combine(blockPath, "block_0.json"));
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
        BlockStore.SaveBlock(Path.Combine(blockPath, "block_0.json"), block);

    }
    static void ProcessAllBlocks(string blockPath, string publicKeyHex)
    {
        //listar todos los archivos json en el directorio
        string[] blockFiles = Directory.GetFiles(blockPath, "*.json");
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
                //verificar firma
                bool isSignatureValid = BlockSigner.VerifySignature(publicKeyHex, block.hash, block.firma);
                if (!isSignatureValid)
                {
                    Console.WriteLine($"Firma invalida en el bloque:{blockFile}");
                    return;
                }
                Console.WriteLine($"Bloque verificado correctamente: {blockFile}");
               
            }
        }
        Console.WriteLine("Comenzando desde el ultimo bloque correcto...");
        return;
    }
}
