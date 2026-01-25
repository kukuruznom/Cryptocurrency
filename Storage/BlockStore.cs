using System.Text.Json;
using KURS.Models;

namespace KURS.Storage
{
    public static class BlockStore
    {
        /// Lee un bloque desde un archivo JSON
        public static Block? LoadBlock(string blockPath)
        {
            try
            {
                string json = File.ReadAllText(blockPath);
                return JsonSerializer.Deserialize<Block>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el bloque: {ex.Message}");
                return null;
            }
        }
        /// Guarda un bloque en un archivo JSON
        public static void SaveBlock(string blockPath, Block block)
        {
            try
            {
                string json = JsonSerializer.Serialize(block, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(blockPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el bloque: {ex.Message}");
            }
        }
    }
}
