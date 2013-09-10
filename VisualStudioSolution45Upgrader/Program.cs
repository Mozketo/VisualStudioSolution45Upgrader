using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualStudioSolution45Upgrader.Infrastructure.Extensions;
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
            string mvcVersion = String.Empty;
            string dotnetVersion = String.Empty;
            bool recursive = false;
            
            var p = new OptionSet() {
                { "p|path=", "the directory path to the solution/project to be upgraded.", v => path = new System.IO.DirectoryInfo(v) },
                { "r", "recursively move through the path looking for .csproj files to upgrade.", v => dotnetVersion = v },
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

            //List<string> lines = new List<string>(System.IO.File.ReadAllLines(path.FullName));
            UpgradeInPath(path, mvcVersion, dotnetVersion);
        }

        static void UpgradeInPath(DirectoryInfo dir, string upgradeMvcVersion, string upgradeDotnetVersion)
        {
            var backupFile = FuncEx.Create((string p) =>
            {
                string date = DateTime.Now.ToString("yyyy-MMM-dd-H-mm-ss");
                string backupPath = String.Format("{0}-{1}.bak", p, date);
                if (File.Exists(backupPath))
                    File.Delete(backupPath);
                File.Copy(p, backupPath);
                return true;
            });

            foreach (var d in dir.GetDirectories())
            {
                UpgradeInPath(d, upgradeMvcVersion, upgradeDotnetVersion);
            }

            var files = dir.GetFiles("*.csproj", "*.fsproj", "*.vbproj", "web.config");
            foreach (var f in files)
            {
                backupFile(f.FullName);

                List<string> lines = new List<string>(System.IO.File.ReadAllLines(f.FullName));

                var upgradedTypes = new Dictionary<IUpgrader, bool>();
                var type = typeof(IUpgrader);
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && p.IsInterface == false);

                foreach (var t in types)
                {
                    var upgrader = (IUpgrader)Activator.CreateInstance(t);
                    bool didUpgrade;
                    lines = upgrader.Upgrade(lines, out didUpgrade);
                    upgradedTypes.Add(upgrader, didUpgrade);
                }

                if (upgradedTypes.Values.Any(u => u == true))
                {
                    File.WriteAllText(f.FullName, String.Join(Environment.NewLine, lines));
                    Console.WriteLine(String.Format("Upgraded {0}", f.FullName));
                }
                else
                {
                    Console.WriteLine(String.Format("Skipped {0}", f.FullName));
                }
            }
        }

        protected void ShowHelp()
        {
            throw new NotImplementedException();
        }
    }
}
