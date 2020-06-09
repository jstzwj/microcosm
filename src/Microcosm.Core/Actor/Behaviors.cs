using System;
using System.Collections.Generic;
using System.Text;

namespace Microcosm.Core.Actor
{
    public class Behaviors
    {
        public BehaviorBuilder<T> Receive<T>()
        {
            return BehaviorBuilder<T>.Create();
        }
    }
}
