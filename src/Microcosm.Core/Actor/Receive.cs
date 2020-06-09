using System;
using System.Collections.Generic;
using System.Text;

namespace Microcosm.Core.Actor
{
    public abstract class Receive<T>
    {
        // public Behavior<T> Receive​(ActorContext<T> ctx, T msg);
        public abstract Behavior<T> ReceiveMessage​(T msg);
    }
}
