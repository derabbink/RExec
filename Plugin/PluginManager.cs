using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using DependencyResolver;

namespace Plugin
{
    /// <summary>
    /// wraps assemblies in another appdomain
    /// </summary>
    public class PluginManager : IDisposable
    {
        private static readonly IList<string> assemblyExtensions = new List<string>(){".dll", ".exe"};

        private AppDomain _appDomain;
        private Executor _executor;
        private string _storageDir;

        public PluginManager()
        {
            _storageDir = ConfigurationManager.AppSettings.Get("Plugin.assembly-storage-dir");
            createAppDomainAndExecutor();
        }

        private void createAppDomainAndExecutor()
        {
            AppDomain.CurrentDomain.AssemblyResolve += findAssemblyInStorageDir;
            
            prepareAppDomainAppBasePath();
            string name = getNewAppDomainName();
            //need to reuse config, in order to get access to THIS assembly and dependencies
            //before others can be loaded into reflection context
            Evidence evidence = AppDomain.CurrentDomain.Evidence;
            AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            setup.PrivateBinPath = _storageDir;
            setup.PrivateBinPathProbe = string.Empty; //any non-null string will do

            _appDomain = AppDomain.CreateDomain(name, evidence, setup);
            _executor = Executor.CreateInstanceInAppDomain(_appDomain);
            
        }

        private string getNewAppDomainName()
        {
            Guid guid = Guid.NewGuid();
            return string.Format("PluginManager.{0}", guid);
        }

        private void prepareAppDomainAppBasePath()
        {
            if (!Directory.Exists(_storageDir))
                Directory.CreateDirectory(_storageDir);

            //copy this assembly (and dependencies) to private bin path
            Resolver.GetAllDependencies(Assembly.GetExecutingAssembly().GetName()).Subscribe(an =>
                {
                    string path = new Uri(an.CodeBase).LocalPath;
                    string fName = Path.GetFileName(path);
                    try
                    {
                        File.Copy(path, Path.Combine(_storageDir, fName), true);
                    }
                    catch (IOException ex)
                    {
                        //skip over locked files. this means they are already loaded
                        if (!isFileLocked(ex))
                            throw;
                    }
                });
        }

        /// <summary>
        /// .net always loads assemblies in all appdomains.
        /// this method finds an assembly in the storage dir for the default appdomain
        /// can be skipped by specifying a PrivateBinPath in the App.config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly findAssemblyInStorageDir(object sender, ResolveEventArgs args)
        {
            DirectoryInfo dir = new DirectoryInfo(_storageDir);
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            foreach (FileInfo f in files)
            {
                if (!assemblyExtensions.Contains(f.Extension.ToLowerInvariant()))
                    continue;

                AssemblyName name = AssemblyName.GetAssemblyName(f.FullName);
                if (name.FullName == args.Name || name.Name == args.Name)
                {
                    var result = Assembly.LoadFrom(f.FullName);
                    return result;
                }
            }
            //if nothing found
            return null;
        }


        /// <summary>
        /// Loads an assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="assemblyName"></param>
        /// <param name="assemblyFullName"></param>
        public void Load(Stream assembly, string assemblyName, string assemblyFullName)
        {
            //must take detour through storing file on diks, because default appdomain needs
            //to load assembly as well, so keeping it in memory after transfer is not an option
            try
            {
                storeAssembly(assembly, assemblyName);
            }
            catch (IOException ex)
            {
                //skip over locked files. this means they are already loaded
                if (!isFileLocked(ex))
                    throw;
            }
            _executor.LoadAssembly(assemblyFullName);
        }

        private void storeAssembly(Stream assembly, string assemblyName)
        {
            string filename = Path.Combine(_storageDir, string.Format("{0}.dll", assemblyName));

            using (FileStream outfile = new FileStream(filename, FileMode.Create))
            {
                const int bufferSize = 4096; //4KiB
                Byte[] buffer = new Byte[bufferSize];
                int bytesRead;
                while ((bytesRead = assembly.Read(buffer, 0, bufferSize)) > 0)
                    outfile.Write(buffer, 0, bytesRead);
            }
        }

        /// <summary>
        /// checks if a file is locked, given its IOException
        /// see http://stackoverflow.com/a/3202085/1296709
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private bool isFileLocked(IOException exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }

        public void Reset()
        {
            unloadAppDomain();
            createAppDomainAndExecutor();
        }

        public object Execute(string assemblyName, string typeName, string actionName)
        {
            object result = _executor.Execute(assemblyName, typeName, actionName);
            Console.WriteLine();
            return result;
        }

        private void unloadAppDomain()
        {
            AppDomain.Unload(_appDomain);
        }

        public void Dispose()
        {
            unloadAppDomain();
        }
    }
}
