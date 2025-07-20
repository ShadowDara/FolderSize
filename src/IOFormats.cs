using Foldersize.Shadowdara.src;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Foldersize.Shadowdara.src
{
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
}
