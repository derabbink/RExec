using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace RExec.ClientProxy
{
    /// <summary>
    /// generates wrapped client channels for duplex clients
    /// </summary>
    /// <typeparam name="T">service contract</typeparam>
    /// <typeparam name="U">callback service contract</typeparam>
    public class DuplexClientFactory<T, U>
    {
        //creating this object is expensive
        private readonly DuplexChannelFactory<T> _channelFactory;
        private readonly InstanceContext _instanceContext;

        /// <summary>
        /// </summary>
        /// <param name="endpointName">name of the endpoint configuration</param>
        /// <param name="callbackHandler">implementation of callback handler</param>
        public DuplexClientFactory(string endpointName, U callbackHandler)
        {
            // Construct InstanceContext to handle messages on callback interface
            _instanceContext = new InstanceContext(callbackHandler);

            _channelFactory = new DuplexChannelFactory<T>(_instanceContext, endpointName);
        }

        public Client<T> GetClient()
        {
            T channel = _channelFactory.CreateChannel();
            return new Client<T>(channel);
        }
    }
}
