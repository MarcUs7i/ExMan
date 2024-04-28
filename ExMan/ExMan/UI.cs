namespace ExMan;

public class UI
{
    public static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("*** ExMan ***");
        Console.WriteLine();
        
        Console.WriteLine($"[0]      Prepare newest {SystemProcesses.BiasDirectory} to export");
        Console.WriteLine($"[1]      Prepare selected {SystemProcesses.BiasDirectory} to export");
        Console.WriteLine($"[2]      Clean Solution for newest {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"[2]      Clean Solution for selected {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"[4]      Make a zip for newest {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"[5]      Make a zip for selected {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"[6]      Extract newest {SystemProcesses.BiasDirectory} from Downloads");
        Console.WriteLine($"[7]      Display current variables (e.g: BiasDirectory and CleanBuildsBias)");
        Console.WriteLine($"[8]      Change the Directory Bias '{SystemProcesses.BiasDirectory}'");
        Console.WriteLine($"[9]      Change the Clean Solution Bias '{SystemProcesses.CleanBuildsBias}'");
        
        Console.WriteLine();
        Console.WriteLine("========================================================================");
        Console.WriteLine();
        
        Console.WriteLine($"[ESC]      Exit ExMan (Exercise Manager)");
    }
    
    public static void MenuActions(int actionKey)
    {
        switch (actionKey)
        {
            case 0:
                //TODO
                break;
            case 1:
                //TODO
                break;
            case 2:
                //TODO
                break;
            case 3:
                //TODO
                break;
            case 4:
                //TODO
                break;
            case 5:
                //TODO
                break;
            case 6:
                //TODO
                break;
            case 7:
                DisplaySystemData();
                
                Console.WriteLine();
                Console.WriteLine("Press any key to exit menu");
                GetNumberFromUser(true);
                break;
            case 8:
                ChangeBiasUI(true);
                break;
            case 9:
                ChangeBiasUI(false);
                break;
            default:
                return;
        }
    }
    
    public static int GetNumberFromUser(bool useEnterButton)
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D0:
                    return 0;
                case ConsoleKey.D1:
                    return 1;
                case ConsoleKey.D2:
                    return 2;
                case ConsoleKey.D3:
                    return 3;
                case ConsoleKey.D4:
                    return 4;
                case ConsoleKey.D5:
                    return 5;
                case ConsoleKey.D6:
                    return 6;
                case ConsoleKey.D7:
                    return 7;
                case ConsoleKey.D8:
                    return 8;
                case ConsoleKey.D9:
                    return 9;
                case ConsoleKey.NumPad0:
                    return 0;
                case ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.NumPad2:
                    return 2;
                case ConsoleKey.NumPad3:
                    return 3;
                case ConsoleKey.NumPad4:
                    return 4;
                case ConsoleKey.NumPad5:
                    return 5;
                case ConsoleKey.NumPad6:
                    return 6;
                case ConsoleKey.NumPad7:
                    return 7;
                case ConsoleKey.NumPad8:
                    return 8;
                case ConsoleKey.NumPad9:
                    return 9;
                case ConsoleKey.Enter:
                    if (useEnterButton) return 10;
                    break;
                case ConsoleKey.Escape:
                    return 11;
                default:
                    continue;
            }
        }
    }
    

    public static void DisplayDirectories()
    {
        string[] matchingDirectories = SystemProcesses.GetDirectories();
        Console.WriteLine($"Directories containing '{SystemProcesses.BiasDirectory}':");
        foreach (string directory in matchingDirectories)
        { 
            Console.WriteLine(directory);
        }
    }
    
    public static void DisplaySystemData()
    {
        Console.WriteLine($"Current Dir:                 {SystemProcesses.CurrentDirectory}");
        Console.WriteLine($"CleanBuildsBiasFileLocation: {SystemProcesses.CleanBuildsBiasFileLocation}");
        Console.WriteLine($"CleanBuildsBias:             {SystemProcesses.CleanBuildsBias}");
        Console.WriteLine($"DataDirectory:               {SystemProcesses.DataDirectory}");
        Console.WriteLine($"BiasFileLocation:            {SystemProcesses.BiasFileLocation}");
        Console.WriteLine($"BiasDirectory:               {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"CleanBuildConfigName:        {SystemProcesses.CleanBuildConfigName}");
        Console.WriteLine($"CleanBuildsConfigLocation:   {SystemProcesses.CleanBuildsConfigLocation}");
    }

    public static void ChangeBiasUI(bool changeDirectoryBias)
    {
        Console.Clear();
        Console.WriteLine("*** ExMan - Settings ***");
        Console.WriteLine();
        
        if (changeDirectoryBias)
        {
            Console.WriteLine($"Change current bias for the directory (current: {SystemProcesses.BiasDirectory})");
            Console.Write("Enter new directory bias (Enter nothing to exit): ");
            string userInput = Console.ReadLine()!;
            if (userInput.Length < 1)
            {
                return;
            }

            SystemProcesses.BiasDirectory = userInput;
        }
        else
        {
            Console.WriteLine($"Change current bias for Clean Solution Script (current: {SystemProcesses.CleanBuildsBias})");
            Console.Write("Enter new Clean-Solution-Script bias (Enter nothing to exit): ");
            string userInput = Console.ReadLine()!;
            if (userInput.Length < 1)
            {
                return;
            }

            SystemProcesses.CleanBuildsBias = userInput;
        }

        SystemProcesses.SaveData();
    }
}
