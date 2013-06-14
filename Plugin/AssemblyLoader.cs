using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Plugin
{
    /// <summary>
    /// Wrapper class for assembly laoding instructions
    /// must extend MarshalByRefObject to invoke methods in other AppDomain
    /// </summary>
    public class AssemblyLoader : MarshalByRefObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public Assembly ReflectionOnlyLoad(AssemblyName assembly)
        {
            return Assembly.ReflectionOnlyLoad(assembly.FullName);
        }

        public string PrintConfig()
        {
            AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            return string.Format("config:{0} appbase:{1} privatebin:{2} appname:{3}", setup.ConfigurationFile, setup.ApplicationBase, setup.PrivateBinPath, setup.ApplicationName);
        }
    }
}
