using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    /// <summary>
    /// http://www.dotnetexpertguide.com/2011/12/upgrade-aspnet-mvc-3-project-to-mvc-4.html
    /// </summary>
    public class ComponentModelUpgrader : BaseUpgrader, IUpgrader
    {
        Dictionary<string, string> upgradeAs = new Dictionary<string, string>();
        IEnumerable<string> _searchPattern = new List<string> { "*.cs" };

        public ComponentModelUpgrader()
        {
            upgradeAs.Add("[Compare(", "[System.ComponentModel.DataAnnotations.Compare(");
        }

        public IEnumerable<string> Upgrade(string filename, IEnumerable<string> lines, out bool didUpgrade)
        {
            lines = base.Upgrade(upgradeAs, lines, out didUpgrade);
            return lines;
        }

        public IEnumerable<string> SearchPattern
        {
            get { return _searchPattern; }
        }
    }
}
