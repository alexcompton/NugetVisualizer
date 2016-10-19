using NuGet;
using NugetTree.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            var root = new NugetTreeNode("root");

            string packageID = "Newtonsoft.Json";
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");

            List<IPackage> packages = repo.FindPackagesById(packageID).ToList();

            packages = packages.Where(item => (item.IsReleaseVersion() == false)).ToList();

            foreach (IPackage p in packages)
            {
                var packageNode = new NugetTreeNode(p.GetFullName());

                if (p.DependencySets != null && p.DependencySets.Count() > 0)
                {
                    foreach (var ds in p.DependencySets)
                    {
                        var dependencyNode = new NugetTreeNode(ds.TargetFramework.Identifier + " " + ds.TargetFramework.Version.ToString());

                        if (ds.Dependencies != null && ds.Dependencies.Count() > 0)
                        {
                            foreach (var dep in ds.Dependencies)
                            {
                                dependencyNode.Add(new NugetTreeNode(dep.Id + " ≥ " + dep.VersionSpec.MinVersion));
                            }
                        }

                        packageNode.Add(dependencyNode);
                    }
                }

                root.Add(packageNode);
            }

            // Recursively display tree
            root.Display(1);

            // Wait for user
            Console.ReadKey();
        }
    }
}
