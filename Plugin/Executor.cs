using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Plugin
{
    internal class Executor : MarshalByRefObject
    {
        private string _storageDir;

        //constructur is public for reflection
        public Executor()
        {
            _storageDir = ConfigurationManager.AppSettings.Get("RExec.Dispatcher.Service.assembly-storage-dir");
        }

        internal static Executor CreateInstanceInAppDomain(AppDomain domain)
        {
            AssemblyName ownAssyName = Assembly.GetExecutingAssembly().GetName();
            string typename = typeof(Executor).FullName;
            return domain.CreateInstanceAndUnwrap(ownAssyName.FullName, typename) as Executor;
        }

        internal object Execute(string assemblyName, string typeName, string actionName)
        {
            Type t = Type.GetType(string.Format("{0}, {1}", typeName, assemblyName), true);
            object instance = Activator.CreateInstance(t);
            MethodInfo action = t.GetMethod(actionName);
            return action.Invoke(instance, new object[] { });
        }

        internal void LoadAssembly(string assemblyName)
        {
            Assembly.Load(assemblyName);
        }
    }
}
