using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public class Mvc4WebConfigVersionUpgrader : BaseUpgrader, IUpgrader
    {
        Dictionary<string, string> upgradeFromTo = new Dictionary<string, string>();
        IEnumerable<string> _searchPattern = new List<string> { "web.config" };

        public Mvc4WebConfigVersionUpgrader()
        {
            upgradeFromTo.Add("System.Web.Mvc, Version=3.0.0.0", "System.Web.Mvc, Version=4.0.0.0");
            upgradeFromTo.Add("System.Web.WebPages, Version=1.0.0.0", "System.Web.WebPages, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.Helpers, Version=1.0.0.0", "System.Web.Helpers, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.WebPages.Razor, Version=1.0.0.0", "System.Web.WebPages.Razor, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.Razor, Version=1.0.0.0", "System.Web.Razor, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.WebPages.Deployment, Version=1.0.0.0", "System.Web.WebPages.Deployment, Version=2.0.0.0");
            upgradeFromTo.Add("webpages:Version\" value=\"1.0.0.0", "webpages:Version\" value=\"2.0.0.0");
        }

        public IEnumerable<string> SearchPattern
        {
            get { return _searchPattern; }
        }

        public IEnumerable<string> Upgrade(string filepath, IEnumerable<string> lines, out bool didUpgrade)
        {
            lines = base.Upgrade(upgradeFromTo, lines, out didUpgrade);
            return lines;
        }
    }
}
