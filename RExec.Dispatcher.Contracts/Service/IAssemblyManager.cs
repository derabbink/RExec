using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RExec.Dispatcher.Contracts.Message;

namespace RExec.Dispatcher.Contracts.Service
{
    [ServiceContract(Name = "AssemblyManager", Namespace = "http://fugro.schemas/rexec/service/assemblymanager")]
    public interface IAssemblyManager
    {
        [OperationContract]
        void AddAssembly(Assembly assembly);
    }
}
