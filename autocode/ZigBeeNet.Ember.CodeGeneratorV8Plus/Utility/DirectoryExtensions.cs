using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Utility
{
    internal static class DirectoryExtensions
    {
        /// <summary>
        /// Asynchronously clears all contents of a directory without deleting the directory itself.
        /// </summary>
        /// <param name="directoryInfo">The DirectoryInfo object representing the directory to clear.</param>
        public static void ClearAsync(this DirectoryInfo directory)
        {
            // Preserve folder metadata (timestamps, attributes, etc.)
            foreach (var file in directory.GetFiles())
                file.Delete();

            foreach (var dir in directory.GetDirectories())
                dir.Delete(true);  // true = recursive
        }
    }
}
