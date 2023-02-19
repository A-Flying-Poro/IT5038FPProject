// ###########
// Change this namespace to something else
// ###########

namespace IT5038FPProject.concise;

static class Program
{
    // Setup resource locations
    private static readonly DirectoryInfo CurrentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
    private static readonly DirectoryInfo ProjectDirectory = CurrentDirectory.Parent!.Parent!.Parent!;
    private static readonly string FolderPathImages = Path.Combine(ProjectDirectory.FullName, "images");
    private static readonly string FolderPathText = Path.Combine(ProjectDirectory.FullName, "text");
    private static readonly string FileAbnormal = Path.Combine(ProjectDirectory.FullName, "abnormal.txt");
    
    private static Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>()
    {
        {".jpg", "image/jpeg"},
        {".txt", "text/plain"},
        {".png", "image/png"},
    };
    private static readonly List<string> AbnormalTextList = FetchAbnormalTexts();

    public static void Main(string[] args)
    {
        // Set up the main menu prompt so we don't have to recreate every loop
        List<string> mainPrompt = new List<string>()
        {
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@",
            "@@ Please select an option. @@",
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@",
            "1. View all files in Image folder",
            "2. View all files in Text folder",
            "3. View all files",
            "4. Perform sorting of files into image and text folders",
            "5. Perform check on abnormal content in text file",
            "6. Exit",
        };
        
        // Set this flag to true to exit program at the end of loop
        bool exit = false;
            
        do
        {
            string inputMainMenu = GetUserChoice(mainPrompt, new List<string>() {"1", "2", "3", "4", "5", "6"});
            switch (inputMainMenu)
            {
                case "6": // Exit
                {
                    exit = true;
                    Console.WriteLine("Goodbye!");
                    break;
                }
                case "1": // Image folder
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(FolderPathImages);
                    ShowFilesDialog(directoryInfo.GetFiles().ToList());
                    break;
                }
                case "2": // Text folder
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(FolderPathText);
                    ShowFilesDialog(directoryInfo.GetFiles().ToList());
                    break;
                }
                case "3": // Image + text folder
                {
                    DirectoryInfo directoryInfoImages = new DirectoryInfo(FolderPathImages);
                    DirectoryInfo directoryInfoText = new DirectoryInfo(FolderPathText);
                    List<FileInfo> files = new List<FileInfo>();
                    // Adds both image and text files into the same list
                    files.AddRange(directoryInfoImages.GetFiles());
                    files.AddRange(directoryInfoText.GetFiles());
                    ShowFilesDialog(files);
                    break;
                }
                case "5": // Abnormal
                {
                    DirectoryInfo directoryInfoText = new DirectoryInfo(FolderPathText);
                    List<FileInfo> abnormalFiles = new List<FileInfo>();

                    // Loop through all files
                    foreach (FileInfo fileInfo in directoryInfoText.GetFiles())
                    {
                        // Read through the contents in the file
                        string fileContent = File.ReadAllText(fileInfo.FullName);
                        // Loop through the abnormal list
                        foreach (string abnormalText in AbnormalTextList)
                        {
                            // If the current file contains the current abnormal text
                            if (fileContent.Contains(abnormalText))
                            {
                                abnormalFiles.Add(fileInfo);
                                break; // Skip the rest of the current loop, since we already found a match
                            }
                        }
                    }
                    
                    ShowFilesDialog(abnormalFiles);
                    
                    break;
                }
                case "4": // Sorting
                {
                    DirectoryInfo directoryInfoImages = new DirectoryInfo(FolderPathImages);
                    DirectoryInfo directoryInfoText = new DirectoryInfo(FolderPathText);

                    // Loop through all files in Images folder
                    foreach (FileInfo fileInfo in directoryInfoImages.GetFiles())
                    {
                        // Checking the MIME type for the file
                        string fileExtension = fileInfo.Extension;
                        if (!MIMETypesDictionary.ContainsKey(fileExtension))
                        {
                            Console.WriteLine($"Unrecognised file type {fileExtension} for {fileInfo.Name}");
                            continue;
                        }

                        // Checking if the MIME type includes the text "text"
                        string fileType = MIMETypesDictionary[fileExtension];
                        if (fileType.Contains("text"))
                        {
                            string originalPath = fileInfo.FullName;
                            string destinationPath = Path.Combine(directoryInfoText.FullName, fileInfo.Name);
                            // Move to Text folder
                            fileInfo.MoveTo(destinationPath);
                            Console.WriteLine($"Moved {originalPath} to {destinationPath}.");
                        }
                    }
                    // Loop through all files in Text folder
                    foreach (FileInfo fileInfo in directoryInfoText.GetFiles())
                    {
                        // Checking the MIME type for the file
                        string fileExtension = fileInfo.Extension;
                        if (!MIMETypesDictionary.ContainsKey(fileExtension))
                        {
                            Console.WriteLine($"Unrecognised file type {fileExtension} for {fileInfo.Name}");
                            continue;
                        }

                        // Checking if the MIME type includes the text "image"
                        string fileType = MIMETypesDictionary[fileExtension];
                        if (fileType.Contains("image"))
                        {
                            string originalPath = fileInfo.FullName;
                            string destinationPath = Path.Combine(directoryInfoImages.FullName, fileInfo.Name);
                            // Move to Image folder
                            fileInfo.MoveTo(destinationPath);
                            Console.WriteLine($"Moved {originalPath} to {destinationPath}.");
                        }
                    }
                    
                    Console.WriteLine("Successfully sorted files.");
                    
                    break;
                }
            }
        } while (!exit);
    }

    
    
    private static List<string> FetchAbnormalTexts()
    {
        string[] abnormalFileLines = File.ReadAllLines(FileAbnormal);
        return abnormalFileLines
            // Filters out empty lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToList();
    }

    private static string GetUserChoice(List<string> prompt, List<string> validChoices)
    {
        while (true) // Repeat asking user
        {
            foreach (string line in prompt)
            {
                Console.WriteLine(line);
            }
            
            string? userChoice = Console.ReadLine();

            // Make sure user entered something before processing it
            while (string.IsNullOrWhiteSpace(userChoice))
            {
                Console.WriteLine("Please enter your choice:");
                userChoice = Console.ReadLine();
            }

            // Check if the user's choice is within the valid choices
            if (validChoices.Contains(userChoice.ToLowerInvariant()))
            {
                // Since this is valid, we can exit the loop and function
                return userChoice;
            }
            
            Console.WriteLine("Please enter a valid choice."); // Restart the loop and ask the user again
        }
    }

    private static void ShowFilesDialog(List<FileInfo> files)
    {
        Console.WriteLine("  No                                              Location of file                                             ||       Type of file    ");
        Console.WriteLine(" ====   ====================================================================================================   ||   ====================");

        int index = 1;
        foreach (FileInfo fileInfo in files)
        {
            // https://dzone.com/articles/c-string-format-examples
            Console.WriteLine($"  {index++,2}    {fileInfo.FullName, -100}   ||    {MIMETypesDictionary[fileInfo.Extension], -18}");
        }
        Console.WriteLine("Completed scan.");

        
        
        
        // File deletion
        
        string inputDelete = GetUserChoice(new List<string>() {"Do you want to delete file(s)? (y/n)"}, new List<string>() {"y", "n"});
        if (inputDelete.Equals("n"))
        {
            // Since user does not want to delete files, we can skip the rest of the function
            return;
        }
        
        Console.Write("Select file(s) to delete by typing file number(s) separated by space: 1 3 8");
        string? inputDeleteFiles = Console.ReadLine()?.Trim(); // Trim the start and end of any whitespace
        if (string.IsNullOrWhiteSpace(inputDeleteFiles))
        {
            Console.WriteLine("No input given, skipping delete.");
            return;
        }

        // List to hold all the files we want to delete
        List<FileInfo> deleteList = new List<FileInfo>();
        
        // Split the user input by space ' ', so we get individual file numbers
        foreach (string fileNumberString in inputDeleteFiles.Split(' '))
        {
            // Try to convert the current user input from string to integer
            if (!int.TryParse(fileNumberString, out int fileNumber))
            {
                Console.WriteLine($"Provided \"{fileNumber}\" is not a number, skipping.");
                continue; // Skip the rest and go to next iteration of the loop
            }

            // Check if the file number is within the list
            if (fileNumber < 1 || fileNumber > files.Count)
            {
                Console.WriteLine($"File number {fileNumber} is not in the list!");
                continue;
            }
            
            // We have to -1 from the input since computers count from 0
            deleteList.Add(files[fileNumber - 1]);
        }

        // If the list is empty
        if (deleteList.Count == 0)
        {
            Console.WriteLine("There are no files to delete.");
            return;
        }

        List<string> promptConfirmDelete = new List<string>()
        {
            "Do you want to delete the following files: (y/n)"
        };
        foreach (FileInfo fileInfo in deleteList)
        {
            promptConfirmDelete.Add(fileInfo.FullName);
        }

        string inputConfirmDelete = GetUserChoice(promptConfirmDelete, new List<string>() {"y", "n"});
        if (inputConfirmDelete.Equals("n"))
        {
            return;
        }

        // Since user confirmed the list of files to be deleted, we can delete them now
        foreach (FileInfo fileInfo in deleteList)
        {
            fileInfo.Delete();
        }
    }
}
