using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public class DotNet45Upgrader : BaseUpgrader, IUpgrader
    {
        Dictionary<string, string> upgradeAs = new Dictionary<string, string>();

        public DotNet45Upgrader()
        {
            upgradeAs.Add(@"<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>", @"<TargetFrameworkVersion>v4.5</TargetFrameworkVersion>");
        }

        public List<string> Upgrade(List<string> lines, out bool didUpgrade)
        {
            lines = base.Upgrade(upgradeAs, lines, out didUpgrade);
            return lines;
        }
    }
}
