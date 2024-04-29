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
        Console.WriteLine($"[3]      Clean Solution for selected {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"[4]      Make a zip for newest {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"[5]      Make a zip for selected {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"[6]      Extract newest {SystemProcesses.BiasDirectory} from Downloads");
        Console.WriteLine($"[7]      Open newest '{SystemProcesses.BiasDirectory}'");
        Console.WriteLine($"[8]      Display current variables (e.g: BiasDirectory and CleanBuildsBias)");
        Console.WriteLine($"[9]      Enter Settings Menu");
        
        Console.WriteLine();
        Console.WriteLine("========================================================================");
        Console.WriteLine();
        
        Console.WriteLine($"[ESC]      Exit ExMan (Exercise Manager)");
    }
    
    public static void MenuActions(int actionKey)
    {
        bool ranSuccessfully;
        switch (actionKey)
        {
            case 0:
                ExManager.PrepareToExport(SystemProcesses.GetNewestDirectory());
                break;
            
            case 1:
                //TODO: make select directory
                ExManager.PrepareToExport(SystemProcesses.GetNewestDirectory());
                break;
            
            case 2:
                Console.Clear();
                ranSuccessfully = ExManager.CleanSolution(SystemProcesses.GetNewestDirectory());
                
                if (ranSuccessfully) Console.WriteLine($"{SystemProcesses.CleanBuildsBias} ran successfully!");
                else Console.WriteLine($"Error running {SystemProcesses.CleanBuildsBias}");
                
                MessageToMenu();
                break;
            
            case 3:
                //TODO: make select directory
                
                Console.Clear();
                ranSuccessfully = ExManager.CleanSolution(SystemProcesses.GetNewestDirectory());
                
                if (ranSuccessfully) Console.WriteLine($"{SystemProcesses.CleanBuildsBias} ran successfully!");
                else Console.WriteLine($"Error running {SystemProcesses.CleanBuildsBias}");
                
                MessageToMenu();
                
                break;
            
            case 4:
                Console.Clear();
                ExManager.ZIP_Archive(SystemProcesses.GetNewestDirectory());
                MessageToMenu();
                break;
            
            case 5:
                //TODO: make select directory
                Console.Clear();
                ExManager.ZIP_Archive(SystemProcesses.GetNewestDirectory());
                MessageToMenu();
                
                break;
            
            case 6:
                ExManager.ExtractNewestExerciseFromDownloads();
                break;
            
            case 7:
                ExManager.OpenNewestExercise();
                Console.WriteLine("Opening...");
                MessageToMenu();
                break;
            case 8:
                DisplaySystemData();
                
                MessageToMenu();
                break;
            case 9:
                SettingsMenu();
                break;
            default:
                return;
        }
    }

    public static void SettingsMenu()
    {
        Console.Clear();
        Console.WriteLine("*** ExMan - Settings ***");
        Console.WriteLine();
        
        Console.WriteLine($"[0]      Change the Directory Bias '{SystemProcesses.BiasDirectory}'");
        Console.WriteLine($"[1]      Change the Clean Solution Bias '{SystemProcesses.CleanBuildsBias}'");
        Console.WriteLine($"[2]      Change the Download PDF Bias '{SystemProcesses.DownloadPDFBias}'");
        Console.WriteLine($"[3]      Change the Download ZIP Bias '{SystemProcesses.DownloadZIPBias}'");
        
        Console.WriteLine();
        Console.WriteLine("========================================================================");
        Console.WriteLine();
        
        Console.WriteLine($"[ESC]      Exit to Menu");

        int keyPress = GetNumberFromUser(false);
        ChangeBiasUI(keyPress);
    }

    public static void MessageToMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to exit to menu");
        GetNumberFromUser(true);
    }
    
    public static int GetNumberFromUser(bool useEnterButton)
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D0 or ConsoleKey.NumPad0:
                    return 0;
                case ConsoleKey.D1 or ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.D2 or ConsoleKey.NumPad2:
                    return 2;
                case ConsoleKey.D3 or ConsoleKey.NumPad3:
                    return 3;
                case ConsoleKey.D4 or ConsoleKey.NumPad4:
                    return 4;
                case ConsoleKey.D5 or ConsoleKey.NumPad5:
                    return 5;
                case ConsoleKey.D6 or ConsoleKey.NumPad6:
                    return 6;
                case ConsoleKey.D7 or ConsoleKey.NumPad7:
                    return 7;
                case ConsoleKey.D8 or ConsoleKey.NumPad8:
                    return 8;
                case ConsoleKey.D9 or ConsoleKey.NumPad9:
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

    public static void SelectDirectories()
    {
        
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
        Console.Clear();
        Console.WriteLine($"Current Dir:                 {SystemProcesses.CurrentDirectory}");
        Console.WriteLine($"CleanBuildsBiasFileLocation: {SystemProcesses.CleanBuildsBiasFileLocation}");
        Console.WriteLine($"CleanBuildsBias:             {SystemProcesses.CleanBuildsBias}");
        Console.WriteLine($"DataDirectory:               {SystemProcesses.DataDirectory}");
        Console.WriteLine($"BiasFileLocation:            {SystemProcesses.BiasFileLocation}");
        Console.WriteLine($"BiasDirectory:               {SystemProcesses.BiasDirectory}");
        Console.WriteLine($"CleanBuildConfigName:        {SystemProcesses.CleanBuildConfigName}");
        Console.WriteLine($"CleanBuildsConfigLocation:   {SystemProcesses.CleanBuildsConfigLocation}");
    }

    public static void ChangeBiasUI(int whatToChange)
    {
        //0 = DirectoryBias
        //1 = CleanSolutionBias
        //2 = DownloadPDFBias
        //3 = DownloadZIPBias
        Console.Clear();
        Console.WriteLine("*** ExMan - Settings ***");
        Console.WriteLine();
        
        if (whatToChange == 0)
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
        else if (whatToChange == 1)
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
        else if (whatToChange == 2)
        {
            Console.WriteLine($"Change current bias for downloaded PDF (current: {SystemProcesses.DownloadPDFBias})");
            Console.Write("Enter new DownloadPDF bias (Enter nothing to exit): ");
            string userInput = Console.ReadLine()!;
            if (userInput.Length < 1)
            {
                return;
            }

            SystemProcesses.DownloadPDFBias = userInput;
        }
        else if (whatToChange == 3)
        {
            Console.WriteLine($"Change current bias for downloaded ZIP (current: {SystemProcesses.DownloadZIPBias})");
            Console.Write("Enter new DownloadZIP bias (Enter nothing to exit): ");
            string userInput = Console.ReadLine()!;
            if (userInput.Length < 1)
            {
                return;
            }

            SystemProcesses.DownloadZIPBias = userInput;
        }
        else return;

        SystemProcesses.SaveData();
    }
}
