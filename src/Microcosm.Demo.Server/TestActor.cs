using Microcosm.Core.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microcosm.Demo.Server
{
    public class TestActor : AbstractBehavior<TestActor.HelloMessage>
    {
        public override Receive<HelloMessage> CreateReceive()
        {
            throw new NotImplementedException();
        }

        public class HelloMessage
        {

        }
    }
}
