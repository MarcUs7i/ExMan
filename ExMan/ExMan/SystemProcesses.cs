﻿namespace ExMan;
using System.Diagnostics;
using System.IO;
public class SystemProcesses
{
    //variables
    public static string DataDirectory = "ExMan-Data";
    
    public static string BiasDirectory = "Exercise";
    public static string BiasFileLocation = @$"{DataDirectory}\bias.cfg";

    public static string CleanBuildsBias = "CleanSolutionDir.ps1";
    public static string CleanBuildsBiasFileLocation = @$"{CleanBuildsBias}";
    public static string CleanBuildConfigName = "CSD.cfg";
    public static string CleanBuildsConfigLocation = @$"{DataDirectory}\{CleanBuildConfigName}";
    
    // Get the current directory of the application
    public static string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
    
    public static bool RunProcess(string command, string arguments, bool waitForExit)
    {
        try
        {
            // Create a new process start info
            ProcessStartInfo startInfo = new ProcessStartInfo();
            // Set the file name or command to be executed
            startInfo.FileName = command;
            // Optionally, you can pass arguments to the process
            // For example, if your command is "dotnet" and you want to run "myapp.dll", you can set arguments as "myapp.dll"
            startInfo.Arguments = arguments;
            // Specify if you want to use the operating system shell to start the process
            startInfo.UseShellExecute = false;
            // Redirect standard output
            startInfo.RedirectStandardOutput = true;
            // Create the process
            Process process = new Process();
            process.StartInfo = startInfo;
            // Start the process
            process.Start();
            // Optionally, you can wait for the process to exit
            if(waitForExit) process.WaitForExit();
            // Optionally, you can read the standard output
            // string output = process.StandardOutput.ReadToEnd();
            // Optionally, you can handle any errors that occur during process execution
            // Here you can check process.ExitCode to see if the process exited with any error code
            // For example, if process.ExitCode is not equal to 0, you can consider it as an error
            // if (process.ExitCode != 0) { /* Handle error */ /*Console.WriteLine(output);*/ }
            return true; // Return true indicating the process was started successfully
        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur
            // For example, you can log the exception or display an error message
            Console.WriteLine("Error occurred: " + ex.Message);
            return false; // Return false indicating the process failed to start
        }
    }
    
    //Data writers - START
    public static void GetData()
    {
        if(!Directory.Exists(DataDirectory)) CreateData();
        if(!File.Exists(BiasFileLocation)) CreateData();
        if(!File.Exists(CleanBuildsConfigLocation)) CreateData();
        
        try
        {
            string[] dirBias = File.ReadAllLines(BiasFileLocation);
            BiasDirectory = dirBias[0];
            
            string[] cleanBias = File.ReadAllLines(CleanBuildsConfigLocation);
            CleanBuildsBias = cleanBias[0];
        }
        catch (Exception ex)
        { 
            Console.WriteLine("An error occurred while reading the file: " + ex.Message);
        }
    }

    public static void CreateData()
    {
        try
        {
            try
            {
                Directory.CreateDirectory(DataDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //Create DirectoryBias
            // Create a new file using FileStream
            using (FileStream fs = File.Create(BiasFileLocation))
            {
                // You can also use StreamWriter to write content to the file
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    // Write content to the file
                    writer.WriteLine(BiasDirectory);
                }
            }
            
            // Create CleanBuildsBias
            using (FileStream fs = File.Create(CleanBuildsConfigLocation))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(CleanBuildsBias);
                }
            }

            Console.WriteLine("File created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    
    public static bool SaveData()
    {
        try
        {
            File.WriteAllText(BiasFileLocation, BiasDirectory);
            File.WriteAllText(CleanBuildsConfigLocation, CleanBuildsBias);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error at saving: {e}");
            return false;
        }
        return true;
    }
    //Data writers - END
    
    //Directories - START
    //Use for extern code to not address variables from this code manually
    public static string[] GetDirectories()
    {
        string[] matchingDirectories = GetDirectoriesContainingString(CurrentDirectory, BiasDirectory);
        return matchingDirectories;
    }

    public static string GetNewestDirectory()
    {
        string[] directories = GetDirectories();
        return directories[^1];
    }
    
    public static string[] GetDirectoriesContainingString(string rootDirectory, string searchPattern)
    {
        // Search for directories containing the search pattern
        DirectoryInfo rootDirInfo = new DirectoryInfo(rootDirectory);
        DirectoryInfo[] allDirectories = rootDirInfo.GetDirectories("*", SearchOption.AllDirectories);

        // Filter directories whose names contain the search pattern
        var matchingDirectories = allDirectories
                                  .Where(dir => dir.Name.Contains(searchPattern, StringComparison.OrdinalIgnoreCase))
                                  .Select(dir => dir.FullName)
                                  .ToArray();

        return matchingDirectories;
    }
    //Directories - END
    
    //EXIT Man - START
    public static void ExitProgram_OnKill(object sender, ConsoleCancelEventArgs e)
    {
        ExitProgram("CTRL + C => Closing Program...");
    }
    
    public static void ExitProgram(string outputOnExit)
    {
        Console.WriteLine();
        Console.WriteLine(outputOnExit);
        Environment.Exit(0);
    }
    //EXIT Man - END
}
