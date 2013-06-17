using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Plugin
{
    /// <summary>
    /// Wrapper class for assembly laoding instructions
    /// must extend MarshalByRefObject to invoke methods in other AppDomain
    /// </summary>
    public class AssemblyLoader : MarshalByRefObject
    {
        private readonly string _assemblyPath;

        public AssemblyLoader()
        {
            _assemblyPath = ConfigurationManager.AppSettings.Get("Plugin.assembly-path");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public AssemblyData ReflectionOnlyLoad(AssemblyName assembly)
        {
            try
            {
                return new AssemblyData(Assembly.ReflectionOnlyLoad(assembly.FullName));
            }
            catch (FileNotFoundException)
            {
                //there is no AssemblyResolve or ReflectionOnlyAssemblyResolve event fired
                //if ReflectionOnlyLoad fails, so this is a custom implementation for that
                IEnumerable<string> paths = _assemblyPath.Split(Path.PathSeparator);
                foreach (string p in paths)
                {
                    DirectoryInfo dir = new DirectoryInfo(p);
                    IEnumerable<FileInfo> files = dir.GetFiles("*.dll");
                    foreach (FileInfo f in files)
                    {
                        AssemblyName name = AssemblyName.GetAssemblyName(f.FullName);
                        if (name.FullName == assembly.FullName || name.Name == assembly.Name)
                            return new AssemblyData(Assembly.ReflectionOnlyLoadFrom(f.FullName));
                    }
                }
                //if we haven't returned by now, reuse the exception
                throw;
            }
        }
    }
}
