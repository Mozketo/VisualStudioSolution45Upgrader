using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    interface IUpgrader
    {
        List<string> Upgrade(List<string> lines, out bool didUpgrade);
    }
}
