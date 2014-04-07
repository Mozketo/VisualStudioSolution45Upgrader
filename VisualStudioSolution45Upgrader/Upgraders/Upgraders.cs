using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Upgraders
{
    public class Upgraders
    {
        public IEnumerable<IUpgrader> All { get; protected set; }

        public Upgraders()
        {
            All = GetUpgraders();
        }

        protected IEnumerable<IUpgrader> GetUpgraders()
        {
            var upgradedTypes = new Dictionary<IUpgrader, bool>();
            var upgraders = new List<IUpgrader>();
            var type = typeof(IUpgrader);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsInterface == false);

            foreach (var t in types)
            {
                var upgrader = (IUpgrader)Activator.CreateInstance(t);
                upgraders.Add(upgrader);
            }

            return upgraders;
        }
    }
}
