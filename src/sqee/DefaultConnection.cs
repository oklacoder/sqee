using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee
{
    public class DefaultConnection :
        IConnection
    {
        public ElasticClient Client { get; }

        public DefaultConnection(
            ElasticClient client)
        {
            Client = client;
        }
    }
}
