using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RExec.Dispatcher.Contracts.Data
{
    [DataContract(Namespace = "http://fugro.schemas/rexec/data/instruction")]
    public class Instruction
    {
        [DataMember]
        public string AssemblyName { get; set; }

        [DataMember]
        public string FQTypeName { get; set; }

        [DataMember]
        public string ActionName { get; set; }
    }
}
