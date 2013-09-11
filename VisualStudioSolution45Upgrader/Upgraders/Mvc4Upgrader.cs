using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    /// <summary>
    /// http://www.dotnetexpertguide.com/2011/12/upgrade-aspnet-mvc-3-project-to-mvc-4.html
    /// </summary>
    public class Mvc4Upgrader : BaseUpgrader, IUpgrader
    {
        Dictionary<string, string> upgradeAs = new Dictionary<string, string>();
        List<string> _searchPattern;

        public Mvc4Upgrader()
        {
            upgradeAs.Add("E53F8FEA-EAE0-44A6-8774-FFD645390401", "E3E379DF-F4C6-4180-9B81-6769533ABE47");

            _searchPattern = new List<string> { "*.csproj", "*.vbproj", "*.fsproj" };
        }

        public List<string> SearchPattern
        {
            get { return _searchPattern; }
        }

        public List<string> Upgrade(List<string> lines, out bool didUpgrade)
        {
            lines = base.Upgrade(upgradeAs, lines, out didUpgrade);
            return lines;
        }
    }
}
