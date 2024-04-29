namespace ExMan;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

public class ExManager
{
    public static string ZIPArchiveName = SystemProcesses.BiasDirectory + ".zip";
    public static string ZIPArchiveLocation = Path.Combine(SystemProcesses.CurrentDirectory, ZIPArchiveName);

    public static bool PrepareToExport(string targetDirectory)
    {
        bool ranSuccessfully = CleanSolution(targetDirectory);

        ranSuccessfully = ranSuccessfully && ZIP_Archive(targetDirectory);
        
        return ranSuccessfully;
    }

    public static bool ExtractNewestExerciseFromDownloads()
    {
        
        
        
        return true;
    }

    public static bool OpenNewestExercise()
    {
        string newestExercise = SystemProcesses.GetNewestDirectory();
        bool ranSuccessfully = true;
        
        string[] slnFiles = Directory.GetFiles(newestExercise, "*.sln");
        string[] pdfFiles = Directory.GetFiles(newestExercise, "*.pdf");
        if (slnFiles.Length > 0) Process.Start(slnFiles[0]);
        else
        {
            ranSuccessfully = false;
            Console.WriteLine("No .sln file found in the folder.");
        }
        
        if (pdfFiles.Length > 0) Process.Start(pdfFiles[0]);
        else
        {
            ranSuccessfully = false;
            Console.WriteLine("No .pdf file found in the folder.");
        }
        return ranSuccessfully;
    }
    
    public static bool ZIP_Archive(string targetDirectory)
    {
        try
        {
            // Create the zip archive
            using (var zipArchive = ZipFile.Open(ZIPArchiveLocation, ZipArchiveMode.Create))
            {
                // Add all files and subdirectories from the source folder to the zip archive
                foreach (string fileOrFolder in Directory.EnumerateFileSystemEntries(targetDirectory, "*",
                             SearchOption.AllDirectories))
                {
                    string entryName = Path.GetRelativePath(targetDirectory, fileOrFolder);
                    if (File.Exists(fileOrFolder))
                    {
                        zipArchive.CreateEntryFromFile(fileOrFolder, entryName);
                    }
                    else if (Directory.Exists(fileOrFolder))
                    {
                        // Ensure that directories are represented in the zip archive
                        zipArchive.CreateEntry(entryName + "/");
                    }
                }
            }

            Console.WriteLine("Folder has been added to the zip archive.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error at archiving into a zip: {e}");
            return false;
        }
        return true;
    }

    public static bool CleanSolution(string targetDirectory)
    {
        SystemProcesses.CleanBuildsBiasFileLocation = Path.Combine(targetDirectory, SystemProcesses.CleanBuildsBias);
        return SystemProcesses.RunProcess("powershell.exe", $"-ExecutionPolicy Bypass -File \"{SystemProcesses.CleanBuildsBiasFileLocation}\"", true);
    }
}