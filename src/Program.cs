/// Project: Foldersize
/// Description: A simple C# console application to calculate the size of folders
/// Author: Shadowdara
/// Version: 1.0.0
/// License: Appache 2.0 Shadowdara 2025

using Foldersize.Shadowdara.src;

using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FolderSize.Shadowdara.src
{
    /// <summary>
    /// MainClass is the entry point for the Folder Size Analyzer application.
    /// It initializes settings, displays version information, and starts folder analysis.
    /// </summary>
    class MainClass
    {
        static void Main(string[] args)
        {
            // Checking Settings...
            // Settings.Read();

            Title();

            Console.WriteLine($"Folder Size Analyzer - Version: {Settings.version}!\n");
            Console.WriteLine("by Shadowdara");

            Console.WriteLine("Welcome to the Folder Size Analyzer!");

            Console.WriteLine($"\n# BaseDirectory:\n# {Output.baseDirectory}\n");

            Console.Write("Do you want to enter the Menu? [y]: ");
            String answer = Console.ReadLine() ?? string.Empty;

            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                Menu();
            }

            string folderPath = Ask_folder();

            // return of Ask_folder() can be null if the user cancels or
            // enters an invalid path
            //
            // TODO:
            // Change from exiting to a loop that allows the user to
            // re-enter the program or change settings
            if (folderPath == null)
            {
                Console.WriteLine("Exiting!");
                Console.ReadLine();
                return;
            }

            //string folderPath = Output.baseDirectory;
            Run(folderPath);
        }

        /// <summary>
        /// Asks the user to input a folder path, confirms their choice, and initiates analysis if confirmed.
        /// </summary>
        /// <remarks>
        /// Validates folder existence and allows cancellation or access to settings.
        /// </remarks>
        static string Ask_folder()
        {
            Title();
            Console.WriteLine("Start Analizing");
            Console.WriteLine("\nPlease enter the path of the folder you want to analyze:\n");
            Console.WriteLine("* For Example");
            Console.WriteLine("* C:/Users/dara");
            Console.Write("$: ");
            string? folderPath = Console.ReadLine();

            Console.WriteLine($"\nYour enterred Folder: {folderPath}");
            Console.Write("\nDo you want to analyze this folder? - enter to continue, (n) to cancel: ");

            string? response = Console.ReadLine();

            if (string.IsNullOrEmpty(response) || response.ToLower() != "n")
            {
                // to check if the folder exists
                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine($"Folder '{folderPath}' does not exist.");
                    return null;
                }
                else
                {
                    return folderPath;
                }
            }
            else
            {
                Console.WriteLine("Folder analysis cancelled.");
            }
            return null;
        }

        /// <summary>
        /// Runs the folder analysis for the specified path, collects folder size data, creates output folders, and generates JSON output.
        /// </summary>
        /// <param name="folderPath">The path of the folder to analyze.</param>
        static void Run(string folderPath)
        {
            Console.WriteLine($"Analyzing folder: {folderPath}");

            List<FolderList> folderSize = [];
            long totalSize = Analyser.DurchsucheUndBerechneGroesse(Output.baseDirectory, folderSize);

            /*
            foreach (var folder in folderSize)
            {
                Console.WriteLine($"Ordner: {folder.FolderPath}, Größe: {folder.SizeInBytes} Bytes");
            }
            */

            Console.WriteLine($"Folder: {folderPath} has been analyzed successfully.");
            Console.WriteLine($"Size: {totalSize} Bytes");
            Console.WriteLine($"Size: {totalSize / 1024} KiloBytes");
            Console.WriteLine($"Size: {totalSize / 1024 / 1024} MegaBytes");
            Console.WriteLine($"Size: {totalSize / 1024 / 1024 / 1024} GigaBytes");
            Console.WriteLine("\nAnalysis complete!");

            Output.Create_output_folders();

            string json = JsonSerializer.Serialize(folderSize, new JsonSerializerOptions { WriteIndented = true });

            // TODO: Fix the JSON output here
            JSON.create(json);
        }

        public static void Menu()
        {
            Title();
            Console.WriteLine("======= MENU =======");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Settings");
            Console.WriteLine("2. LICENSE");
            Console.WriteLine("3. Infos");
            Console.WriteLine("4. go back and start analysing");
            Console.Write("\nPlease select an option (0-4): ");
            String answer = Console.ReadLine() ?? string.Empty;

            if (answer == "0")
            {
                Console.WriteLine("Exiting!");
                Console.ReadLine();
                return;
            }
            else if (answer == "1")
            {
                // Open Settings
            }
            else if (answer == "2")
            {
                Console.WriteLine("LICENSE");
                Console.WriteLine("This project is licensed under the Apache License 2.0.");
                Console.WriteLine("For more information, please visit: https://www.apache.org/licenses/LICENSE-2.0");
                Console.WriteLine("Created by Shadowdara in 2025.");
                Console.ReadLine();
            }
            else if (answer == "3")
            {
                Console.WriteLine("Info");
                Console.WriteLine("This is a simple C# console application to calculate the size of folders.");
                Console.ReadLine();
            }
            else if (answer == "4")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }

        public static void Title()
        {
            Console.Clear();
            Console.WriteLine(
                "'########::'#######::'##:::::::'########::'########:'########::\n" +
                " ##.....::'##.... ##: ##::::::: ##.... ##: ##.....:: ##.... ##:\n" +
                " ##::::::: ##:::: ##: ##::::::: ##:::: ##: ##::::::: ##:::: ##:\n" +
                " ######::: ##:::: ##: ##::::::: ##:::: ##: ######::: ########::\n" +
                " ##...:::: ##:::: ##: ##::::::: ##:::: ##: ##...:::: ##.. ##:::\n" +
                " ##::::::: ##:::: ##: ##::::::: ##:::: ##: ##::::::: ##::. ##::\n" +
                " ##:::::::. #######:: ########: ########:: ########: ##:::. ##:\n" +
                "..:::::::::.......:::........::........:::........::..:::::..::\n" +
                ":'######::'####:'########:'########::::::::::::::::::::::::::::\n" +
                "'##... ##:. ##::..... ##:: ##.....:::::::::::::::::::::::::::::\n" +
                " ##:::..::: ##:::::: ##::: ##::::::::::::::::::::::::::::::::::\n" +
                ". ######::: ##::::: ##:::: ######::::::::::::::::::::::::::::::\n" +
                ":..... ##:: ##:::: ##::::: ##...:::::::::::::::::::::::::::::::\n" +
                "'##::: ##:: ##::: ##:::::: ##::::::::::::::::::::::::::::::::::\n" +
                ". ######::'####: ########: ########::::::::::::::::::::::::::::\n" +
                ":......:::....::........::........:::::::::::::::::::::::::::::\n"
            );
        }
    }
}
