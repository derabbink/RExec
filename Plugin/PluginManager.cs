using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin
{
    public class PluginManager
    {

        private AppDomain _appDomain;

        public PluginManager()
        {
            createAppDomain();
        }

        private void createAppDomain()
        {
            _appDomain = AppDomain.CreateDomain("Plugin AppDomain");
        }

        /// <summary>
        /// Loads an assembly
        /// </summary>
        /// <param name="assembly"></param>
        public void Load(byte[] assembly)
        {
            _appDomain.Load(assembly);
        }

        /// <summary>
        /// Removes all loaded assemblies
        /// </summary>
        public void Reset()
        {
            unloadAppDomain();
            createAppDomain();
        }

        private void unloadAppDomain()
        {
            AppDomain.Unload(_appDomain);
        }
    }
}
