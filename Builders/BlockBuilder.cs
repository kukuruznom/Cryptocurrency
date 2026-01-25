using System.Text.Json;
using KURS.Models;
using KURS.Storage;

namespace KURS.Builders
{
    public static class BlockBuilder
    {
    public static Block CreateBlock(int index, string previousHash, string[] transactions, int nonce, string blockPath)
    {
        long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        //guardar un json con las propiedades del bloque en bloques/chain/block_{index}.json
        Block block = new Block
        {
            index = index,
            timestamp = timestamp,
            previousHash = previousHash,
            transactions = transactions,
            nonce = nonce
        };
        BlockStore.SaveBlock(blockPath, block);
        return block;
    }
    }
}   