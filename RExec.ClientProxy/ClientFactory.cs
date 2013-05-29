using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace RExec.ClientProxy
{
    /// <summary>
    /// generates wrapped client channels
    /// </summary>
    /// <typeparam name="T">service contract</typeparam>
    public class ClientFactory<T>
    {
        //creating this object is expensive
        private readonly ChannelFactory<T> _channelFactory;

        /// <summary>
        /// </summary>
        /// <param name="endpointName">name of the endpoint configuration</param>
        public ClientFactory(string endpointName)
        {
            _channelFactory = new ChannelFactory<T>(endpointName);
        }

        public Client<T> GetClient()
        {
            T channel = _channelFactory.CreateChannel();
            return new Client<T>(channel);
        }
    }
}
