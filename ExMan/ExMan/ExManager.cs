namespace ExMan;
using System.IO;
using System.IO.Compression;

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
        try
        {
            string newestDirectory = SystemProcesses.MakeNewestDirectory();
        
            // Get the newest PDF file with the prefix and move it to the newest directory
            string[] pdfFiles = Directory.GetFiles(SystemProcesses.DownloadDirectory,
                $"{SystemProcesses.DownloadPDFBias}*.pdf");
            if (pdfFiles.Length > 0)
            {
                File.Move(pdfFiles[0], Path.Combine(newestDirectory, Path.GetFileName(pdfFiles[0])));
            }
            else
            {
                Console.WriteLine("No PDF file found in the downloads directory.");
                return false;
            }

            // Get the corresponding ZIP archive and move it to the parent directory of the newest directory
            string[] zipFiles = Directory.GetFiles(SystemProcesses.DownloadDirectory,
                $"{SystemProcesses.DownloadZIPBias}*.zip");
            if (zipFiles.Length > 0)
            {
                File.Move(zipFiles[0], Path.Combine(Directory.GetParent(newestDirectory)!.FullName, Path.GetFileName(zipFiles[0])));
            }
            else
            {
                Console.WriteLine("No ZIP file found in the downloads directory.");
                return false;
            }

            // Extract the ZIP archive to the newest directory
            zipFiles = Directory.GetFiles(Directory.GetParent(newestDirectory)!.FullName, $"{SystemProcesses.DownloadZIPBias}*.zip");
            ZipFile.ExtractToDirectory(zipFiles[0], newestDirectory);
            File.Delete(zipFiles[0]);
            
            //if there is a folder that contains everything, then move everything to the parent
            string[] readAbleZipArray = UI.MakeDirectoryListingReadable(zipFiles);
            string readAbleZip = readAbleZipArray[0];
            string directoryCheck = "";
            for (int i = 0; i < readAbleZip.Length; i++)
            {
                if (readAbleZip[i] == '.') break;
                directoryCheck += readAbleZip[i];
            }

            string fullDirectoryCheck = Path.Combine(newestDirectory, directoryCheck);
            if (Directory.Exists(fullDirectoryCheck))
            {
                try
                {
                    // Get all files and folders in the child directory
                    string[] files = Directory.GetFiles(fullDirectoryCheck);
                    string[] directories = Directory.GetDirectories(fullDirectoryCheck);

                    // Move files to the parent directory
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(newestDirectory, fileName);
                        File.Move(file, destFile);
                    }

                    // Move subdirectories to the parent directory
                    foreach (string directory in directories)
                    {
                        string directoryName = new DirectoryInfo(directory).Name;
                        string destDir = Path.Combine(newestDirectory, directoryName);
                        Directory.Move(directory, destDir);
                    }

                    // Optionally, you can delete the empty child directory after moving its contents
                    Directory.Delete(fullDirectoryCheck);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }

        return true;
    }


    public static bool OpenNewestExercise()
    {
        string newestExercise = SystemProcesses.GetNewestDirectory();
        bool ranSuccessfully = true;
        
        string[] slnFiles = Directory.GetFiles(newestExercise, "*.sln");
        string[] pdfFiles = Directory.GetFiles(newestExercise, "*.pdf");
        if (slnFiles.Length > 0) SystemProcesses.RunProcess("powershell.exe", $"Start-Process '{SystemProcesses.DefaultSlnEditor}' -FilePath '{slnFiles[0]}'", true);
        else
        {
            ranSuccessfully = false;
            Console.WriteLine("No .sln file found in the folder.");
        }
        
        if (pdfFiles.Length > 0) SystemProcesses.RunProcess("powershell.exe", $"Start-Process msedge -FilePath '{pdfFiles[0]}'", true);
        else
        {
            ranSuccessfully = false;
            Console.WriteLine("No .pdf file found in the folder.");
        }
        return ranSuccessfully;
    }
    
    public static bool ZIP_Archive(string targetDirectory)
    {
        if (targetDirectory == String.Empty)
        {
            return false;
        }
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
        if (targetDirectory == String.Empty)
        {
            return false;
        }
        SystemProcesses.CleanBuildsBiasFileLocation = Path.Combine(targetDirectory, SystemProcesses.CleanBuildsBias);
        return SystemProcesses.RunProcess("powershell.exe", $"-ExecutionPolicy Bypass -File \"{SystemProcesses.CleanBuildsBiasFileLocation}\"", true);
    }
}