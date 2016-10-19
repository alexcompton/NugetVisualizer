using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetTree.Common
{
    public class NugetTreeNode : NugetTreeBase
    {
        private List<NugetTreeBase> _children = new List<NugetTreeBase>();

        public NugetTreeNode(string name)
            : base(name)
        {
        }

        public override void Add(NugetTreeBase node)
        {
            _children.Add(node);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);

            // Recursively display child nodes
            foreach (var component in _children)
            {
                component.Display(depth + 2);
            }
        }

        public override void Remove(NugetTreeBase node)
        {
            _children.Remove(node);
        }
    }
}
