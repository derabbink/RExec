using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace RExec.ClientProxy
{
    /// <summary>
    /// Wrapper class for WCF duplex channel that can disposed of without throwing exeptions
    /// </summary>
    /// <typeparam name="T">service contract</typeparam>
    public class DuplexClient<T> : IDisposable
    {
        public T Channel { get; private set; }

        public DuplexClient(T channel)
        {
            Channel = channel;
        }

        public void Dispose()
        {
            //Dispose() on ICommunicationObject can throw
            var channel = Channel as ICommunicationObject;
            try
            {
                channel.Close();
            }
            catch (CommunicationException)
            {
                channel.Abort();
            }
            catch (TimeoutException)
            {
                channel.Abort();
            }
        }
    }
}
