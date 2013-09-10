using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public abstract class BaseUpgrader
    {
        public virtual List<string> Upgrade(Dictionary<string, string> upgradeFromTo, List<string> lines, out bool didUpgrade)
        {
            didUpgrade = false;

            foreach (var upgrader in upgradeFromTo)
            {
                int index = 0;
                while (index != -1)
                {
                    index = lines.FindIndex(l => l.Contains(upgrader.Key));
                    if (index > -1)
                    {
                        lines[index] = lines[index].Replace(upgrader.Key, upgrader.Value);
                        didUpgrade = true;
                    }
                }
            }

            return lines;
        }
    }
}
