using System.Text.Json;
using KURS.Models;
using KURS.Storage;

namespace KURS.Builders
{
    public static class BlockBuilder
    {
    public static Block CreateBlock(string[] transactions, int nonce, string blockPath)
    {
        //obtener el indice del nuevo bloque
        int index = 0;
        if (Directory.Exists(blockPath))
        {
            index = Directory.GetFiles(blockPath, "block_*.json").Length;
        }
        long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        //obtener el hash del bloque anterior
        string previousHash = "0";
        if (index > 0)
        {
            Block? previousBlock = BlockStore.LoadBlock(Path.Combine(blockPath, $"block_{index - 1}.json"));
            if (previousBlock != null)
            {
                previousHash = previousBlock.hash;
            }
        }
        //calcular el hash del nuevo bloque
        string hash = Crypto.BlockHasher.CalculateHash(index, timestamp, previousHash, transactions, nonce);
        string firma = Crypto.BlockSigner.SignBlock("5b123740d619014126bd1263e032545d6eb5b310a374daf45616b37db5ccc29b", hash);
        //guardar un json con las propiedades del bloque en bloques/chain/block_{index}.json
        Block block = new Block
        {
            index = index,
            timestamp = timestamp,
            previousHash = previousHash,
            transactions = transactions,
            hash = hash,
            firma = firma,
            nonce = nonce
        };
        BlockStore.SaveBlock(Path.Combine(blockPath, $"block_{index}.json"), block);
        return block;
    }
    }
}   