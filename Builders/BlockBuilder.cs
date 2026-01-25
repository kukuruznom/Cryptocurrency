using System.Text.Json;
using KURS.Models;

namespace KURS.Builders
{
    public static class BlockBuilder
    {
    public static Block CreateBlock(int index, long timestamp, string previousHash, string[] transactions, int nonce)
    {
        //guardar un json con las propiedades del bloque en bloques/chain/block_{index}.json
        Block block = new Block
        {
            index = index,
            timestamp = timestamp,
            previousHash = previousHash,
            transactions = transactions,
            nonce = nonce
        };
        string blockPath = $"C:/Users/Kukuruznom/Desktop/Programas/KURS/bloques/chain/block_{index}.json";
        string json = JsonSerializer.Serialize(block, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(blockPath, json);
        return block;
    }
    }
}   