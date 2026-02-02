using System.Runtime.Serialization;
using KURS.Builders;
using KURS.Models;
using KURS.Storage;

namespace KURS.Function;
public class Function
{
    public static void Help()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("help - Show this help message");
        Console.WriteLine("mint <amount> <toAddress> - Mint new coins to the specified address");
        Console.WriteLine("burn <amount> <fromAddress> - Burn coins from the specified address");
        Console.WriteLine("transfer <amount> <fromAddress> <toAddress> - Transfer coins from one address to another");
        Console.WriteLine("utxo <address|all> - Show UTXO for the specified address or all addresses");
    }

    public static void Mint(int amount, string toAddress, string blockPath)
    {
        string[] transaction = {amount.ToString(), toAddress, "mint"};
        BlockBuilder.CreateBlock(transaction, 0, blockPath);
    }
    public static void Burn(int amount , string fromAddress, string blockPath)
    {
        if (GetBalance(fromAddress, blockPath) < amount)
        {
            Console.WriteLine("Error: insufficient funds for the burn.");
            return;
        }
        string[] transaction = {amount.ToString(), fromAddress, "burn"};
        BlockBuilder.CreateBlock(transaction, 0, blockPath);
    }
    public static void Transfer(int amount , string fromAddress, string toAddress, string blockPath)
    {
        if (fromAddress == toAddress)
        {
            Console.WriteLine("Error: fromAddress and toAddress cannot be the same.");
            return;
        }
        else if (amount <= 0)
        {
            Console.WriteLine("Error: amount must be greater than zero.");
            return;
        }
        else if (string.IsNullOrWhiteSpace(fromAddress) || string.IsNullOrWhiteSpace(toAddress))
        {
            Console.WriteLine("Error: fromAddress and toAddress cannot be empty.");
            return;
        }
        else if (GetBalance(fromAddress, blockPath) < amount)
        {
            Console.WriteLine("Error: insufficient funds for the transfer.");
            return;
        }
        string[] transaction = {amount.ToString(), fromAddress, toAddress, "transfer"};
        BlockBuilder.CreateBlock(transaction, 0, blockPath);
    }
public static void utxo(string address, string blockPath)
{
    int balance = GetBalance(address, blockPath);
    if (address == "all")
        Console.WriteLine($"Total de monedas en circulación: {balance}");
    else
        Console.WriteLine($"Balance de la dirección {address}: {balance}");
}
public static int GetBalance(string targetAddress, string blockPath)
{
    int balance = 0;
    bool checkAll = targetAddress == "all";
    string[] blockfiles = Directory.GetFiles(blockPath, "*.json");

    foreach (string blockfile in blockfiles)
    {
        Block? block = BlockStore.LoadBlock(blockfile);
        if (block?.transactions == null) continue;
        if (Path.GetFileName(blockfile) == "block_0.json") continue;
        int i = 0;
        while (i < block.transactions.Count())
        {
            string amountStr = block.transactions[i];
            string fromAddr = block.transactions[i + 1];
            string type = block.transactions[i + 2];
            int amount = int.Parse(amountStr);

            if (type == "mint")
            {
                if (checkAll || fromAddr == targetAddress) balance += amount;
                i += 3;
            }
            else if (type == "burn")
            {
                if (checkAll || fromAddr == targetAddress) balance -= amount;
                i += 3;
            }
            else // Es un transfer
            {
                string toAddr = type;
                // El tipo ("transfer") está en i + 3
                if (!checkAll)
                {
                    if (fromAddr == targetAddress) balance -= amount;
                    if (toAddr == targetAddress) balance += amount;
                }
                i += 4;
            }
        }
    }
    return balance;
}
}