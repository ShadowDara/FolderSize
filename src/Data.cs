using Foldersize.Shadowdara.src;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foldersize.Shadowdara.src
{
    /// <summary>
    /// FolderList is a data structure representing a folder and its size in bytes.
    /// </summary>
    /// <property name="folderPath">The path of the folder.</property>
    /// <property name="SizeInBytes">The size of the folder in bytes.</property>
    class FolderList
    {
        public string? FolderPath { get; set; }
        public long SizeInBytes { get; set; }
    }
}
