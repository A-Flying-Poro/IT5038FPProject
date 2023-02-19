// ###########
// Change this namespace to something else
// ###########
namespace IT5038FPProject.simple;

static class Program
{
    private static readonly DirectoryInfo CurrentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
    private static readonly DirectoryInfo ProjectDirectory = CurrentDirectory.Parent!.Parent!.Parent!;
    private static readonly string FolderPathImages = Path.Combine(ProjectDirectory.FullName, "images");
    private static readonly string FolderPathText = Path.Combine(ProjectDirectory.FullName, "text");
    private static readonly string FileAbnormalFile = Path.Combine(ProjectDirectory.FullName, "abnormal.txt");
    
    private static Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>()
    {
        {".jpg", "image/jpeg"},
        {".txt", "text/plain"},
        {".png", "image/png"},
    };
    private static List<string> _abnormalTexts = new List<string>();
    
    // ###########
    // Remember to change Main2 to Main
    // ###########
    public static void Main2(string[] args)
    {
        // Read all lines in abnormal.txt 
        var abnormalTextLines = File.ReadAllLines(FileAbnormalFile);
        foreach (string line in abnormalTextLines)
        {
            // Check to make sure this line is not empty
            if (!string.IsNullOrEmpty(line))
            {
                // Add to list of abnormal texts
                _abnormalTexts.Add(line);
            }
        }
        
        // Flag to stop the program
        bool exit = false;

        do
        {
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("@@ Please select an option. @@");
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine("1. View all files in Image folder");
            Console.WriteLine("2. View all files in Text folder");
            Console.WriteLine("3. View all files");
            Console.WriteLine("4. Perform sorting of files into image and text folders");
            Console.WriteLine("5. Perform check on abnormal content in text file");
            Console.WriteLine("6. Exit");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": // Image folder
                {
                    List<FileInfo> files = new List<FileInfo>();
                    
                    Console.WriteLine(" No       Name and location of file       ||      Type of file");
                    Console.WriteLine("====     ===========================      ||     ==============");

                    int index = 1;
                    DirectoryInfo directoryInfo = new DirectoryInfo(FolderPathImages);
                    foreach (var fileInfo in directoryInfo.GetFiles())
                    {
                        files.Add(fileInfo);
                        Console.WriteLine(" " + index++ + "        " + fileInfo.FullName + "            " + MIMETypesDictionary[fileInfo.Extension]);
                    }
                    Console.WriteLine("Completed scan.");
                    
                    
                    
                    // Handle deletion
                    // Flag to repeat question if user did not provide acceptable answer
                    bool inputDeleteAccepted = false;

                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete file(s)? (y/n)");

                        string? inputDelete = Console.ReadLine()?.ToLowerInvariant(); // Make the user input lower case to accept both upper case and lower case Y/N
                        switch (inputDelete)
                        {
                            case "y": // Files to delete
                            {
                                // List to hold files to delete
                                List<FileInfo> deleteList = new List<FileInfo>();
                                
                                Console.WriteLine("Select file(s) to delete by typing file number(s) separated by space: 1 3");
                                string? inputFile = Console.ReadLine(); // User input for the file numbers
                                if (!string.IsNullOrEmpty(inputFile)) // If user provided file numbers to delete
                                {
                                    string[] inputFilesString = inputFile.Trim().Split(' '); // Split user input by space
                                    foreach (string inputFileString in inputFilesString)
                                    {
                                        // Try to convert the string to integer
                                        if (int.TryParse(inputFileString, out int fileNumber))
                                        {
                                            // Check if fileNumber is valid
                                            if (fileNumber < 1 || fileNumber > files.Count)
                                            {
                                                Console.WriteLine("Provided file number " + fileNumber + " is not valid.");
                                                continue;
                                            }
                                            
                                            // Since fileNumber is index + 1
                                            int fileIndex = fileNumber - 1;
                                            // Mark all the files to be deleted
                                            deleteList.Add(files[fileIndex]);
                                        }
                                        else // If input is not a number
                                        {
                                            Console.WriteLine("Provided " + inputFileString + " is not a number! Ignoring it.");
                                        }
                                    }



                                    if (deleteList.Count == 0)
                                    {
                                        Console.WriteLine("There are no files requested to be deleted.");
                                    }
                                    else
                                    {
                                        // Confirm if the user wants to delete the files requested
                                        Console.WriteLine("Do you want to delete the following files: (y/n)");
                                        foreach (var deleteFileInfo in deleteList)
                                        {
                                            Console.WriteLine(deleteFileInfo.FullName);
                                        }

                                        string? inputDeleteConfirm = Console.ReadLine()?.ToLowerInvariant();
                                        switch (inputDeleteConfirm)
                                        {
                                            case "y":
                                            {
                                                foreach (var deleteFileInfo in deleteList)
                                                {
                                                    deleteFileInfo.Delete();
                                                    Console.WriteLine(deleteFileInfo.FullName + " deleted successfully.");
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                
                                
                                inputDeleteAccepted = true;
                                break;
                            }
                            case "n": // No files to delete, we can return to main menu
                            {
                                inputDeleteAccepted = true;
                                break;
                            }
                            default:
                            {
                                Console.WriteLine("Unknown option!");
                                break;
                            }
                        }
                    } while (!inputDeleteAccepted);
                    
                    break;
                }
                case "2": // Text folder
                {
                    List<FileInfo> files = new List<FileInfo>();
                    
                    Console.WriteLine(" No       Name and location of file       ||      Type of file");
                    Console.WriteLine("====     ===========================      ||     ==============");

                    int index = 1;
                    DirectoryInfo directoryInfo = new DirectoryInfo(FolderPathText);
                    foreach (var fileInfo in directoryInfo.GetFiles())
                    {
                        files.Add(fileInfo);
                        Console.WriteLine(" " + index++ + "        " + fileInfo.FullName + "            " + MIMETypesDictionary[fileInfo.Extension]);
                    }
                    Console.WriteLine("Completed scan.");
                    
                    
                    
                    // Handle deletion
                    // Flag to repeat question if user did not provide acceptable answer
                    bool inputDeleteAccepted = false;

                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete file(s)? (y/n)");

                        string? inputDelete = Console.ReadLine()?.ToLowerInvariant(); // Make the user input lower case to accept both upper case and lower case Y/N
                        switch (inputDelete)
                        {
                            case "y": // Files to delete
                            {
                                // List to hold files to delete
                                List<FileInfo> deleteList = new List<FileInfo>();
                                
                                Console.WriteLine("Select file(s) to delete by typing file number(s) separated by space: 1 3");
                                string? inputFile = Console.ReadLine(); // User input for the file numbers
                                if (!string.IsNullOrEmpty(inputFile)) // If user provided file numbers to delete
                                {
                                    string[] inputFilesString = inputFile.Trim().Split(' '); // Split user input by space
                                    foreach (string inputFileString in inputFilesString)
                                    {
                                        // Try to convert the string to integer
                                        if (int.TryParse(inputFileString, out int fileNumber))
                                        {
                                            // Check if fileNumber is valid
                                            if (fileNumber < 1 || fileNumber > files.Count)
                                            {
                                                Console.WriteLine("Provided file number " + fileNumber + " is not valid.");
                                                continue;
                                            }
                                            
                                            // Since fileNumber is index + 1
                                            int fileIndex = fileNumber - 1;
                                            // Mark all the files to be deleted
                                            deleteList.Add(files[fileIndex]);
                                        }
                                        else // If input is not a number
                                        {
                                            Console.WriteLine("Provided " + inputFileString + " is not a number! Ignoring it.");
                                        }
                                    }



                                    if (deleteList.Count == 0)
                                    {
                                        Console.WriteLine("There are no files requested to be deleted.");
                                    }
                                    else
                                    {
                                        // Confirm if the user wants to delete the files requested
                                        Console.WriteLine("Do you want to delete the following files: (y/n)");
                                        foreach (var deleteFileInfo in deleteList)
                                        {
                                            Console.WriteLine(deleteFileInfo.FullName);
                                        }

                                        string? inputDeleteConfirm = Console.ReadLine()?.ToLowerInvariant();
                                        switch (inputDeleteConfirm)
                                        {
                                            case "y":
                                            {
                                                foreach (var deleteFileInfo in deleteList)
                                                {
                                                    deleteFileInfo.Delete();
                                                    Console.WriteLine(deleteFileInfo.FullName + " deleted successfully.");
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                
                                
                                inputDeleteAccepted = true;
                                break;
                            }
                            case "n": // No files to delete, we can return to main menu
                            {
                                inputDeleteAccepted = true;
                                break;
                            }
                            default:
                            {
                                Console.WriteLine("Unknown option!");
                                break;
                            }
                        }
                    } while (!inputDeleteAccepted);

                    break;
                }
                case "3": // All files
                {
                    List<FileInfo> files = new List<FileInfo>();
                    
                    Console.WriteLine(" No       Name and location of file       ||      Type of file");
                    Console.WriteLine("====     ===========================      ||     ==============");

                    int index = 1;
                    DirectoryInfo directoryInfoText = new DirectoryInfo(FolderPathImages);
                    foreach (var fileInfo in directoryInfoText.GetFiles())
                    {
                        files.Add(fileInfo);
                        Console.WriteLine(" " + index++ + "        " + fileInfo.FullName + "            " + MIMETypesDictionary[fileInfo.Extension]);
                    }
                    DirectoryInfo directoryInfoImages = new DirectoryInfo(FolderPathImages);
                    foreach (var fileInfo in directoryInfoImages.GetFiles())
                    {
                        files.Add(fileInfo);
                        Console.WriteLine(" " + index++ + "        " + fileInfo.FullName + "            " + MIMETypesDictionary[fileInfo.Extension]);
                    }
                    Console.WriteLine("Completed scan.");
                    
                    
                    
                    // Handle deletion
                    // Flag to repeat question if user did not provide acceptable answer
                    bool inputDeleteAccepted = false;

                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete file(s)? (y/n)");

                        string? inputDelete = Console.ReadLine()?.ToLowerInvariant(); // Make the user input lower case to accept both upper case and lower case Y/N
                        switch (inputDelete)
                        {
                            case "y": // Files to delete
                            {
                                // List to hold files to delete
                                List<FileInfo> deleteList = new List<FileInfo>();
                                
                                Console.WriteLine("Select file(s) to delete by typing file number(s) separated by space: 1 3");
                                string? inputFile = Console.ReadLine(); // User input for the file numbers
                                if (!string.IsNullOrEmpty(inputFile)) // If user provided file numbers to delete
                                {
                                    string[] inputFilesString = inputFile.Trim().Split(' '); // Split user input by space
                                    foreach (string inputFileString in inputFilesString)
                                    {
                                        // Try to convert the string to integer
                                        if (int.TryParse(inputFileString, out int fileNumber))
                                        {
                                            // Check if fileNumber is valid
                                            if (fileNumber < 1 || fileNumber > files.Count)
                                            {
                                                Console.WriteLine("Provided file number " + fileNumber + " is not valid.");
                                                continue;
                                            }
                                            
                                            // Since fileNumber is index + 1
                                            int fileIndex = fileNumber - 1;
                                            // Mark all the files to be deleted
                                            deleteList.Add(files[fileIndex]);
                                        }
                                        else // If input is not a number
                                        {
                                            Console.WriteLine("Provided " + inputFileString + " is not a number! Ignoring it.");
                                        }
                                    }



                                    if (deleteList.Count == 0)
                                    {
                                        Console.WriteLine("There are no files requested to be deleted.");
                                    }
                                    else
                                    {
                                        // Confirm if the user wants to delete the files requested
                                        Console.WriteLine("Do you want to delete the following files: (y/n)");
                                        foreach (var deleteFileInfo in deleteList)
                                        {
                                            Console.WriteLine(deleteFileInfo.FullName);
                                        }

                                        string? inputDeleteConfirm = Console.ReadLine()?.ToLowerInvariant();
                                        switch (inputDeleteConfirm)
                                        {
                                            case "y":
                                            {
                                                foreach (var deleteFileInfo in deleteList)
                                                {
                                                    deleteFileInfo.Delete();
                                                    Console.WriteLine(deleteFileInfo.FullName + " deleted successfully.");
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                
                                
                                inputDeleteAccepted = true;
                                break;
                            }
                            case "n": // No files to delete, we can return to main menu
                            {
                                inputDeleteAccepted = true;
                                break;
                            }
                            default:
                            {
                                Console.WriteLine("Unknown option!");
                                break;
                            }
                        }
                    } while (!inputDeleteAccepted);

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
                case "5": // Abnormal content
                {
                    // Prepares a list to collect files containing abnormal text
                    List<FileInfo> files = new List<FileInfo>();
                    
                    Console.WriteLine(" No       Name and location of file       ||      Abnormal text detected");
                    Console.WriteLine("====     ===========================      ||     ========================");
                    int index = 1;
                    
                    // Loop through the files in the text folder
                    DirectoryInfo directoryInfo = new DirectoryInfo(FolderPathText);
                    foreach (var fileInfo in directoryInfo.GetFiles())
                    {
                        // Read the contents of the current file
                        string fileContents = File.ReadAllText(fileInfo.FullName);
                        // Loop through the list of abnormal texts
                        foreach (string abnormalText in abnormalTextLines)
                        {
                            // Check if the current file contains the current abnormal text
                            if (fileContents.Contains(abnormalText))
                            {
                                // Add the current file into the list
                                files.Add(fileInfo);
                                Console.WriteLine(" " + index++ + "        " + fileInfo.FullName + "            " + abnormalText);
                                
                                break; // Since this file already contains abnormal text, we can skip searching through the rest of the abnormal texts
                            }
                        }
                    }
                    Console.WriteLine("Completed scan.");
                    
                    
                    
                    // Handle deletion
                    // Flag to repeat question if user did not provide acceptable answer
                    bool inputDeleteAccepted = false;

                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Do you want to delete file(s)? (y/n)");

                        string? inputDelete = Console.ReadLine()?.ToLowerInvariant(); // Make the user input lower case to accept both upper case and lower case Y/N
                        switch (inputDelete)
                        {
                            case "y": // Files to delete
                            {
                                // List to hold files to delete
                                List<FileInfo> deleteList = new List<FileInfo>();
                                
                                Console.WriteLine("Select file(s) to delete by typing file number(s) separated by space: 1 3");
                                string? inputFile = Console.ReadLine(); // User input for the file numbers
                                if (!string.IsNullOrEmpty(inputFile)) // If user provided file numbers to delete
                                {
                                    string[] inputFilesString = inputFile.Trim().Split(' '); // Split user input by space
                                    foreach (string inputFileString in inputFilesString)
                                    {
                                        // Try to convert the string to integer
                                        if (int.TryParse(inputFileString, out int fileNumber))
                                        {
                                            // Check if fileNumber is valid
                                            if (fileNumber < 1 || fileNumber > files.Count)
                                            {
                                                Console.WriteLine("Provided file number " + fileNumber + " is not valid.");
                                                continue;
                                            }
                                            
                                            // Since fileNumber is index + 1
                                            int fileIndex = fileNumber - 1;
                                            // Mark all the files to be deleted
                                            deleteList.Add(files[fileIndex]);
                                        }
                                        else // If input is not a number
                                        {
                                            Console.WriteLine("Provided " + inputFileString + " is not a number! Ignoring it.");
                                        }
                                    }



                                    if (deleteList.Count == 0)
                                    {
                                        Console.WriteLine("There are no files requested to be deleted.");
                                    }
                                    else
                                    {
                                        // Confirm if the user wants to delete the files requested
                                        Console.WriteLine("Do you want to delete the following files: (y/n)");
                                        foreach (var deleteFileInfo in deleteList)
                                        {
                                            Console.WriteLine(deleteFileInfo.FullName);
                                        }

                                        string? inputDeleteConfirm = Console.ReadLine()?.ToLowerInvariant();
                                        switch (inputDeleteConfirm)
                                        {
                                            case "y":
                                            {
                                                foreach (var deleteFileInfo in deleteList)
                                                {
                                                    deleteFileInfo.Delete();
                                                    Console.WriteLine(deleteFileInfo.FullName + " deleted successfully.");
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                
                                
                                inputDeleteAccepted = true;
                                break;
                            }
                            case "n": // No files to delete, we can return to main menu
                            {
                                inputDeleteAccepted = true;
                                break;
                            }
                            default:
                            {
                                Console.WriteLine("Unknown option!");
                                break;
                            }
                        }
                    } while (!inputDeleteAccepted);
                    
                    break;
                }
                case "6":
                {
                    exit = true;
                    Console.WriteLine("Goodbye!");
                    break;
                }

                default:
                {
                    Console.WriteLine("Unrecognised input.");
                    break;
                }
            }
        } while (!exit);
    }
}
