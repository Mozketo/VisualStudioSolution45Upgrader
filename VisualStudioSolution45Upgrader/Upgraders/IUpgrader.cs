using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public interface IUpgrader
    {
        IEnumerable<string> SearchPattern { get; }
        IEnumerable<string> Upgrade(string filepath, IEnumerable<string> lines, out bool didUpgrade);
    }
}
