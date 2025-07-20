/// Project: Foldersize
/// Description: A simple C# console application to calculate the size of folders
/// Author: Shadowdara
/// Version: 1.0.0

using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FolderSize.Shadowdara
{
    #region Main
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

            Console.WriteLine("*********************************************");
            Console.WriteLine($"*\n*  Folder Size Analyzer - Version: {Settings.version}!\n*");
            Console.WriteLine("*********************************************\n");

            Console.WriteLine($"BaseDirectory: {Output.baseDirectory}\n");

            string folderPath = Ask_folder();

            // return of Ask_folder() can be null if the user cancels or
            // enters an invalid path
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
            Console.WriteLine("Please enter the path of the folder you want to analyze:\n");
            Console.WriteLine("For Example");
            Console.WriteLine("C:/Users/dara");
            Console.Write("$: ");
            string? folderPath = Console.ReadLine();

            Console.WriteLine($"\nYour enterred Folder: {folderPath}");
            Console.WriteLine("\nDo you want to analyze this folder? - enter to continue, (n) to cancel");
            Console.WriteLine("or $$settings to open the settings");

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

            List<FolderList> folderSize = new List<FolderList>();
            Analyser.DurchsucheUndBerechneGroesse(Output.baseDirectory, folderSize);

            Output.Create_output_folders();

            // TODO: Fix the JSON output here
            JSON.create(folderSize.ToString());
        }
    }
    #endregion

    #region Analyser
    /// <summary>
    /// Analyser provides methods for searching folders and calculating their sizes recursively.
    /// </summary>
    class Analyser
    {
        /// <summary>
        /// Recursively searches for files and subfolders in the specified directory and prints file paths.
        /// </summary>
        /// <param name="ordner">The directory to search.</param>
        public static void SearchFolder(string ordner)
        {
            // Alle Dateien im aktuellen Ordner
            foreach (string datei in Directory.GetFiles(ordner))
            {
                Console.WriteLine("Datei: " + datei);
            }

            // Rekursiv alle Unterordner durchsuchen
            foreach (string unterordner in Directory.GetDirectories(ordner))
            {
                SearchFolder(unterordner);
            }
        }

        /// <summary>
        /// Recursively calculates the total size of files in the specified directory and its subdirectories.
        /// Adds folder size information to the provided list.
        /// </summary>
        /// <param name="ordner">The directory to analyze.</param>
        /// <param name="liste">The list to store folder size information.</param>
        /// <returns>The total size in bytes of the directory and its subdirectories.</returns>
        public static long DurchsucheUndBerechneGroesse(string ordner, List<FolderList> liste)
        {
            long groesseInBytes = 0;

            try
            {
                // Dateien summieren
                foreach (string datei in Directory.GetFiles(ordner))
                {
                    FileInfo info = new FileInfo(datei);
                    groesseInBytes += info.Length;
                }

                // Unterordner rekursiv durchsuchen
                foreach (string unterordner in Directory.GetDirectories(ordner))
                {
                    groesseInBytes += DurchsucheUndBerechneGroesse(unterordner, liste);
                }

                // In Liste einfügen
                liste.Add(new FolderList
                {
                    folderPath = ordner,
                    SizeInBytes = groesseInBytes
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Zugriff auf {ordner}: {ex.Message}");
            }

            return groesseInBytes;
        }
    }

    /// <summary>
    /// Settings manages application configuration and version information.
    /// </summary>
    class Settings
    {
        public static string version = "1.0.0";

        public static void Create()
        { }

        static bool Check()
        {
            return false;
        }

        public static void Read()
        {
            if (Check())
            { }
        }
    }
    #endregion

    #region Output
    /// <summary>
    /// Output handles creation of output directories and files for different formats (HTML, JSON, txt, MD).
    /// </summary>
    class Output
    {
        public static string baseDirectory = AppContext.BaseDirectory;

        /// <summary>
        /// Creates output folders for HTML, JSON, txt, and MD formats if they do not already exist.
        /// </summary>
        public static void Create_output_folders()
        {
            // HTML
            if (!Directory.Exists(baseDirectory + "/output/html"))
            {
                Directory.CreateDirectory(baseDirectory + "/output/html");
            }

            // JSON
            if (!Directory.Exists(baseDirectory + "/output/json"))
            {
                Directory.CreateDirectory(baseDirectory + "/output/json");
            }

            // txt
            if (!Directory.Exists(baseDirectory + "/output/txt"))
            {
                Directory.CreateDirectory(baseDirectory + "/output/txt");
            }

            // MD
            if (!Directory.Exists(baseDirectory + "/output/md"))
            {
                Directory.CreateDirectory(baseDirectory + "/output/md");
            }
        }

        /// <summary>
        /// Prompts the user to choose the output format.
        /// </summary>
        public static void Ask_how()
        {
            Console.WriteLine("How do you want to have the output?");
        }

        /// <summary>
        /// Creates a new file with the specified content at the given path, if the file does not already exist.
        /// </summary>
        /// <param name="path">The file path including the file name.</param>
        /// <param name="content">The content to write to the file.</param>
        public static void create_file(string path, string content)
        {
            // Checks if the files already exists
            if (!File.Exists(path))
            {
                // Datei erstellen und Text hineinschreiben
                File.WriteAllText(path, content);
                Console.WriteLine($"created: {path}");
            }
            else
            {
                Console.WriteLine("File is already existing!");
            }
        }
    }
    #endregion

    #region output fo
    class Raw
    { }

    class HTML
    { }

    /// <summary>
    /// JSON provides functionality to serialize data and write it to a JSON file.
    /// </summary>
    class JSON
    {
        /// <summary>
        /// Serializes the provided data to JSON format and writes it to "ordnerInfos.json".
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        public static void create(string data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("ordnerInfos.json", json);
        }
    }

    class Text
    { }

    class MD
    { }

    class Test
    { }

    /// <summary>
    /// FolderList is a data structure representing a folder and its size in bytes.
    /// </summary>
    /// <property name="folderPath">The path of the folder.</property>
    /// <property name="SizeInBytes">The size of the folder in bytes.</property>
    class FolderList
    {
        public string? folderPath { get; set; }
        public long SizeInBytes { get; set; }
    }
    #endregion
}
