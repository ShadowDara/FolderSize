using FolderSize.Shadowdara.src;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersize.Shadowdara.src
{
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
}
