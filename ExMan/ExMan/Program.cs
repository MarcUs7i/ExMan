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
    SystemProcesses.GetData();
    // Combine the current directory with the relative file path
    SystemProcesses.DataDirectory = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.DataDirectory);
    SystemProcesses.BiasFileLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.BiasFileLocation);
    //SystemProcesses.CleanBuildsBiasFileLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.CleanBuildsBiasFileLocation);
    SystemProcesses.CleanBuildsConfigLocation = Path.Combine(SystemProcesses.CurrentDirectory, SystemProcesses.CleanBuildsConfigLocation);

    int keyPress;
    do
    {
        UI.DisplayMenu();
        keyPress = UI.GetNumberFromUser(false);
        UI.MenuActions(keyPress);
    } while (keyPress != -1);
}

Initializer();
/*SystemProcesses.GetData();
UI.DisplaySystemData();

SystemProcesses.GetDirectories();*/

SystemProcesses.ExitProgram("Exiting ExMan...");