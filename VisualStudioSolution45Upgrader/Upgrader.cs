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
    public class Upgrader
    {
        public void UpgradeInPath(DirectoryInfo dir, IEnumerable<IUpgrader> upgraders, bool recursive = true)
        {
            var backupFile = FuncEx.Create((string p) => {
                string date = DateTime.Now.ToString("yyyy-MMM-dd-H-mm-ss");
                string backupPath = String.Format("{0}-{1}.bak", p, date);
                if (File.Exists(backupPath))
                    File.Delete(backupPath);
                File.Copy(p, backupPath);
                return true;
            });

            if (recursive == true)
            {
                foreach (var d in dir.GetDirectories())
                {
                    UpgradeInPath(d, upgraders);
                }
            }

            var searchPatterns = upgraders.SelectMany(u => u.SearchPattern).Distinct().ToArray(); // Search patterns are defined by the upgrader
            var files = dir.GetFiles(searchPatterns);
            foreach (var f in files)
            {
                backupFile(f.FullName);

                // http://stackoverflow.com/questions/9213720/most-efficient-way-of-reading-file
                IEnumerable<string> lines = new List<string>(System.IO.File.ReadAllLines(f.FullName));

                var upgradedTypes = new Dictionary<IUpgrader, bool>();
                foreach (var upgrader in upgraders)
                {
                    bool didUpgrade;
                    lines = upgrader.Upgrade(f.FullName, lines, out didUpgrade);
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
    }
}
