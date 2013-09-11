using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public interface IUpgrader
    {
        List<string> SearchPattern { get; }
        List<string> Upgrade(List<string> lines, out bool didUpgrade);
    }
}
