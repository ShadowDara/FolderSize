using Foldersize.Shadowdara.src;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersize.Shadowdara.src
{
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
}
