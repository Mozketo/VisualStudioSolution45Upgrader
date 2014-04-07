using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualStudioSolution45Upgrader.Upgraders;

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
            bool convertToMvc4 = false;
            bool convertToDotNet45 = false;
            
            var p = new OptionSet() {
                { "p|path=", "the directory path to the solution/project to be upgraded.", v => path = new System.IO.DirectoryInfo(v) },
                { "dotnet45", "Convert the project to .net v4.5.1", v => convertToDotNet45 = true },
                { "mvc4", "Convert the project to MVC4", v => convertToMvc4 = true },
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

            // I don't like this hardcoded list of upgraders, but it's fine for now.
            //var upgraders = new Upgraders.Upgraders().All;
            var upgraders = new List<IUpgrader>();
            if (convertToDotNet45)
            {
                upgraders.AddRange(new List<IUpgrader> { new Upgraders.DotNet451Upgrader(), new Upgraders.ComponentModelUpgrader(), new Upgraders.AllowAnonymousUpgrader() });
            }

            if (convertToMvc4)
            {
                upgraders.AddRange(new List<IUpgrader> { new Upgraders.Mvc4Upgrader(), new Upgraders.Mvc4WebConfigVersionUpgrader() });
            }

            if (upgraders.Any() == false)
            {
                Console.WriteLine("");
                Console.WriteLine("No upgraders are selected from the commandline. Please use the commandline args to define what you want to upgrade.");
                return;
            }

            // Using the Path supplied on the command-line upgrade all files in the solution.
            var upgrader = new Upgrader();
            upgrader.UpgradeInPath(path, upgraders.AsEnumerable(), recursive);


            Console.WriteLine("1. Add the following to the system.web element:");
            Console.WriteLine("");
            Console.WriteLine("<machineKey compatibilityMode=\"Framework20SP1\"/>");
            Console.WriteLine("");
            Console.WriteLine("2. Change the targetFramework attribute of the compilation element from 4.0 to 4.5.1:");
            Console.WriteLine("");
            Console.WriteLine("<compilation debug=\"true\" targetFramework=\"4.5.1\" optimizeCompilations=\"true\" batch=\"false\">");
            Console.WriteLine("");
            Console.WriteLine("3. Add targetFramework=\"4.5.1\" attribute to the httpRuntime element:");
            Console.WriteLine("");
            Console.WriteLine("<httpRuntime targetFramework=\"4.5.1\" maxRequestLength=\"1024000\" requestValidationMode=\"2.0\" enableVersionHeader=\"false\"/>");
        }



        protected void ShowHelp()
        {
            throw new NotImplementedException();
        }
    }
}
