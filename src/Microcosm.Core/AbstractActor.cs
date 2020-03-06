using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microcosm.Core
{
    public abstract class AbstractActor
    {
        public abstract Task AddProperty();

        public abstract Task OnReceive(object o);
    }
}
