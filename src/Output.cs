using Foldersize.Shadowdara.src;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersize.Shadowdara.src
{
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
}
