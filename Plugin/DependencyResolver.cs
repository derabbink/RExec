using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace Plugin
{
    public class DependencyResolver
    {
        public static IObservable<AssemblyName> GetAllDependencies(AssemblyName start)
        {
            AppDomain tempDomain = createTempDomain();
            AssemblyLoader loader = getRemoteAssemblyLoader(tempDomain);
            Console.WriteLine(loader.PrintConfig());

            var result = GetAllDependenciesRecursive(start, loader, new List<AssemblyName>()).
                Finally(() => AppDomain.Unload(tempDomain));
            return result;
        }

        private static AppDomain createTempDomain()
        {
            string name = generateDomainName();
            //need to reuse config, in order to get access to THIS assembly and dependencies before others can be loaded for inspection
            Evidence evidence = AppDomain.CurrentDomain.Evidence;
            AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            
            AppDomain domain = AppDomain.CreateDomain(name, evidence, setup);
            domain.AssemblyResolve += assemblyResolve;
            return domain;
        }

        private static Assembly assemblyResolve(object sender, ResolveEventArgs args)
        {
            string path = ConfigurationManager.AppSettings.Get("Plugin.assembly-path");
            IEnumerable<string> paths = path.Split(Path.PathSeparator);
            foreach (string p in paths)
            {
                DirectoryInfo dir = new DirectoryInfo(p);
                IEnumerable<FileInfo> files = dir.GetFiles("*.dll");
                foreach (FileInfo f in files)
                {
                    try
                    {
                        AssemblyName name = AssemblyName.GetAssemblyName(f.FullName);
                        if (name.Name == args.Name || name.FullName == args.Name)
                            return Assembly.LoadFile(f.FullName);
                    }
                    catch {}
                }
            }
            return null;
        }

        private static string generateDomainName()
        {
            Guid guid = Guid.NewGuid();
            return string.Format("{0}-tmp.{1}", getOwnAssemblyName().Name, guid);
        }

        private static AssemblyName getOwnAssemblyName()
        {
            return Assembly.GetExecutingAssembly().GetName();
        }

        private static AssemblyLoader getRemoteAssemblyLoader(AppDomain domain)
        {
            AssemblyName ownAssyName = getOwnAssemblyName();
            Assembly assy = domain.Load(ownAssyName);
            AssemblyName assyName = assy.GetName(); //same as getOwnAssemblyName()
            string typename = typeof (AssemblyLoader).FullName;
            return domain.CreateInstanceAndUnwrap(assyName.Name, typename) as AssemblyLoader;
        }

        static private IObservable<AssemblyName> GetAllDependenciesRecursive(AssemblyName start, AssemblyLoader assemblyLoader, List<AssemblyName> filter)
        {
            Assembly loaded = assemblyLoader.ReflectionOnlyLoad(start);

            //don't follow leads in the global assembly cache
            if (loaded.GlobalAssemblyCache)
            {
                filter.Add(loaded.GetName());
                return Observable.Empty<AssemblyName>();
            }

            IEnumerable<AssemblyName> parent = loaded.GetReferencedAssemblies();

            IObservable<AssemblyName> result = parent.ToObservable().
                                                      //Select(an => augmentAssemblyName(an, start)).
                                                      Do(an => Console.WriteLine("{0} ({1}) depends on {2} ({3})", start.Name, start.CodeBase, an.Name, an.CodeBase)).
                                                      Where(an => !filter.Contains(an, new AssemblyNameEqualityComparer())).
                                                      Select(an => GetAllDependenciesRecursive(an, assemblyLoader, filter)).
                                                      Concat().
                                                      Concat(Observable.Return(start)).
                                                      Do(filter.Add);
            return result;
        }

        /// <summary>
        /// Adds information to an AssemblyName, derived from a provided context AssemblyName
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private static AssemblyName augmentAssemblyName(AssemblyName name, AssemblyName context)
        {
            if (string.IsNullOrEmpty(name.CodeBase))
            {
                
            }
            return name;
        }
    }
}
