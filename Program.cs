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
    class MainClass
    {
        static void Main(string[] args)
        {
            // Checking Settings...
            // Settings.Read();

            Console.WriteLine("******************************************");
            Console.WriteLine($"\nFolder Size Analyzer - Version: {Settings.version}!\n");

            //Console.WriteLine($"BaseDirectory: {Output.baseDirectory}");

            //Ask_folder();
            string folderPath = Output.baseDirectory;
            Run(folderPath);
        }

        /// <summary>
        /// Asks the user for a folder path and confirms if they want to analyze it.
        static void Ask_folder()
        {
            Console.WriteLine("Please enter the path of the folder you want to analyze:\n");
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
                    return;
                }
                Run(folderPath);
            }
            else
            {
                Console.WriteLine("Folder analysis cancelled.");
            }
        }

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
    class Analyser
    {
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
                    folerPath = ordner,
                    SizeinBytes = groesseInBytes
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Zugriff auf {ordner}: {ex.Message}");
            }

            return groesseInBytes;
        }
    }

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
    class Output
    {
        public static string baseDirectory = AppContext.BaseDirectory;

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

        public static void Ask_how()
        {
            Console.WriteLine("How do you want to have the output?");
        }

        /// <summary>
        /// Create a new file with the content in the folder path direction
        /// The end of the path is the file name
        /// function does not override if the files exists
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

    #region output formats
    class Raw
    { }

    class HTML
    { }

    class JSON
    {
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
    {}

    // data structure for the size array
    class FolderList
    {
        public string? folerPath { get; set; }
        public long SizeinBytes { get; set; }
    }
    #endregion
}
