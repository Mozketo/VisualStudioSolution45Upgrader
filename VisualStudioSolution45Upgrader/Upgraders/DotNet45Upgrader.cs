﻿using System;
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
        List<string> _searchPattern = new List<string> { "*.csproj", "*.vbproj", "*.fsproj" };

        public DotNet45Upgrader()
        {
            upgradeAs.Add(@"<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>", @"<TargetFrameworkVersion>v4.5</TargetFrameworkVersion>");
        }

        public List<string> SearchPattern
        {
            get { return _searchPattern; }
        }

        public List<string> Upgrade(string filename, List<string> lines, out bool didUpgrade)
        {
            lines = base.Upgrade(upgradeAs, lines, out didUpgrade);
            return lines;
        }
    }
}
