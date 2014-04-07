using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public class DotNet451Upgrader : BaseUpgrader, IUpgrader
    {
        Dictionary<string, string> upgradeAs = new Dictionary<string, string>();
        IEnumerable<string> _searchPattern = new List<string> { "*.csproj", "*.vbproj", "*.fsproj", "*.config" };

        public DotNet451Upgrader()
        {
            upgradeAs.Add(@"<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>", @"<TargetFrameworkVersion>v4.5</TargetFrameworkVersion>");
            //upgradeAs.Add(@"<TargetFrameworkVersion>v4.5</TargetFrameworkVersion>", @"<TargetFrameworkVersion>v4.5.1</TargetFrameworkVersion>");

            upgradeAs.Add("<compilation debug=\"true\" targetFramework=\"4.0\"", "<compilation debug=\"true\" targetFramework=\"4.5\"");
            upgradeAs.Add("<httpRuntime maxRequestLength", "<httpRuntime targetFramework=\"4.5\" maxRequestLength");
        }

        public IEnumerable<string> SearchPattern
        {
            get { return _searchPattern; }
        }

        public IEnumerable<string> Upgrade(string filename, IEnumerable<string> lines, out bool didUpgrade)
        {
            lines = base.Upgrade(upgradeAs, lines, out didUpgrade);
            return lines;
        }
    }
}
