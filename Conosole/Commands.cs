using KURS.Function;

class Commands
{
    public static void Loop(string blockPath)
    {
         Console.WriteLine("Blockchain Shell v1.0. write \"help\" for commands");
                while(true)
        {
            Console.Write("KURS_$> ");
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) continue;
            if (input.ToLower()=="exit")break;
            string[] parts= input.Split(' ');
            string command = parts[0].ToLower();
            try
            {
                switch(command)
                {
                    case "help":
                        Function.Help();
                        break;
                    case "mint":
                        if (parts.Length != 3)
                        {
                            Console.WriteLine("Usage: mint <amount> <toAddress>");
                            break;
                        }
                        int amount = int.TryParse(parts[1], out int parsedAmount) ? parsedAmount : 0;
                        string toAddress = parts[2];
                        Function.Mint(amount, toAddress, blockPath);
                        break;
                    case "burn":
                        if (parts.Length != 3)
                        {
                            Console.WriteLine("Usage: burn <amount> <fromAddress>");
                            break;
                        }
                        int burnAmount = int.TryParse(parts[1], out int parsedBurnAmount) ? parsedBurnAmount : 0;
                        string fromAddress = parts[2];
                        Function.Burn(burnAmount, fromAddress, blockPath);
                        break;
                    case "transfer":
                        if (parts.Length != 4)
                        {
                            Console.WriteLine("Usage: transfer <amount> <fromAddress> <toAddress>");
                            break;
                        }
                        int transferAmount = int.TryParse(parts[1], out int parsedTransferAmount) ? parsedTransferAmount : 0;
                        string transferFromAddress = parts[2];
                        string transferToAddress = parts[3];
                        Function.Transfer(transferAmount, transferFromAddress, transferToAddress, blockPath);
                        break;
                    case "utxo":
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Usage: utxo <address|all>");
                            break;
                        }
                        Function.utxo(parts[1], blockPath);
                        
                        break;
                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                    break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
            
        }
    }
}