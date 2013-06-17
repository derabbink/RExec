using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
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

            ISet<AssemblyName> assemblies = new HashSet<AssemblyName>();


            Func<AssemblyName, AssemblyData> preProcess = loader.ReflectionOnlyLoad;
            Func<AssemblyName, bool> filter = an => !assemblies.Contains(an, new AssemblyNameEqualityComparer());
            Action<AssemblyName> postProcess = an => assemblies.Add(an);

            Func<AssemblyName, IObservable<AssemblyName>> descend = an => GetAllDependenciesRecursive(an, filter, preProcess, postProcess);

            var result = GetAllDependenciesRecursive(start, filter, preProcess, postProcess).
                Finally(() => AppDomain.Unload(tempDomain));
            return result;
        }

        private static AppDomain createTempDomain()
        {
            string name = generateDomainName();
            //need to reuse config, in order to get access to THIS assembly and dependencies
            //before others can be loaded into reflection context
            Evidence evidence = AppDomain.CurrentDomain.Evidence;
            AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            
            AppDomain domain = AppDomain.CreateDomain(name, evidence, setup);
            return domain;
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

        static private IObservable<AssemblyName> GetAllDependenciesRecursive(AssemblyName start, Func<AssemblyName, bool> filter, Func<AssemblyName, AssemblyData> preProcess, Action<AssemblyName> postProcess)
        {
            AssemblyData loaded = preProcess(start);
            
            //don't follow leads in the global assembly cache
            if (loaded.GlobalAssemblyCache)
            {
                postProcess(loaded.Name);
                return Observable.Empty<AssemblyName>();
            }

            IEnumerable<AssemblyName> parent = loaded.ReferencedAssemblies;

            
            IObservable<AssemblyName> result = parent.ToObservable()
                                                     .Where(filter)
                                                     .Select(an => GetAllDependenciesRecursive(an, filter, preProcess, postProcess)).Concat()
                                                     .Concat(Observable.Return(loaded.Name)) //loaded.Name contains correct CodeBase
                                                     .Do(postProcess);
            return result;
        }

    }
}
