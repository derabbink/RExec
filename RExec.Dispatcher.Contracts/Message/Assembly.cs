using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RExec.Dispatcher.Contracts.Message
{
    [MessageContract]
    public class Assembly
    {
        //additional data must be [MessageHeader]s

        /// <summary>
        /// With a stream, there can be only ONE MessageBodyMember,
        /// or else transfer reverts to a buffered strategy
        /// </summary>
        [MessageBodyMember] public Stream AssemblyStream;
    }
}
