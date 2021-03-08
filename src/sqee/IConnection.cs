using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqee
{
    public interface IConnection
    {
        public Nest.ElasticClient Client { get; }
    }
}
