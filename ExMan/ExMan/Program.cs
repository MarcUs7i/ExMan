using System.Text;
using ExMan;

Console.OutputEncoding = Encoding.UTF8;
Console.Clear();
Console.CancelKeyPress += new ConsoleCancelEventHandler(SystemProcesses.ExitProgram_OnKill!);

Console.WriteLine("*** ExMan ***");
Console.WriteLine();
Console.WriteLine("Loading...");

void Initializer()
{
    // Combine the current directory with the relative file path
    SystemProcesses.DataDirectory = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.DataDirectory);
    SystemProcesses.BiasFileLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.BiasFileLocation);
    //SystemProcesses.CleanBuildsBiasFileLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.CleanBuildsBiasFileLocation);
    SystemProcesses.CleanBuildsConfigLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.CleanBuildsConfigLocation);
    SystemProcesses.DownloadPDFBiasFileLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.DownloadPDFBiasFileLocation);
    SystemProcesses.DownloadZIPBiasFileLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.DownloadZIPBiasFileLocation);
    SystemProcesses.GetData();

    int keyPress;
    do
    {
        //UI.DisplayMenu();
        //keyPress = UI.GetNumberFromUser(false);
        UI.ReloadMenus();
        UI.mainMenu.Run();
        keyPress = UI.mainMenu.SelectedIndex;
        UI.MenuActions(keyPress);
    } while (keyPress != -1);
}

Initializer();

Console.Clear();
Console.WriteLine();
SystemProcesses.ExitProgram("Exiting ExMan...");