using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp
{
    public interface IFrameIdentifier
    {
        static abstract ushort FrameId { get; }
    }
}
