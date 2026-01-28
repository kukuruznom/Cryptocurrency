using KURS.Builders;

namespace KURS.Functions;
public class Function
{
    public static void Mint(int amount, string toAddress, string blockPath)
    {
        string[] transaction = {amount.ToString(), toAddress, "mint"};
        BlockBuilder.CreateBlock(transaction, 0, blockPath);
    }
    public static void Burn(int amount , string fromAddress, string blockPath)
    {
        string[] transaction = {amount.ToString(), fromAddress, "burn"};
        BlockBuilder.CreateBlock(transaction, 0, blockPath);
    }
    public static void Transfer(int amount , string fromAddress, string toAddress, string blockPath)
    {
        string[] transaction = {amount.ToString(), fromAddress, toAddress, "transfer"};
        BlockBuilder.CreateBlock(transaction, 0, blockPath);
    }

}