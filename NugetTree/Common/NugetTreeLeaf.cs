using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetTree.Common
{
    public class NugetTreeLeaf : NugetTreeBase
    {
        public NugetTreeLeaf(string name)
            : base(name)
        {

        }

        public override void Add(NugetTreeBase node)
        {
            Console.WriteLine("Cannot add to a leaf");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }

        public override void Remove(NugetTreeBase node)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }
    }
}
