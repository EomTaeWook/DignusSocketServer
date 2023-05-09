using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoClient
{
    internal class DummyPacket
    {
        public DateTime Send { get; set; }

        public DateTime Receive { get; set; }

        public int Index { get; set; }
    }
}
