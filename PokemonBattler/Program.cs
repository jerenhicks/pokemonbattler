public class Program
{
    public static void Main()
    {
        Console.WriteLine("Welcome to the Pokemon Battle Simulator!");
        Console.WriteLine("To get started please select your battle mode: (1) Battle All Monsters (2) Choose Your Monsters");
        int battleMode = 1;
        bool selectingBattleMode = true;
        do
        {
            // TODO: this can probably be cleaned up, but works for now...
            var input = Console.ReadLine();
            if (int.TryParse(input, out int mode))
            {
                if (mode == 1 || mode == 2)
                {
                    battleMode = mode;
                    selectingBattleMode = false;
                }
                else
                {
                    Console.WriteLine("Sorry, that was an invalid mode. Please select mode 1 or 2:");
                }
            }
            else
            {
                Console.WriteLine("Sorry, that was an invalid mode. Please select mode 1 or 2:");
            }
        } while (selectingBattleMode);

        Console.WriteLine("Starting battle mode {0}!", battleMode == 1 ? "Battle All Monsters" : "Choose Your Monsters");

        var battleConsole = new BattleConsole(battleMode);

        battleConsole.Run();
    }
}