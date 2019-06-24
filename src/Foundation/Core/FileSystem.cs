using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Foundation.Core
{
    public static class FileSystem
    {
        public static void CreateDirectory(string directory)
        {
            var parent = Path.GetDirectoryName(directory);
            if(!Directory.Exists(parent))
            {
                CreateDirectory(parent);
            }

            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
