// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


namespace Task1_Shell
{
    class Program
    {
        static string currentDir = Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write($"{currentDir}> ");
                string command = Console.ReadLine();
                string[] tokens = command.Split(' ');

                if (tokens[0] == "ls")
                {
                    ListDirectory(currentDir);
                }
                else if (tokens[0] == "pwd")
                {
                    Console.WriteLine(currentDir);
                }
                else if (tokens[0] == "cd")
                {
                    if (tokens.Length > 1)
                    {
                        ChangeDirectory(tokens[1]);
                    }
                    else
                    {
                        Console.WriteLine("Error: No directory specified.");
                    }
                }
                else if (tokens[0] == "clear")
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"Error: Command not recognized: {tokens[0]}");
                }
            }
        }

        static void ListDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var item in dir.GetFileSystemInfos())
            {
                Console.WriteLine($"{item.Name}\t{item.CreationTime}\t{(item.Attributes.HasFlag(FileAttributes.Directory) ? "<DIR>" : "")}");
            }
        }

        static void ChangeDirectory(string path)
        {
            try
            {
                if (Path.IsPathRooted(path))
                {
                    currentDir = path;
                }
                else
                {
                    currentDir = Path.Combine(currentDir, path);
                }

                currentDir = Path.GetFullPath(currentDir);
                Directory.SetCurrentDirectory(currentDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
