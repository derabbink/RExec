using RExec.Dispatcher.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace RExec.Dispatcher.Contracts.Service
{
    [ServiceContract(Name = "Executor", Namespace = "http://fugro.schemas/rexec/service/executor")]
    public interface IExecutor
    {
        [OperationContract]
        object Execute(Instruction instruction);
    }
}
