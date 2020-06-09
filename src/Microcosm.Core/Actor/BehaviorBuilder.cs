using System;
using System.Collections.Generic;
using System.Text;

namespace Microcosm.Core.Actor
{
    public class BehaviorBuilder<T>
    {
        public static BehaviorBuilder<T> Create()
        {
            return new BehaviorBuilder<T>();
        }

        public BehaviorBuilder<T> OnMessage()
        {
            return this;
        }
    }
}
