using NuGet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NugetVisualizer
{
    class NugetCoreClient
    {
        
        private static ConcurrentDictionary<string, IPackage> packages = new ConcurrentDictionary<string, IPackage>();

        public static async Task<List<TreeNode>> AddNodes()
        {
            var tree = new List<TreeNode>();
            
            string packageID = "Newtonsoft.Json";
            
            IPackageRepository repo = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
 
            List<IPackage> packages = repo.FindPackagesById(packageID).ToList();
            
            packages = packages.Where(item => (item.IsReleaseVersion() == false)).ToList();
            
            foreach (IPackage p in packages)
            {
                var depSet = new List<TreeNode>();

                if (p.DependencySets != null && p.DependencySets.Count() > 0)
                {
                    foreach(var ds in p.DependencySets)
                    {
                        var deps = new List<TreeNode>();

                        if (ds.Dependencies != null && ds.Dependencies.Count() > 0)
                        {
                            foreach(var dep in ds.Dependencies)
                            {
                                deps.Add(new TreeNode(dep.Id + " ≥ " + dep.VersionSpec.MinVersion));
                            }
                        }

                        depSet.Add(new TreeNode(ds.TargetFramework.Identifier + " " + ds.TargetFramework.Version.ToString(), deps.ToArray()));
                    }
                }

                tree.Add(new TreeNode(p.GetFullName(), depSet.ToArray()));
            }

            await Task.Delay(0);
            return tree;
        }
    }
}
