using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetTree.Common
{
    public abstract class NugetTreeBase
    {
        protected string name;

        public NugetTreeBase(string name)
        {
            this.name = name;
        }

        public abstract void Add(NugetTreeBase node);
        public abstract void Remove(NugetTreeBase node);
        public abstract void Display(int depth);
    }
}
