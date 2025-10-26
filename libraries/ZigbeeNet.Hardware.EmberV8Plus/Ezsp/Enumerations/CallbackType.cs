using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Hardware.EmberV8Plus.Ezsp.Enumerations
{
    public enum CallbackType
    {
        NotACallback = 0,
        Synchronous = 1,
        Asynchronous = 2,
        Reserved = 3
    }
}
