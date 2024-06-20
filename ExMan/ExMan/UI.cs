namespace ExMan;

public class UI
{

    public static string[] mainMenuDisplay =
    {
        $"[0]      Prepare newest {SystemProcesses.BiasDirectory} to export",
        $"[1]      Prepare selected {SystemProcesses.BiasDirectory} to export",
        $"[2]      Clean Solution for newest {SystemProcesses.BiasDirectory}",
        $"[3]      Clean Solution for selected {SystemProcesses.BiasDirectory}",
        $"[4]      Make a zip for newest {SystemProcesses.BiasDirectory}",
        $"[5]      Make a zip for selected {SystemProcesses.BiasDirectory}",
        $"[6]      Extract newest {SystemProcesses.BiasDirectory} from Downloads",
        $"[7]      Open newest '{SystemProcesses.BiasDirectory}'",
        $"[8]      Display current variables (e.g: BiasDirectory and CleanBuildsBias)",
        $"[9]      Enter Settings Menu"
        
    };
    
    public static string[] settingsMenuDisplay =
    {
        $"[0]      Change the Directory Bias '{SystemProcesses.BiasDirectory}'",
        $"[1]      Change the Clean Solution Bias '{SystemProcesses.CleanBuildsBias}'",
        $"[2]      Change the Download PDF Bias '{SystemProcesses.DownloadPDFBias}'",
        $"[3]      Change the Download ZIP Bias '{SystemProcesses.DownloadZIPBias}'",
        $"[4]      Change the Default SLN Editor '{SystemProcesses.DefaultSlnEditor}'"
        
    };

    public static string[] mainMenuEndMessage =
    {
        "",
        "========================================================================",
        "",
        $"[ESC]      Exit ExMan (Exercise Manager)"
    };
    
    public static string[] settingsMenuEndMessage =
    {
        "",
        "========================================================================",
        "",
        $"[ESC]      Exit to Menu"
    };

    public static string mainMenuStartMessage = "*** ExMan ***";
    public static string settingsMenuStartMessage = "*** ExMan - Settings ***";
    public static string selectDirectoriesMessage = "Select directory!";
    
    public static Menu mainMenu = new Menu(mainMenuStartMessage, mainMenuDisplay, mainMenuEndMessage, true);
    public static Menu settingsMenu = new Menu(settingsMenuStartMessage, settingsMenuDisplay, settingsMenuEndMessage, true);
    public static Menu selectDirectories = new Menu(selectDirectoriesMessage, new string[3], settingsMenuEndMessage, true);

    public static void ReloadMenus()
    {
        settingsMenuDisplay = new string[] {
            $"[0]      Change the Directory Bias '{SystemProcesses.BiasDirectory}'",
            $"[1]      Change the Clean Solution Bias '{SystemProcesses.CleanBuildsBias}'",
            $"[2]      Change the Download PDF Bias '{SystemProcesses.DownloadPDFBias}'",
            $"[3]      Change the Download ZIP Bias '{SystemProcesses.DownloadZIPBias}'",
            $"[4]      Change the Default SLN Editor '{SystemProcesses.DefaultSlnEditor}'"
        
        };
        
        mainMenuDisplay = new string[] {
            $"[0]      Prepare newest {SystemProcesses.BiasDirectory} to export",
            $"[1]      Prepare selected {SystemProcesses.BiasDirectory} to export",
            $"[2]      Clean Solution for newest {SystemProcesses.BiasDirectory}",
            $"[3]      Clean Solution for selected {SystemProcesses.BiasDirectory}",
            $"[4]      Make a zip for newest {SystemProcesses.BiasDirectory}",
            $"[5]      Make a zip for selected {SystemProcesses.BiasDirectory}",
            $"[6]      Extract newest {SystemProcesses.BiasDirectory} from Downloads",
            $"[7]      Open newest '{SystemProcesses.BiasDirectory}'",
            $"[8]      Display current variables (e.g: BiasDirectory and CleanBuildsBias)",
            $"[9]      Enter Settings Menu"
        };

        string[] dir = SystemProcesses.GetDirectories();
        MakeDirectoryListingReadable(dir);
        
        mainMenu = new Menu(mainMenuStartMessage, mainMenuDisplay, mainMenuEndMessage, true);
        settingsMenu = new Menu(settingsMenuStartMessage, settingsMenuDisplay, settingsMenuEndMessage, true);
        selectDirectories = new Menu(selectDirectoriesMessage, dir, settingsMenuEndMessage, true);
    }
    
    public static void MenuActions(int actionKey)
    {
        bool ranSuccessfully;
        string targetDirectory;
        string readAbleTargetDirectory;
        
        switch (actionKey)
        {
            case 0:
                Console.Clear();
                targetDirectory = SystemProcesses.GetNewestDirectory();
                readAbleTargetDirectory = MakeDirectoryListingReadable(new[] { targetDirectory })[0];
                ranSuccessfully = ExManager.PrepareToExport(targetDirectory);

                if (ranSuccessfully) Console.WriteLine($"{readAbleTargetDirectory} exported successfully!");
                else Console.WriteLine($"Error exporting {SystemProcesses.CleanBuildsBias}");

                MessageToMenu();
                break;

            case 1:
                Console.Clear();
                targetDirectory = SelectDirectories();
                readAbleTargetDirectory = MakeDirectoryListingReadable(new[] { targetDirectory })[0];
                ranSuccessfully = ExManager.PrepareToExport(targetDirectory);
                
                Console.Clear();
                if (ranSuccessfully) Console.WriteLine($"{readAbleTargetDirectory} exported successfully!");
                else Console.WriteLine($"Error exporting {SystemProcesses.CleanBuildsBias}");
                
                MessageToMenu();
                break;
            
            case 2:
                Console.Clear();
                ranSuccessfully = ExManager.CleanSolution(SystemProcesses.GetNewestDirectory());
                
                if (ranSuccessfully) Console.WriteLine($"{SystemProcesses.CleanBuildsBias} ran successfully!");
                else Console.WriteLine($"Error running {SystemProcesses.CleanBuildsBias}");
                
                MessageToMenu();
                break;
            
            case 3:
                Console.Clear();
                ranSuccessfully = ExManager.CleanSolution(SelectDirectories());
                
                Console.Clear();
                if (ranSuccessfully) Console.WriteLine($"{SystemProcesses.CleanBuildsBias} ran successfully!");
                else Console.WriteLine($"Error running {SystemProcesses.CleanBuildsBias}");
                
                MessageToMenu();
                
                break;
            
            case 4:
                Console.Clear();
                targetDirectory = SystemProcesses.GetNewestDirectory();
                readAbleTargetDirectory = MakeDirectoryListingReadable(new[] { targetDirectory })[0];
                ranSuccessfully = ExManager.ZIP_Archive(targetDirectory);
                
                if (ranSuccessfully) Console.WriteLine($"{readAbleTargetDirectory} archived successfully!");
                else Console.WriteLine($"Error archiving {readAbleTargetDirectory}");
                
                MessageToMenu();
                break;
            
            case 5:
                Console.Clear();
                targetDirectory = SelectDirectories();
                readAbleTargetDirectory = MakeDirectoryListingReadable(new[] { targetDirectory })[0];
                
                ranSuccessfully = ExManager.ZIP_Archive(targetDirectory);
                
                Console.Clear();
                if (ranSuccessfully) Console.WriteLine($"{readAbleTargetDirectory} archived successfully!");
                else Console.WriteLine($"Error archiving {readAbleTargetDirectory}");
                MessageToMenu();
                
                break;
            
            case 6:
                Console.Clear();
                ranSuccessfully = ExManager.ExtractNewestExerciseFromDownloads();
                
                if (ranSuccessfully) Console.WriteLine($"Extracting successfully!");
                else Console.WriteLine($"Error extracting");
                
                MessageToMenu();
                break;
            
            case 7:
                Console.Clear();
                ExManager.OpenNewestExercise();
                Console.Clear();
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
        ReloadMenus();
        settingsMenu.Run();
        int keyPress = settingsMenu.SelectedIndex;
        ChangeBiasUI(keyPress);
    }

    public static void MessageToMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to exit to menu");
        Console.ReadKey();
    }

    public static string SelectDirectories()
    {
        selectDirectories.Run();
        int pos = selectDirectories.SelectedIndex;
        if (pos == -1) return String.Empty;
        string[] dirs = SystemProcesses.GetDirectories();
        return dirs[pos];
    }

    public static string[] MakeDirectoryListingReadable(string[] directories)
    {
        List<string> readableDirectories = new List<string>();

        foreach (string directory in directories)
        {
            // Get the directory name from the full path
            string directoryName = Path.GetFileName(directory);

            // Optionally, you can truncate long directory names
            // Here, I'm keeping it simple, but you can adjust as needed
            readableDirectories.Add(directoryName);
        }

        return readableDirectories.ToArray();
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
        Console.WriteLine($"DownloadPDFBias:   {SystemProcesses.DownloadPDFBias}");
        Console.WriteLine($"DownloadPDFBiasFileLocation:   {SystemProcesses.DownloadPDFBiasFileLocation}");
        Console.WriteLine($"DownloadZIPBias:   {SystemProcesses.DownloadZIPBias}");
        Console.WriteLine($"DownloadZIPBiasFileLocation:   {SystemProcesses.DownloadZIPBiasFileLocation}");
        Console.WriteLine($"DefaultSlnEditor:   {SystemProcesses.DefaultSlnEditor}");
        Console.WriteLine($"DefaultSlnEditorConfigLocation:   {SystemProcesses.DefaultSlnEditorConfigLocation}");
        Console.WriteLine($"DownloadDirectory:   {SystemProcesses.DownloadDirectory}");
    }

    public static void ChangeBiasUI(int whatToChange)
    {
        //0 = DirectoryBias
        //1 = CleanSolutionBias
        //2 = DownloadPDFBias
        //3 = DownloadZIPBias
        //4 = DefaultSlnEditor
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
        else if (whatToChange == 4)
        {
            Console.WriteLine($"Change current default SLN editor (current: {SystemProcesses.DefaultSlnEditor})");
            Console.Write("Enter new DownloadZIP bias (Enter nothing to exit): ");
            string userInput = Console.ReadLine()!;
            if (userInput.Length < 1)
            {
                return;
            }

            SystemProcesses.DefaultSlnEditor = userInput;
        }
        else return;

        SystemProcesses.SaveData();
    }
}
