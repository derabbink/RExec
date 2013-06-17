using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Plugin
{
    /// <summary>
    /// Contains metadata from an Assembly object
    /// Used in transfer between AppDomains
    /// </summary>
    [Serializable]
    public class AssemblyData
    {
        public AssemblyData(Assembly assembly)
        {
            Name = assembly.GetName();
            ReferencedAssemblies = assembly.GetReferencedAssemblies();
            CodeBase = assembly.CodeBase;
            GlobalAssemblyCache = assembly.GlobalAssemblyCache;
        }

        public AssemblyName Name { get; private set; }

        public string CodeBase { get; private set; }

        public IEnumerable<AssemblyName> ReferencedAssemblies { get; private set; }

        public bool GlobalAssemblyCache { get; private set; }
    }
}
