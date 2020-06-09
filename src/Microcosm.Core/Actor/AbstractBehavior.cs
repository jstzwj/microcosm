using System;
using System.Collections.Generic;
using System.Text;

namespace Microcosm.Core.Actor
{
    public abstract class AbstractBehavior<T>
    {
        public abstract Receive<T> CreateReceive();

        // public Behavior<T> Receive​(ActorContext<T> ctx, T msg) { }
    }
}
