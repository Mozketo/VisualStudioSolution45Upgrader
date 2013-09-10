using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Infrastructure.Extensions
{
    public static class DirectoryInfoEx
    {
        public static IEnumerable<FileInfo> GetFiles(this DirectoryInfo dir, params string[] searchPattern)
        {
            var files = new List<FileInfo>();
            foreach (var pattern in searchPattern)
            {
                files.AddRange(dir.GetFiles(pattern).AsEnumerable());   
            }
            return files;
        }
    }
}
