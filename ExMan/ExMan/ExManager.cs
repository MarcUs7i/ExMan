namespace ExMan;
using System.IO;
using System.IO.Compression;

public class ExManager
{
    public static string ZIPArchiveName = SystemProcesses.BiasDirectory + ".zip";
    public static string ZIPArchiveLocation = Path.Combine(SystemProcesses.CurrentDirectory, ZIPArchiveName);

    public static bool PrepareToExport(string targetDirectory)
    {
        bool ranSuccessfully = false;

        ranSuccessfully = CleanSolution(targetDirectory);

        ranSuccessfully = ranSuccessfully && ZIP_Pacman(targetDirectory);
        
        return ranSuccessfully;
    }
    
    public static bool ZIP_Pacman(string targetDirectory)
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