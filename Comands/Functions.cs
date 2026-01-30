using System.Data.Common;
using System.Reflection;
using System.Runtime.Serialization;
using KURS.Builders;
using KURS.Models;
using KURS.Storage;

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
    public static void utxo(string Addres, string blockPath)
    {
        if (Addres == "all")
        {
            string[] blockfiles = Directory.GetFiles(blockPath, "*.json");
            foreach (string blockfile in blockfiles)
            {
                Block? block = BlockStore.LoadBlock(blockfile);
                if (block == null)
                {
                    Console.WriteLine($"Error al leer el bloque {blockfile}");
                    continue;
                }
                int transactioncount = block.transactions.Count();
                // sumar y restar los utxo
                if (transactioncount >2)
                {
                    for (int i=0; i < transactioncount ; i+=3)
                    {
                        string amount = block.transactions[i];
                        string addres = block.transactions[i+1];
                        string type = "";
                        string toAddres = "";
                        if (block.transactions[i+2]=="mint" || block.transactions[i+2]=="burn")
                        {
                            type = block.transactions[i+2];
                        }
                        else if (block.transactions[i+3]=="transfer")
                        {
                            toAddres = block.transactions[i+2];
                            type = block.transactions[i+3];
                            i++; // por que transfer tiene un campo mas
                        }
                    }
                }            
            }
        }
        else
        {
            string[] blockfiles = Directory.GetFiles(blockPath, "*.json");
            int balance = 0;
            foreach (string blockfile in blockfiles)
            {
                Block? block = BlockStore.LoadBlock(blockfile);
                if (block == null)
                {
                    Console.WriteLine($"Error al leer el bloque {blockfile}");
                    continue;
                }
                int transactioncount = block.transactions.Count();
                // sumar y restar los utxo
                if (transactioncount >2)
                {
                    for (int i=0; i < transactioncount ; i+=3)
                    {
                        string amount = block.transactions[i];
                        string addres = block.transactions[i+1];
                        string type = "";
                        string toAddres = "";
                        if (block.transactions[i+2]=="mint")
                        {
                            type = block.transactions[i+2];
                            if (addres == Addres)
                            {
                                balance += int.Parse(amount);
                            }
                        }
                        else if(block.transactions[i+2]=="burn")
                        {
                            if (addres == Addres)
                            {
                                balance -= int.Parse(amount);
                            }
                        }
                        else if (block.transactions[i+3]=="transfer")
                        {
                            toAddres = block.transactions[i+2];
                            if (addres == toAddres)
                            {
                                balance += int.Parse(amount);
                            }
                            else if (addres == Addres)
                            {
                                balance -= int.Parse(amount);
                            }
                            i++; // por que transfer tiene un campo mas
                        }
                    }
                }           
            }
            Console.WriteLine($"Balance de la direccion {Addres} es: {balance}"); 
        }
    }

}