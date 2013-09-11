using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the list of params from the command line args such as starting path to recurse for files
            // Input and Output version numbers to upgrade

            bool showHelp = false;
            System.IO.DirectoryInfo path = null;
            bool recursive = true;
            
            var p = new OptionSet() {
                { "p|path=", "the directory path to the solution/project to be upgraded.", v => path = new System.IO.DirectoryInfo(v) },
                //{ "r", "recursively move through the path looking for .csproj files to upgrade.", v => recursive = Boolean.Parse(v) },
                { "h|help",  "show this message and exit", v => showHelp = v != null },
            };

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
                return;
            }

            if (path == null || path.Exists == false)
            {
                Console.WriteLine("Path does not exist");
                return;
            }

            // Using the Path supplied on the command-line upgrade all files in the solution.
            var upgrader = new Upgrader();
            upgrader.UpgradeInPath(path, recursive);
        }

        

        protected void ShowHelp()
        {
            throw new NotImplementedException();
        }
    }
}
