using KURS.Builders;

namespace KURS.MintBurn;
public class Mint()
{
    public static void mint(int amount, int index, string toAddress)
    {
        string transaction = $"MINT {amount} TO {toAddress}";
        BlockBuilder.CreateBlock(index, "", [transaction], 0, "bloques/chain");
    }
}