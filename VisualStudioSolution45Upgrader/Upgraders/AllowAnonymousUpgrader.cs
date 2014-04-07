using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public class AllowAnonymousUpgrader : BaseUpgrader, IUpgrader
    {
        Dictionary<string, string> upgradeAs = new Dictionary<string, string>();
        IEnumerable<string> _searchPattern = new List<string> { "*.cs" };

        public AllowAnonymousUpgrader()
        {
            upgradeAs.Add("[AllowAnonymous]", "[Janison.Web.UI.Security.AllowAnonymous]");
            upgradeAs.Add(", AllowAnonymous]", ", Janison.Web.UI.Security.AllowAnonymous]");
            upgradeAs.Add(",AllowAnonymous]", ", Janison.Web.UI.Security.AllowAnonymous]");
            upgradeAs.Add("[AllowAnonymous,", "[Janison.Web.UI.Security.AllowAnonymous,");
            upgradeAs.Add("[AllowAnonymousAttribute],", "[Janison.Web.UI.Security.AllowAnonymous]");
            upgradeAs.Add("Attributes<AllowAnonymousAttribute>", "Attributes<Janison.Web.UI.Security.AllowAnonymousAttribute>");
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
