public class Program
{

    private static BattleConsole battleConsole;

    public static void Main()
    {
        battleConsole = new BattleConsole();
        bool exitGame = false;
        battleConsole.LoadData();
        do
        {
            InstructionsBlock();
            var input = Console.ReadLine();
            if (int.TryParse(input, out int mode))
            {
                if (mode == 1 || mode == 2)
                {
                    battleConsole.BattleMode = mode;
                    battleConsole.Run();
                }
                else if (mode == 3)
                {
                    battleConsole.OutputBattleLogs = !battleConsole.OutputBattleLogs;
                    Console.WriteLine("WARNING! Battle output logs enabled! This will enable detailed logs of each battle. The file will be roughly 1.5GB in size.");
                }
                else if (mode == 4)
                {
                    exitGame = true;
                }
                else
                {
                    Console.WriteLine("Sorry, that was an invalid mode. ");
                }
            }
            else
            {
                Console.WriteLine("Sorry, that was an invalid mode.");
            }
        } while (!exitGame);
    }

    private static void InstructionsBlock()
    {
        Console.WriteLine("Welcome to the Pokemon Battle Simulator!");
        Console.WriteLine("Battle Output Logs: " + (battleConsole.OutputBattleLogs ? "Enabled" : "Disabled"));
        Console.WriteLine("To get started please select your battle mode: (1) Battle All Monsters (2) Choose Your Monsters (3) To Enable Battle Output Logs or (4) to exit the game.");
    }
}