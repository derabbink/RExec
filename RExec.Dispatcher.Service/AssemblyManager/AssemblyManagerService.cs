using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RExec.Dispatcher.Contracts.Message;
using RExec.Dispatcher.Contracts.Service;

namespace RExec.Dispatcher.Service.AssemblyManager
{
    public class AssemblyManagerService : IAssemblyManager
    {
        public void AddAssembly(Assembly assembly)
        {
            MemoryStream memStream = new MemoryStream();
            assembly.AssemblyStream.CopyTo(memStream);
            byte[] streamData = memStream.ToArray();
            
            Encoding enc = new UnicodeEncoding();
            string text = enc.GetString(streamData);
            
            Console.WriteLine(text);
        }
    }
}
