﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public class WebConfigVersionUpgrader : BaseUpgrader, IUpgrader
    {
        Dictionary<string, string> upgradeFromTo = new Dictionary<string, string>();
        List<string> _searchPattern = new List<string> { "web.config" };

        public WebConfigVersionUpgrader()
        {
            upgradeFromTo.Add("System.Web.Mvc, Version=3.0.0.0", "System.Web.Mvc, Version=4.0.0.0");
            upgradeFromTo.Add("System.Web.WebPages, Version=1.0.0.0", "System.Web.WebPages, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.Helpers, Version=1.0.0.0", "System.Web.Helpers, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.WebPages.Razor, Version=1.0.0.0", "System.Web.WebPages.Razor, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.Razor, Version=1.0.0.0", "System.Web.Razor, Version=2.0.0.0");
            upgradeFromTo.Add("System.Web.WebPages.Deployment, Version=1.0.0.0", "System.Web.WebPages.Deployment, Version=2.0.0.0");
            upgradeFromTo.Add("webpages:Version\" value=\"1.0.0.0", "webpages:Version\" value=\"2.0.0.0");
        }

        public List<string> SearchPattern
        {
            get { return _searchPattern; }
        }

        public List<string> Upgrade(string filepath, List<string> lines, out bool didUpgrade)
        {
            lines = base.Upgrade(upgradeFromTo, lines, out didUpgrade);
            return lines;
        }
    }
}
